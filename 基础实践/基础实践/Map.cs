using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace 基础实践 {
    internal class Map {
        public List<Grid> Grids { get; private set; }
        public Map((int x, int y) beginPoint, int num) {
            Grids = new List<Grid>(num);
            Random rng = new Random();
            int randomNum;
            GridPositionCalculator cal = new GridPositionCalculator();
            var computeType = (int randomNum, int i) => {
                if (randomNum < 85 || i == 0 || i == num - 1) {
                    return GridType.Normal;
                }
                else if (randomNum >= 85 && randomNum < 90) {
                    return GridType.Boom;
                }
                else if (randomNum >= 90 && randomNum < 95) {
                    return GridType.Pause;
                }
                else {
                    return GridType.Tunnel;
                }

            };
            for (int i = 0; i < num; i++) {
                randomNum = rng.Next(0, 101);
                var type = computeType(randomNum, i);
                Grids.Add(new Grid(type, beginPoint.x, beginPoint.y));
                cal.ComputePosition(ref beginPoint, num);
            }

        }

        public void DrawMap() {
            foreach (Grid grid in Grids) {
                grid.Draw();
            }
        }

    }

    class GridPositionCalculator {
        private int _indexX;
        private int _indexY;
        private int _stride;
        public GridPositionCalculator() {
            _indexX = 0;
            _indexY = 0;
            _stride = 2;
        }

        public void ComputePosition(ref (int x, int y) beginPoint, int num) {
            if (_indexX == 10) {
                beginPoint.y++;
                _indexY++;
                if (_indexY == 2) {
                    _stride = -_stride;
                    _indexX = _indexY = 0;
                }
            }
            else {
                beginPoint.x += _stride;
                _indexX++;
            }
        }

    }
}
