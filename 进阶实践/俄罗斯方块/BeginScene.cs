using System;
using System.Collections.Generic;
using System.Text;

namespace 俄罗斯方块 {
    internal class BeginScene : BeginOrEndSceneBase {
        public BeginScene() {
            _title = "俄罗斯方块";
            _optionOne = "开始游戏";
        }
        protected override void EnterJLogic() {
            if (OptionIndex == 0) {
                Game.ChangeScene(E_SceneType.Game);
            }
            else {
                Environment.Exit(0);
            }
        }
    }
}
