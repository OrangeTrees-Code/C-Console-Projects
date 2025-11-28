using System;
using System.Collections.Generic;
using System.Text;

namespace 贪吃蛇 {
    /// <summary>
    /// 场景类型枚举
    /// </summary>
    enum E_SceneType {
        /// <summary>
        /// 开始场景
        /// </summary>
        Begin,
        /// <summary>
        /// 游戏场景
        /// </summary>
        Game,
        /// <summary>
        /// 结束场景
        /// </summary>
        End,
    }
    internal class Game {
        public const int _consoleWidth = 80;
        public const int _consoleHeight = 20;
        protected static IScenceUpdate _nowScene;

        public Game() {
            Console.CursorVisible = false;
            Console.SetWindowSize(_consoleWidth, _consoleHeight);
            Console.SetBufferSize(_consoleWidth, _consoleHeight);

            ChangeScene(E_SceneType.Begin);
        }

        public void Start() {
            while (true) {
                if (_nowScene != null) {
                    _nowScene.Update();
                }
            }
        }
        
        public static void ChangeScene(E_SceneType type) {
            (_nowScene as GameScene)?.Dispose();
            Console.Clear();
            switch (type) {
                case E_SceneType.Begin:
                    _nowScene = new BeginScene();
                    break;
                case E_SceneType.Game:
                    _nowScene = new GameScene();
                    break;
                case E_SceneType.End:
                    _nowScene = new EndScene();
                    break;
            }
        }


    }
}
