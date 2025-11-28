using System;
using System.Collections.Generic;
using System.Text;

namespace 俄罗斯方块 {
    enum E_DirType {
        Left,
        Right,
    }
    internal class BlockManager : IDraw {
        protected List<DrawObject> Blocks { get; set; }
        protected Dictionary<E_DrawType, BlockInfo> AllShapesDict { get; set; }

        private Random _rng;
        private BlockInfo _nowBlockInfo;
        public int NowShapeIndex { get; set; }

        public BlockManager() {
            AllShapesDict = new Dictionary<E_DrawType, BlockInfo>() {
                {E_DrawType.Cube,new BlockInfo(E_DrawType.Cube) },
                {E_DrawType.Line,new BlockInfo(E_DrawType.Line) },
                {E_DrawType.Tank,new BlockInfo(E_DrawType.Tank) },
                {E_DrawType.Left_Ladder,new BlockInfo(E_DrawType.Left_Ladder) },
                {E_DrawType.Right_ladder,new BlockInfo(E_DrawType.Right_ladder) },
                {E_DrawType.Left_Long_Ladder,new BlockInfo(E_DrawType.Left_Long_Ladder) },
                {E_DrawType.Right_Long_Ladder,new BlockInfo(E_DrawType.Right_Long_Ladder) }
            };
            _rng = new Random();
            RandomCreatShape();
        }

        public void RandomCreatShape() {
            int randomNum = _rng.Next(1, 8);
            E_DrawType type = (E_DrawType)(randomNum);
            Blocks = new List<DrawObject>() {
                new DrawObject(type),
                new DrawObject(type),
                new DrawObject(type),
                new DrawObject(type)
            };
            randomNum = _rng.Next(12, (Console.BufferWidth - 16)/2) * 2;
            Blocks[0].Position = new Vector2i(randomNum, -5);
            _nowBlockInfo = AllShapesDict[type];
            NowShapeIndex = _rng.Next(0, _nowBlockInfo.Count);
            SetShape();
        }

        public void Draw() {
            foreach (DrawObject block in Blocks) {
                block.Draw();
            }
        }

        public void EraseShape() {
            foreach (DrawObject block in Blocks) {
                block.Erase();
            }
        }

        public void Rotate(E_DirType type) {
            EraseShape();
            switch (type) {
                case E_DirType.Left:
                    NowShapeIndex--;
                    break;
                case E_DirType.Right:
                    NowShapeIndex++;
                    break;
            }
            SetShape();
            Draw();
        }

        public bool CanRotate(E_DirType type, Map map) {
            int tempIndex = NowShapeIndex;
            switch (type) {
                case E_DirType.Left:
                    tempIndex--;
                    break;
                case E_DirType.Right:
                    tempIndex++;
                    break;
            }
            var shape = _nowBlockInfo[tempIndex];
            foreach (var pos in shape) {
                var tempPos = Blocks[0].Position + pos;
                if (tempPos.X < 2 || tempPos.X >= map.Size.Width || tempPos.Y >= map.Size.Height - 1) {
                    return false;
                }
                foreach (var wall in map.DynamicWalls) {
                    if (pos == wall.Position) {
                        return false;
                    }
                }
            }
            return true;

        }

        public bool CanMove(E_DirType type, Map map) {
            Vector2i offset = new Vector2i(type == E_DirType.Left ? -2 : 2, 0);
            Vector2i tempPos;
            foreach (var block in Blocks) {
                tempPos = block.Position + offset;
                if (tempPos.X < 2 || tempPos.X >= map.Size.Width || tempPos.Y >= map.Size.Height-1) {
                    return false;
                }
                foreach (var wall in map.DynamicWalls) {
                    if (wall.Position == tempPos) {
                        return false;
                    }
                }
            }
            return true;
        }

        public void Move(E_DirType type) {
            EraseShape();
            Vector2i offset = new Vector2i(type == E_DirType.Left ? -2 : 2, 0);
            foreach (var block in Blocks) {
                block.Position += offset;
            }
            Draw();
        }

        public void AutoMove() {
            EraseShape();
            foreach (var block in Blocks) {
                block.Position.Y += 1;
            }
            Draw();
        }
        public bool CanAntoMove(Map map) {
            Vector2i offset = new Vector2i(0, 1);
            Vector2i tempPos;
            foreach (var block in Blocks) {
                tempPos = block.Position + offset;
                if (tempPos.Y >= map.Size.Height-1) {
                    map.AddWalls(Blocks);
                    RandomCreatShape();
                    return false;
                }
                foreach (var wall in map.DynamicWalls) {
                    if (wall.Position == tempPos) {
                        map.AddWalls(Blocks);
                        RandomCreatShape();
                        return false;
                    }
                }
            }
            return true;
        }

        private void SetShape() {
            var shape = _nowBlockInfo[NowShapeIndex];
            for (int i = 0; i < shape.Length; i++) {
                Blocks[i + 1].Position = Blocks[0].Position + shape[i];
            }
        }
    }
}
