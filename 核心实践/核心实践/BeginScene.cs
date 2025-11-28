using System;
using System.Collections.Generic;
using System.Text;

namespace 贪吃蛇 {
    internal class BeginScene : BeginOrEndSceneBase {
        public BeginScene() {
            _title = "贪吃蛇";
            _optionOne = "开始游戏";
        }
        protected override void LogicAfterEnterJ() {
            if (NowIndex == 0) {
                Game.ChangeScene(E_SceneType.Game);
            }
            else {
                Environment.Exit(0);
            }

        }
    }
}
