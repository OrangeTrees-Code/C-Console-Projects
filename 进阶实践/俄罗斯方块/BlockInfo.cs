using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace 俄罗斯方块 {
    internal class BlockInfo {
        public List<Vector2i[]> Shapes { get; set; }
        public BlockInfo(E_DrawType type) {
            Shapes = new List<Vector2i[]>();
            switch (type) {
                case E_DrawType.Cube:
                    Shapes.Add(new Vector2i[3] {
                        new Vector2i(2, 0),
                        new Vector2i(0, 1),
                        new Vector2i(2, 1),
                    });
                    break;
                case E_DrawType.Line:
                    Shapes.Add(new Vector2i[3] {
                        new Vector2i(0,-1),
                        new Vector2i(0,1),
                        new Vector2i(0,2)
                    });
                    Shapes.Add(new Vector2i[3] {
                        new Vector2i(-4,0),
                        new Vector2i(-2,0),
                        new Vector2i(2,0)
                    });
                    Shapes.Add(new Vector2i[3] {
                        new Vector2i(0,-2),
                        new Vector2i(0,-1),
                        new Vector2i(0,1)
                    });
                    Shapes.Add(new Vector2i[3] {
                        new Vector2i(-2,0),
                        new Vector2i(2,0),
                        new Vector2i(4,0)
                    });
                    break;
                case E_DrawType.Tank:
                    Shapes.Add(new Vector2i[3] {
                        new Vector2i(-2,0),
                        new Vector2i(2,0),
                        new Vector2i(0,1)
                    });
                    Shapes.Add(new Vector2i[3] {
                        new Vector2i(0,-1),
                        new Vector2i(-2,0),
                        new Vector2i(0,1)
                    });
                    Shapes.Add(new Vector2i[3] {
                        new Vector2i(0,-1),
                        new Vector2i(-2,0),
                        new Vector2i(2,0)
                    });
                    Shapes.Add(new Vector2i[3] {
                        new Vector2i(0,-1),
                        new Vector2i(2,0),
                        new Vector2i(0,1)
                    });
                    break;
                case E_DrawType.Left_Ladder:
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(0,-1),
                        new Vector2i(2,0),
                        new Vector2i(2,1)
                    });
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(2,0),
                        new Vector2i(0,1),
                        new Vector2i(-2,1)
                    });
                    Shapes.Add(new Vector2i[3]{
                       new Vector2i(-2,-1),
                        new Vector2i(-2,0),
                        new Vector2i(0,1)
                    });
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(0,-1),
                        new Vector2i(2,-1),
                        new Vector2i(-2,0)
                    });
                    break;
                case E_DrawType.Right_ladder:
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(0,-1),
                        new Vector2i(-2,0),
                        new Vector2i(-2,1)
                    });
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(-2,-1),
                        new Vector2i(0,-1),
                        new Vector2i(2,0)
                    });
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(2,-1),
                        new Vector2i(2,0),
                        new Vector2i(0,1)
                    });
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(0,1),
                        new Vector2i(2,1),
                        new Vector2i(-2,0)
                    });
                    break;
                case E_DrawType.Left_Long_Ladder:
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(-2,-1),
                        new Vector2i(0,-1),
                        new Vector2i(0,1)
                    });
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(2,-1),
                        new Vector2i(-2,0),
                        new Vector2i(2,0)
                    });
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(0,-1),
                        new Vector2i(2,1),
                        new Vector2i(0,1)
                    });
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(2,0),
                        new Vector2i(-2,0),
                        new Vector2i(-2,1)
                    });
                    break;
                case E_DrawType.Right_Long_Ladder:
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(0,-1),
                        new Vector2i(0,1),
                        new Vector2i(2,-1)
                    });
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(2,0),
                        new Vector2i(-2,0),
                        new Vector2i(2,1)
                    });
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(0,-1),
                        new Vector2i(-2,1),
                        new Vector2i(0,1)
                    });
                    Shapes.Add(new Vector2i[3]{
                        new Vector2i(-2,-1),
                        new Vector2i(-2,0),
                        new Vector2i(2,0)
                    });
                    break;
            }

        }

        public int Count {
            get => Shapes.Count;
        }
        public Vector2i[] this[int index] {
            get {
                index = Math.Abs(index) % Count;
                return Shapes[index];
            }
        }
    }
}
