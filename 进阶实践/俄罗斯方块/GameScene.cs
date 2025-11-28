using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace 俄罗斯方块 {
    internal class GameScene : ISceneUpdate {
        Map _map;
        BlockManager _blockManager;
        GameInfo _gameInfo;
        //Thread _inputThread;
        //private bool _isRunning = true;

        // 导入 Windows API
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        // 虚拟键码
        const int VK_LEFT = 0x25;   // 左箭头
        const int VK_RIGHT = 0x27;  // 右箭头
        const int VK_A = 0x41;      // A 键
        const int VK_D = 0x44;      // D 键
        const int VK_S = 0x53;      // S 键

        public GameScene() {
            _gameInfo = new GameInfo();
            _map = new Map(this,this._gameInfo);
            _blockManager = new BlockManager();
            InputThread.Instance.inputEvent+=DetectInput;
            //_inputThread = new Thread(DetectInput);
            //_inputThread.IsBackground = true;
            //_inputThread.Start();
        }

        public void Update() {
            lock (_blockManager) {
                _map.Draw();
                _blockManager.Draw();
                _gameInfo.ShowInfo();
                if (_blockManager.CanAntoMove(_map)) {
                    _blockManager.AutoMove();
                }
            }
            Thread.Sleep(500);
        }

        // 检测键是否被按下
        private bool IsKeyPressed(int keyCode) {
            return (GetAsyncKeyState(keyCode) & 0x8000) != 0;
        }

        private void DetectInput() {
            //while (_isRunning) {
                lock (_blockManager) {
                    // 检测旋转（左右箭头）
                    if (IsKeyPressed(VK_LEFT)) {
                        if (_blockManager.CanRotate(E_DirType.Left, _map)) {
                            _blockManager.Rotate(E_DirType.Left);
                            Thread.Sleep(100); // 防止旋转过快
                        }
                    }
                    if (IsKeyPressed(VK_RIGHT)) {
                        if (_blockManager.CanRotate(E_DirType.Right, _map)) {
                            _blockManager.Rotate(E_DirType.Right);
                            Thread.Sleep(100);
                        }
                    }

                    // 检测移动（A/D）
                    if (IsKeyPressed(VK_A)) {
                        if (_blockManager.CanMove(E_DirType.Left, _map)) {
                            _blockManager.Move(E_DirType.Left);
                            Thread.Sleep(50); // 防止移动过快
                        }
                    }
                    if (IsKeyPressed(VK_D)) {
                        if (_blockManager.CanMove(E_DirType.Right, _map)) {
                            _blockManager.Move(E_DirType.Right);
                            Thread.Sleep(50);
                        }
                    }

                    // 检测加速下落（S）
                    if (IsKeyPressed(VK_S)) {
                        if (_blockManager.CanAntoMove(_map)) {
                            _blockManager.AutoMove();
                        }
                    }
                }

                Thread.Sleep(10); // 主循环延迟
            //}
        }

        public void Dispose() {
            //_isRunning = false;
            //_inputThread?.Join(1000);
            InputThread.Instance.inputEvent -= DetectInput;
        }
    }
}