using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading; // 添加此 using 以确保 Thread 可用

namespace 贪吃蛇 {

    internal class GameScene : IScenceUpdate,IDisposable {
        public Map _map;
        private Snake _snake;
        private Food _food;
        private Thread _detectInput;
        private bool _isRunning = true;
        public GameScene() {
            _map = new Map();
            _snake = new Snake(Console.BufferWidth / 2, Console.BufferHeight / 2);
            _food = new Food(_snake);
            Action detectKeyboard = () => DetectInput();
            _detectInput = new Thread(new ThreadStart(detectKeyboard));
            _detectInput.IsBackground = true;
            _detectInput.Start();
        }

        public void Dispose() {
            _isRunning = false;
            _detectInput?.Join(1000);
        }

        public void Update() {
            _map.Draw();
            _food.Draw();
            _snake.Draw();
            Thread.Sleep(100);
            _snake.Move();
            if (_snake.CheckHit(_map)) {
                Game.ChangeScene(E_SceneType.End);
            }
            _snake.CheckEatFood(_food);
        }

        [DllImport("user32.dll")]
        static extern int GetAsyncKeyState(int key);

        int 

        private void DetectInput() {
            while (_isRunning) {
                if (Console.KeyAvailable) {
                    switch (Console.ReadKey(true).Key) {
                        case ConsoleKey.W:
                            _snake.ChangeMoveDir(E_MoveDir.Up);
                            break;
                        case ConsoleKey.A:
                            _snake.ChangeMoveDir(E_MoveDir.Left);
                            break;
                        case ConsoleKey.S:
                            _snake.ChangeMoveDir(E_MoveDir.Down);
                            break;
                        case ConsoleKey.D:
                            _snake.ChangeMoveDir(E_MoveDir.Right);
                            break;
                    }
                }
                Thread.Sleep(10); // 防止CPU占用过高
            }
        }
    }
}
