using System;
using System.Collections.Generic;
using System.Text;

namespace 贪吃蛇 {
    enum E_MoveDir {
        Up = -1,
        Down = 1,
        Left = -2,
        Right = 2,
    }

    static class E_MoveDirExtension {
        public static E_MoveDir Flip(this E_MoveDir dir) {
            return (E_MoveDir)(-((int)dir));
        }
    }


    internal class Snake : IDraw {
        private SnakeBody[] _bodys;
        private int _count;
        private const int _maxLength = 200;
        private E_MoveDir _moveDir;

        public Snake(int x, int y) {
            _bodys = new SnakeBody[_maxLength];
            _bodys[0] = new SnakeBody(x, y, E_SnakeBodyType.Head);
            _count++;
        }
        public void Draw() {
            for (int i = 0; i < _count; i++) {
                _bodys[i].Draw();
            }
        }

        public void Move() {
            WipeTheLastBody();
            MoveBody();
            switch (_moveDir) {
                case E_MoveDir.Up:
                    _bodys[0].Position.Y--;
                    break;
                case E_MoveDir.Down:
                    _bodys[0].Position.Y++;
                    break;
                case E_MoveDir.Left:
                    _bodys[0].Position.X -= 2;
                    break;
                case E_MoveDir.Right:
                    _bodys[0].Position.X += 2;
                    break;
            }
        }

        public void ChangeMoveDir(E_MoveDir dir) {
            if(dir == _moveDir||_count>1&&dir==_moveDir.Flip()) {
                return;
            }
            _moveDir = dir;
        }

        public bool CheckHit(Map map) {
            for (int i = 0; i < map._walls.Length; i++) {
                if (map._walls[i].Position == _bodys[0].Position) {
                    return true;
                }
            }
            for(int i = 1; i < _count; i++) {
                if (_bodys[i].Position == _bodys[0].Position) {
                    return true; 
                }
            }
            return false;
        }

        public bool CheckOverlap(Vector2i v) {
            for (int i = 0; i < _count; i++) {
                if (_bodys[i].Position == v) {
                    return true;
                }
            }
            return false;
        }

        public void CheckEatFood(Food food) {
            if (food.Position == _bodys[0].Position) {
                food.RandomSpawn(this);
                AddBody();
            }
        }
        private void WipeTheLastBody() {
            var lastBody = _bodys[_count - 1];
            Global.Erase(lastBody.Position);
        }
        private void AddBody() {
            var lastBody = _bodys[_count - 1];
            _bodys[_count] = new SnakeBody(lastBody.Position.X, lastBody.Position.Y, E_SnakeBodyType.Body);
            _count++;
        }
        private void MoveBody() {
            for(int i = _count - 1; i > 0; i--) {
                _bodys[i].Position = _bodys[i - 1].Position;
            }
        }
    }
}
