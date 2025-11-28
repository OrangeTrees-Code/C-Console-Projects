using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
//using System.Linq;

namespace 俄罗斯方块 {
    internal class Map : IDraw {
        protected List<DrawObject> StaticWalls { get; set; }
        public List<DrawObject> DynamicWalls { get; private set; }

        private int _width = Console.BufferWidth - 10;
        private int _height = Console.BufferHeight;
        public (int Width, int Height) Size {
            get => (_width, _height);
        }

        private int[] layerInfo = new int[Console.BufferHeight-1];

        private List<DrawObject> PreDeleteBlocks = new List<DrawObject>();

        private GameScene _gameScene;

        private GameInfo _gameInfo;
        public Map(GameScene gameScene, GameInfo gameInfo) {
            StaticWalls = new List<DrawObject>();
            DynamicWalls = new List<DrawObject>();
            for (int i = 0; i < _width; i += 2) {
                StaticWalls.Add(new DrawObject(E_DrawType.Wall, i, Console.BufferHeight - 1));
            }
            for (int i = 0; i < _height; i++) {
                StaticWalls.Add(new DrawObject(E_DrawType.Wall, 0, i));
                StaticWalls.Add(new DrawObject(E_DrawType.Wall, Console.BufferWidth - 10, i));
            }

            _gameScene = gameScene;
            _gameInfo = gameInfo;
        }
        public void Draw() {
            foreach (DrawObject wall in StaticWalls) {
                wall.Draw();
            }
            DrawDynamicWalls();
        }


        public void AddWalls(List<DrawObject> blocks) {
            foreach (DrawObject block in blocks) {
                block.Type = E_DrawType.Wall;
                DynamicWalls.Add(block);
                if (block.Position.Y <= 0) {
                    Game.ChangeScene(E_SceneType.End);
                    return;
                }
                layerInfo[block.Position.Y]++;
                
            }
            List<int> preDelLayers = new List<int>();
            for (int i = 0; i < layerInfo.Length; i++) {
                if (layerInfo[i] == Size.Width / 2 - 1) {
                    preDelLayers.Add(i);
                    _gameInfo.Score++;
                }
            }
            if (preDelLayers.Count > 0) {
                EraseDynamicWalls();
                EraseLayer(preDelLayers);
                DrawDynamicWalls();
            }
        }

        private void EraseLayer(List<int> preDelLayers) {
            PreDeleteBlocks.Clear();

            foreach (var wall in DynamicWalls) {
                if (preDelLayers.Contains(wall.Position.Y)) {
                    PreDeleteBlocks.Add(wall);
                }
                else {
                    // 计算该方块下方有多少被消除的层
                    int moveDown = 0;
                    foreach (int delLayer in preDelLayers) {
                        if (delLayer > wall.Position.Y) {
                            moveDown++;
                        }
                    }
                    wall.Position.Y += moveDown;
                }
            }

            // 删除方块
            foreach (var block in PreDeleteBlocks) {
                DynamicWalls.Remove(block);
            }

            // 更新 layerInfo
            // 方法1: 重新统计(简单但稍慢)
            Array.Clear(layerInfo, 0, layerInfo.Length);
            foreach (var wall in DynamicWalls) {
                layerInfo[wall.Position.Y]++;
            }

        }

        private void EraseDynamicWalls() {
            foreach (var wall in DynamicWalls) {
                wall.Erase();
            }
        }

        private void DrawDynamicWalls() {
            foreach (var wall in DynamicWalls) {
                wall.Draw();
            }
        }

        

    }
}
