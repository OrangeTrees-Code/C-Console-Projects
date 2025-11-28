using System;
using System.Collections.Generic;
using System.Text;

namespace 俄罗斯方块 {
    internal class InputThread {
        private Thread _inputThread;
        public event Action inputEvent;
        private static InputThread _instance = new InputThread();
        public static InputThread Instance {
            get {
                return _instance;
            }
        }

        private InputThread() {
            _inputThread = new Thread(DetectInput);
            _inputThread.IsBackground = true;
            _inputThread.Start();
        }

        private void DetectInput() {
            while (true) {
                inputEvent?.Invoke();
            }
        }

    }
}
