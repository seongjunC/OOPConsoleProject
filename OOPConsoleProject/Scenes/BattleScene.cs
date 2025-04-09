using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject.Scenes
{
    public class BattleScene : BaseScene
    {
        bool IsFirst;
        bool IsBattleEnd;

        public BattleScene()
        {
            IsFirst = true;
            IsBattleEnd = false;
        }

        // TODO : 몬스터 구현 
        public override void Render()
        {
            Console.SetCursorPosition(0, Console.WindowWidth / 2);
            Console.WriteLine("몬스터 미 구현");
        }

        public override void Input()
        {
            if (IsFirst)
            {
                Console.WriteLine("아무키나 눌러 전투를 시작합니다.");
                Console.ReadKey(true);
                IsFirst = false;
            }
            else
            {
                Console.WriteLine("아무키나 눌러 전투를 계속 진행합니다.");
                Console.ReadKey(true);
            }
        }


        public override void Result()
        {
            if (IsBattleEnd)
            {
                Game.ChangeScene("map");
            }
        }

        public override void Update()
        {

        }

        public override void SceneDic()
        {
            Game.InsertDic("Battle", this);
        }
    }
}
