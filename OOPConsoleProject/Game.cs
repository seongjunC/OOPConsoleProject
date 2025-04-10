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
        // 게임이 종료되는 조건이다.
        public static bool gameOver;
        // 현재 화면에 표시될 씬이다.
        private static BaseScene curScene;
        // TODO : 씬을 스택에 담아 보관한다.
        private static Stack<string> SceneStack = new Stack<string>();
        // 씬 전환에 사용할 수 있도록 딕셔너리에 씬들을 보관한다.
        private static Dictionary<string, BaseScene> sceneDic = new Dictionary<string, BaseScene>();
        // 플레이어 객체를 생성한다.
        private static Player player;
        public static Player Player { get { return player; } } 


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


        // 딕셔너리에 씬을 넣는 역할
        public static void InsertDic(string SceneName, BaseScene scene)
        {
            sceneDic[SceneName] = scene;
        }

        // curScene에 딕셔너리에 존재하는 씬을 넣어 씬을 전환해준다.
        public static void ChangeScene(string SceneName)
        {
            
            // 씬의 퇴장 시에 필요한 행동이 있으면 진행한다. (초기화 등)
            curScene.Exit();
            curScene = sceneDic[SceneName];
            // 씬의 입장 시에 필요한 행동이 있으면 진행한다. (초기 설정) 
            curScene.Enter();
        }

        // 게임이 시작될 때의 초기 설정을 진행한다.
        private static void Start()
        {
            Console.CursorVisible = false;

            gameOver = false;
            player = new Player();

            // 각 Scene에서 해당 씬의 이름과 씬을 딕셔너리에 넣는다.
            // 씬을 추가로 구현할 때마다 추가해 줘야한다. 
            new TitleScene().SceneDic();
            new MapScene().SceneDic();
            new BattleScene().SceneDic();

            // 시작할 땐 먼저 title씬이 표시되도록 curScene에 넣는다.
            curScene = sceneDic["title"];
            
        }

        // 게임의 마무리를 진행한다.
        private static void End()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("BP가 0이 되었습니다....");
            Console.WriteLine("게임 오버......");

            Console.WriteLine("플레이어의 레벨 : {0}", player.Level);
            Console.Write("플레이어의 스탯 : ");
            player.Stat.PrintStat();

            Console.WriteLine("게임을 종료합니다.");
        }
    }
}
