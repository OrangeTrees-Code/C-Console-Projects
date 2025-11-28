using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace 基础实践 {
    internal class Grid {
        public GridType Type { get; private set; }
        public Vector2i Position { get; private set; }
        public static Dictionary<GridType, (string icon, ConsoleColor color)> Grid_String_Map { get; private set; }
        public Grid(GridType type,int x,int y) { 
            Type = type;
            Position = new Vector2i(x,y);
        }
        static Grid() {
            Grid_String_Map = new Dictionary<GridType, (string icon,ConsoleColor color)>{
                {GridType.Normal,("□",ConsoleColor.White) },
                {GridType.Pause,("‖",ConsoleColor.Blue)},
                {GridType.Boom,("●",ConsoleColor.Red) },
                {GridType.Tunnel,("¤",ConsoleColor.Yellow) }
            };

        }
        public void Draw() {
            var gridInfo = Grid_String_Map[Type];
            SceneManager.ShowSomeWords(gridInfo.icon, Position.X, Position.Y, gridInfo.color);
        }

    }

    enum GridType {
        Normal,
        Pause,
        Boom,
        Tunnel,
    }

    struct Vector2i {
        public int X;
        public int Y;
        public Vector2i(int x,int y) {
            X = x; Y = y;
        }
    }
}
