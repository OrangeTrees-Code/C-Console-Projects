using System;
using System.Collections.Generic;
using System.Security.Cryptography;
namespace Main {
    internal class Program {
        static public int WindowWidth { get; set; }
        static public int WindowHeight { get; set; }
        static void Main(string[] args) {
            WindowWidth = 50;
            WindowHeight = 30;
            Console.CursorVisible = false;
            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.SetBufferSize(WindowWidth, WindowHeight);
            SceneID scene = SceneID.Lobby;
            Lobby lobby = new Lobby();
            GameState gameState = new GameState();
            while (true) {
                switch (scene) {
                    case SceneID.Lobby:
                        #region 开始场景逻辑
                        Console.Clear();
                        lobby.Show();
                        while (true) {
                            bool isQuitWhile = false;
                            switch (Console.ReadKey(true).Key) {
                                case ConsoleKey.W:
                                    lobby.NowIndex = lobby.NowIndex > 0 ? lobby.NowIndex - 1 : 0;
                                    lobby.Show();
                                    break;
                                case ConsoleKey.S:
                                    lobby.NowIndex = lobby.NowIndex < 1 ? lobby.NowIndex + 1 : 1;
                                    lobby.Show();
                                    break;
                                case ConsoleKey.J:
                                    switch (lobby.NowIndex) {
                                        case 0:
                                            scene = SceneID.Game;
                                            isQuitWhile = true;
                                            break;
                                        case 1:
                                            Environment.Exit(0);
                                            break;
                                    }
                                    break;
                            }
                            if (isQuitWhile == true) {
                                break;
                            }
                        }
                        #endregion
                        break;
                    case SceneID.Game:
                        #region 游戏场景逻辑
                        Console.Clear();
                        Boss boss = new Boss(24, 15, "■", ConsoleColor.Green, (7, 13), 100);
                        Player player = new Player(4, 5, "●", ConsoleColor.Yellow, (8, 12), 100);
                        Princess princess = null;
                        gameState.ShowWalls();
                        player.DrawIfAlive();
                        bool IsReadyToEnd = false;
                        while (!IsReadyToEnd) {
                            if (!player.IsFight) {
                                while (!player.IsFight && !IsReadyToEnd) {
                                    if (princess != null) {
                                        princess.DrawIfAlive();
                                    }
                                    boss.DrawIfAlive();
                                    //player.Clear();
                                    switch (Console.ReadKey(true).Key) {
                                        case ConsoleKey.W:
                                            player.Dir = Move_dir.Up;
                                            player.flush(boss, princess);
                                            break;
                                        case ConsoleKey.S:
                                            player.Dir = Move_dir.Down;
                                            player.flush(boss, princess);

                                            break;
                                        case ConsoleKey.A:
                                            player.Dir = Move_dir.Left;
                                            player.flush(boss, princess);

                                            break;
                                        case ConsoleKey.D:
                                            player.Dir = Move_dir.Right;
                                            player.flush(boss, princess);

                                            break;
                                        case ConsoleKey.J:
                                            if (boss.IsAlive && EntityBase.ComputeDistance(player, boss) <= 1) {
                                                gameState.ShowGuidance();
                                                gameState.ShowHealthInfo(player, boss);
                                                player.IsFight = true;
                                            }
                                            else if (princess != null && princess.IsAlive && EntityBase.ComputeDistance(player, princess) <= 1) {
                                                scene = SceneID.Exit;
                                                gameState.IsWIn = true;
                                                IsReadyToEnd = true;
                                            }
                                            break;
                                    }
                                }
                            }
                            if (player.IsFight) {
                                DamageInfoPayload payload = new DamageInfoPayload();

                                while (true) {
                                    if (Console.ReadKey(true).Key == ConsoleKey.J && player.IsAlive) {
                                        payload = player.Attack(boss);
                                        gameState.ShowDamageInfo(payload);
                                        if (!boss.IsAlive) {
                                            gameState.ShowSuccessInfo();
                                            if (Console.ReadKey(true).Key == ConsoleKey.J) {
                                                princess = new Princess(24, 5, "★", ConsoleColor.Blue);
                                                boss.Clear();
                                                player.IsFight = false;
                                                break;
                                            }
                                        }
                                    }
                                    if (Console.ReadKey(true).Key == ConsoleKey.J && boss.IsAlive) {
                                        payload = boss.Attack(player);
                                        gameState.ShowDamageInfo(payload);
                                        if (!player.IsAlive) {
                                            gameState.ShowFailureInfo();
                                            if (Console.ReadKey(true).Key == ConsoleKey.J) {
                                                player.IsFight = false;
                                                IsReadyToEnd = true;
                                                scene = SceneID.Exit;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        break;
                    case SceneID.Exit:
                        #region 结束场景逻辑
                        gameState.ShowFinalScene();
                        lobby.Show_Final();
                        bool isQuitWhile2 = false;
                        while (!isQuitWhile2) {
                            switch (Console.ReadKey(true).Key) {
                                case ConsoleKey.W:
                                    lobby.EndIndex = lobby.EndIndex > 0 ? lobby.EndIndex - 1 : 0;
                                    lobby.Show_Final();
                                    break;
                                case ConsoleKey.S:
                                    lobby.EndIndex = lobby.EndIndex < 1 ? lobby.EndIndex + 1 : 1;
                                    lobby.Show_Final();
                                    break;
                                case ConsoleKey.J:
                                    switch (lobby.EndIndex) {
                                        case 0:
                                            scene = SceneID.Lobby;
                                            isQuitWhile2 = true;
                                            Console.Clear();
                                            break;
                                        case 1:
                                            Environment.Exit(0);
                                            break;
                                    }
                                    break;
                            }

                        }
                        #endregion
                        break;
                }
            }

            
        }




    }


}
