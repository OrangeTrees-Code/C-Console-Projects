using System;
using System.Collections.Generic;
using System.Text;

namespace 俄罗斯方块 {
    enum E_SceneType {
        Begin,
        Game,
        End,
    }
    internal class Game {
        protected int _consoleWidth { get; set; }
        protected int _consoleHeight { get; set; }
        public static ISceneUpdate _nowScene {  get; set; }
        public Game() {
            _consoleWidth = 212;
            _consoleHeight = 62;

            Console.CursorVisible = false;
            Console.SetWindowSize(_consoleWidth, _consoleHeight);
            Console.SetBufferSize(_consoleWidth, _consoleHeight);
            ChangeScene(E_SceneType.Begin);

        }

        public void Start() {
            while (true) {
                _nowScene?.Update();
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
