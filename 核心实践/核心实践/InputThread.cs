using System;
using System.Collections.Generic;
using System.Text;

namespace 贪吃蛇 {
    internal class InputThread {
        private Thread _inputThread;
        public event Action InputEvent;
        private static InputThread _instance = new InputThread();
        public static InputThread Instance { get => _instance; }

        private InputThread() {
            _inputThread = new Thread(DetectInput);
            _inputThread.IsBackground = true;
            _inputThread.Start();
        }

        private void DetectInput() {
            while (true) {
                InputEvent?.Invoke();
            }
        }
    }
}
