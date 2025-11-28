using Main;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace 基础实践 {
    class SceneManager {
        public int MenuIndex { get; private set; }
        public SceneID CurrentSceneID { get; private set; }
        public Map GameMap { get; private set; }
        public List<EntityBase> Entities { get; private set; }
        public Dictionary<int, int> PosIndex_Counts_Map { get; private set; }
        public Random Rng { get; private set; }



        public SceneManager(Map gameMap) {
            MenuIndex = 0;
            CurrentSceneID = SceneID.Lobby;
            GameMap = gameMap;
            Entities = new List<EntityBase>() { new Player(0), new Bot(0) };
            PosIndex_Counts_Map = new Dictionary<int, int>();
            foreach (EntityBase entity in Entities) {
                PosIndex_Counts_Map[entity.PosIndex] = PosIndex_Counts_Map.GetValueOrDefault(entity.PosIndex, 0) + 1;
            }
            Rng = new Random();

        }

        private void Reset() {
            var beginPoint = GameMap.Grids[0].Position;
            var gridNum = GameMap.Grids.Count;
            GameMap = new Map((beginPoint.X, beginPoint.Y), gridNum);
            Entities = new List<EntityBase>() { new Player(0), new Bot(0) };
            PosIndex_Counts_Map = new Dictionary<int, int>();
            foreach (EntityBase entity in Entities) {
                PosIndex_Counts_Map[entity.PosIndex] = PosIndex_Counts_Map.GetValueOrDefault(entity.PosIndex, 0) + 1;
            }
        }
        private void ChooseScene_Menu() {
            bool isInLobby = true;
            while (isInLobby) {
                switch (Console.ReadKey(true).Key) {
                    case ConsoleKey.W:
                        MenuIndex = MenuIndex > 0 ? MenuIndex - 1 : 0;
                        Show_Menu();
                        break;
                    case ConsoleKey.S:
                        MenuIndex = MenuIndex < 1 ? MenuIndex + 1 : 1;
                        Show_Menu();
                        break;
                    case ConsoleKey.J:
                        switch (MenuIndex) {
                            case 0:
                                CurrentSceneID = CurrentSceneID == SceneID.Lobby ? SceneID.Game : SceneID.Lobby;
                                Reset();
                                GC.Collect();
                                break;
                            case 1:
                                Environment.Exit(0);
                                break;
                        }
                        isInLobby = false;
                        break;
                }

            }
        }

        private void Show_Menu() {
            ConsoleColor colorAtIndex2 = MenuIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
            ConsoleColor colorAtIndex1 = MenuIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
            switch (CurrentSceneID) {

                case SceneID.Lobby:
                    ShowSomeWords("飞行棋", Console.BufferWidth / 2 - 3, 8, ConsoleColor.White);
                    ShowSomeWords("开始游戏", Console.BufferWidth / 2 - 4, 13, colorAtIndex1);
                    ShowSomeWords("结束游戏", Console.BufferWidth / 2 - 4, 15, colorAtIndex2);
                    break;
                case SceneID.End:
                    ShowSomeWords("游戏结束", Console.BufferWidth / 2 - 4, 8, ConsoleColor.White);
                    ShowSomeWords("返回到主菜单", Console.BufferWidth / 2 - 6, 13, colorAtIndex1);
                    ShowSomeWords("结束游戏", Console.BufferWidth / 2 - 4, 15, colorAtIndex2);
                    break;
            }
        }
        private void ShowUnchangedScene() {
            DrawWalls();
            ShowTips();
        }
        private void ShowTips() {
            ShowSomeWords("□:普通格子", 2, Console.BufferHeight - 10, ConsoleColor.White);
            ShowSomeWords("‖:暂停，一回合不动", 2, Console.BufferHeight - 9, ConsoleColor.Blue);
            ShowSomeWords("●:炸弹，倒退5格", 26, Console.BufferHeight - 9, ConsoleColor.Red);
            ShowSomeWords("¤:时空隧道，随机倒退，暂停，换位置", 2, Console.BufferHeight - 8, ConsoleColor.Yellow);
            ShowSomeWords("★:玩家", 2, Console.BufferHeight - 7, ConsoleColor.Cyan);
            ShowSomeWords("▲:电脑", 12, Console.BufferHeight - 7, ConsoleColor.Magenta);
            ShowSomeWords("◎:玩家和电脑重合", 22, Console.BufferHeight - 7, ConsoleColor.DarkGreen);
            ShowSomeWords("按任意键开始扔色子", 2, Console.BufferHeight - 5, ConsoleColor.White);


        }

        private void ClearBottomTips() {
            ClearLine(Console.BufferHeight - 5);
            ClearLine(Console.BufferHeight - 4);
            ClearLine(Console.BufferHeight - 3);
            ClearLine(Console.BufferHeight - 2);
        }

        private void ClearLine(int line) {
            for (int i = 2; i < Console.BufferWidth - 2; i++) {
                Console.SetCursorPosition(i, line);
                Console.Write(" ");
            }
        }
        public static void ShowSomeWords(string str, int x, int y, ConsoleColor color) {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(str);
        }
        private void DrawWalls() {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < Console.BufferWidth; i += 2) {
                DrawSingleWall(i, 0);
                DrawSingleWall(i, Console.BufferHeight - 1);
                DrawSingleWall(i, Console.BufferHeight - 6);
                DrawSingleWall(i, Console.BufferHeight - 11);

            }
            for (int i = 0; i < Console.BufferHeight; i++) {
                DrawSingleWall(0, i);
                DrawSingleWall(Console.BufferWidth - 2, i);
            }

        }

        private static void DrawSingleWall(int x, int y) {
            Console.SetCursorPosition(x, y);
            Console.Write("■");
        }
        public void RunGameLogic() {
            ShowUnchangedScene();
            GameMap.DrawMap();
            DrawEnitys();
            bool isGameOver = false;
            while (!isGameOver) {
                Console.ReadKey(true);
                RandomMove(Entities[0]);
                GameMap.DrawMap();
                DrawEnitys();
                isGameOver = IsWin(Entities[0]);
                if (isGameOver) {
                    Console.ReadKey(true);
                    CurrentSceneID = SceneID.End;
                    break;
                }
                Console.ReadKey(true);
                RandomMove(Entities[1]);
                GameMap.DrawMap();
                DrawEnitys();
                isGameOver = IsWin(Entities[1]);
                if (isGameOver) {
                    Console.ReadKey(true);

                    CurrentSceneID = SceneID.End;
                    break;
                }

            }

        }

        private void DrawEnitys() {
            foreach (EntityBase entity in Entities) {
                entity.Draw(GameMap);
            }
            foreach (var Key in PosIndex_Counts_Map.Keys) {
                if (PosIndex_Counts_Map[Key] > 1) {
                    Draw_AtSamePos(Key);
                }
            }
        }

        private void RandomMove(EntityBase entity) {
            ClearBottomTips();
            Console.ForegroundColor = entity.Type == EntityType.Player ? ConsoleColor.Cyan : ConsoleColor.Magenta;
            if (entity.IsPause) {
                Console.SetCursorPosition(2, Console.BufferHeight - 5);
                Console.Write($"处于暂停点,{entity.Type}需要暂停一回合");
                Console.SetCursorPosition(2, Console.BufferHeight - 4);
                Console.Write($"按任意键,让{(EntityType)(((int)entity.Type + 1) % 2)}开始扔色子");
                entity.IsPause = false;
                return;
            }
            //GameMap.Grids[entity.PosIndex].Draw();
            PosIndex_Counts_Map[entity.PosIndex]--;
            int randomNum = Rng.Next(1, 7);
            Console.SetCursorPosition(2, Console.BufferHeight - 5);
            Console.Write($"{entity.Type}扔出的点数为: {randomNum}");
            int res = entity.PosIndex + randomNum;
            entity.PosIndex = res > GameMap.Grids.Count - 1 ? GameMap.Grids.Count - 1 : res;
            PosIndex_Counts_Map[entity.PosIndex] = PosIndex_Counts_Map.GetValueOrDefault(entity.PosIndex, 0) + 1;
            ActiveEffect(entity.PosIndex, entity);
        }

        private void ActiveEffect(int posIndex, EntityBase entity) {
            switch (GameMap.Grids[posIndex].Type) {
                case GridType.Normal:
                    Console.SetCursorPosition(2, Console.BufferHeight - 4);
                    Console.Write($"{entity.Type}到达了一个安全位置");
                    Console.SetCursorPosition(2, Console.BufferHeight - 3);
                    Console.Write($"按任意键,让{(EntityType)(((int)entity.Type + 1) % 2)}开始扔色子");
                    break;
                case GridType.Pause:
                    Console.SetCursorPosition(2, Console.BufferHeight - 4);
                    Console.Write($"{entity.Type}到达了暂停点,需要暂停一回合");
                    Console.SetCursorPosition(2, Console.BufferHeight - 3);
                    Console.Write($"按任意键,让{(EntityType)(((int)entity.Type + 1) % 2)}开始扔色子");
                    entity.IsPause = true;
                    break;
                case GridType.Boom:
                    Console.SetCursorPosition(2, Console.BufferHeight - 4);
                    Console.Write($"{entity.Type}踩到了炸弹,退后五格");
                    Console.SetCursorPosition(2, Console.BufferHeight - 3);
                    Console.Write($"按任意键,让{(EntityType)(((int)entity.Type + 1) % 2)}开始扔色子");
                    PosIndex_Counts_Map[entity.PosIndex]--;
                    entity.PosIndex = entity.PosIndex - 5 < 0 ? 0 : entity.PosIndex - 5;
                    PosIndex_Counts_Map[entity.PosIndex] = PosIndex_Counts_Map.GetValueOrDefault(entity.PosIndex, 0) + 1;
                    break;
                case GridType.Tunnel:
                    Console.SetCursorPosition(2, Console.BufferHeight - 4);
                    Console.Write($"{entity.Type}踩到了时空隧道");
                    Console.SetCursorPosition(2, Console.BufferHeight - 3);
                    int randomNum = Rng.Next(0, 101);
                    if (randomNum <= 30) {
                        Console.Write($"触发倒退5格");
                        PosIndex_Counts_Map[entity.PosIndex]--;
                        entity.PosIndex = entity.PosIndex - 5 < 0 ? 0 : entity.PosIndex - 5;
                        PosIndex_Counts_Map[entity.PosIndex] = PosIndex_Counts_Map.GetValueOrDefault(entity.PosIndex, 0) + 1;
                    }
                    else if (randomNum <= 60) {
                        Console.Write($"触发暂停一回合");
                        entity.IsPause = true;
                    }
                    else {
                        Console.Write($"惊喜,惊喜,双方交换位置");
                        EntityBase other = entity;
                        while (other == entity) {
                            int randomIndex = Rng.Next(0, Entities.Count);
                            other = Entities[randomIndex];
                        }
                        (other.PosIndex, entity.PosIndex) = (entity.PosIndex, other.PosIndex);
                    }
                    Console.SetCursorPosition(2, Console.BufferHeight - 2);
                    Console.Write($"按任意键,让{(EntityType)(((int)entity.Type + 1) % 2)}开始扔色子");
                    break;
            }
        }

        private bool IsWin(EntityBase entity) {
            if (entity.PosIndex == GameMap.Grids.Count - 1) {
                ClearBottomTips();
                Console.SetCursorPosition(2, Console.BufferHeight - 4);
                switch (entity.Type) {
                    case EntityType.Player:
                        Console.Write("恭喜你,你率先到达了终点");
                        break;
                    case EntityType.Bot:
                        Console.Write("很遗憾,电脑率先到达了终点");
                        break;
                }
                Console.SetCursorPosition(2, Console.BufferHeight - 3);
                Console.Write("请按任意键结束游戏");
                return true;
            }
            return false;
        }

        private void Draw_AtSamePos(int posIndex) {
            var pos = GameMap.Grids[posIndex].Position;
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("◎");
        }
        public void RunMenuLogic() {
            Show_Menu();
            ChooseScene_Menu();
        }
    }
    internal enum SceneID {
        Lobby,
        Game,
        End

    }
}
