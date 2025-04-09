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
        Monster monster;
        int monNowHp;
        int playerNowHp;
        Player player = Game.Player;

        public BattleScene()
        {
            IsFirst = true;
            IsBattleEnd = false;
            List<MonsterBuilder> BuilderList = new List<MonsterBuilder>(); 

            MonsterBuilder orangeMushroomBuilder = new MonsterBuilder();
            orangeMushroomBuilder
                .SetName("주황버섯")
                .SetGold(100)
                .SetEXP(100)
                .SetStat(new Stat(100, 5, 5, 10, 7));




            BuilderList.Add( orangeMushroomBuilder );

        }

        // TODO : 몬스터 구현 
        public override void Render()
        {
            Console.SetCursorPosition(0, Console.WindowWidth / 2);
            Console.WriteLine("몬스터 미 구현");
            Console.WriteLine("{0} / {1} : 몬스터의 체력", monNowHp, monster.Stat.Hp);
            Console.WriteLine("{0} / {1} : 플레이어의 체력", playerNowHp, player.Stat.Hp);
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

        public override void Enter()
        {

            monNowHp = monster.Stat.Hp;
            playerNowHp = player.Stat.Hp;
        }

        public override void SceneDic()
        {
            Game.InsertDic("Battle", this);
        }
    }
}
