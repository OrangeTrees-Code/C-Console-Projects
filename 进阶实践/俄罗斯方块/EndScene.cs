using System;
using System.Collections.Generic;
using System.Text;

namespace 俄罗斯方块 {
    internal class EndScene : BeginOrEndSceneBase {
        public EndScene() {
            _title = "游戏结束";
            _optionOne = "返回到主菜单";
        }
        protected override void EnterJLogic() {
            if (OptionIndex == 0) {
                Game.ChangeScene(E_SceneType.Begin);
            }
            else {
                Environment.Exit(0);
            }
        }
    }
}
