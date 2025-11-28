using System;
using System.Collections.Generic;
using System.Text;

namespace 贪吃蛇 {
    internal class EndScene : BeginOrEndSceneBase {
        public EndScene() {
            _title = "游戏结束";
            _optionOne = "返回到主界面";
        }
        protected override void LogicAfterEnterJ() {
            if (NowIndex == 0) {
                Game.ChangeScene(E_SceneType.Begin);
            }
            else {
                Environment.Exit(0);
            }
        }
    }
}
