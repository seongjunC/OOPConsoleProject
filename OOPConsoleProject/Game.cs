using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OOPConsoleProject.Scenes;

namespace OOPConsoleProject
{
    public static class Game
    {
        public static bool gameOver;
        private static BaseScene curScene;
        private static Stack<string> SceneStack = new Stack<string>();
        private static Dictionary<string, BaseScene> sceneDic = new Dictionary<string, BaseScene>();



        public static void Run()
        {

            Start();

            while (!gameOver)
            {
                Console.Clear();
                curScene.Render();
                curScene.Input();
                Console.WriteLine();
                curScene.Update();
                Console.WriteLine();
                curScene.Result();
            }

            End();
        }

        public static void InsertDic(string SceneName, BaseScene scene)
        {
            sceneDic[SceneName] = scene;
        }

        public static void ChangeScene(string SceneName)
        {
            curScene = sceneDic[SceneName];
        }

        private static void Start()
        {
            Console.CursorVisible = false;

            gameOver = false;

            new TitleScene().SceneDic();


        }

        private static void End()
        {

        }
    }
}
