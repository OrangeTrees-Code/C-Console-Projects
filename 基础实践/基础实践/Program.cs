using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using 基础实践;

internal class Program {
    public static int ConsoleWidth = 50;
    public static int ConsoleHeight = 30;

    static void Main(string[] args) {
        ConsoleInit();
        SceneManager sceneManager = new SceneManager(new Map((14,3),80));
        while (true) {
            switch (sceneManager.CurrentSceneID) {
                case SceneID.Lobby:
                    Console.Clear();
                    sceneManager.RunMenuLogic();
                    break;
                case SceneID.Game:
                    Console.Clear();
                    sceneManager.RunGameLogic();
                    break;
                case SceneID.End:

                    Console.Clear();
                    sceneManager.RunMenuLogic();

                    break;
            }
        }
    }

    static void ConsoleInit() {
        Console.CursorVisible = false;
        Console.SetWindowSize(ConsoleWidth, ConsoleHeight);
        Console.SetBufferSize(ConsoleWidth, ConsoleHeight);
    }
}


