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
        bool IsPlayerDead;
        bool IsEnemyDead;
        Monster monster;
        List<MonsterBuilder> BuilderList = new List<MonsterBuilder>();
        int monNowHp;
        int playerNowHp;
        Player player = Game.Player;
        Random random = new Random();
        int MapNumber = MapScene.MapNumber;

        public BattleScene()
        {
            MonsterBuilder orangeMushroomBuilder = new MonsterBuilder();
            orangeMushroomBuilder
                .SetName("주황 버섯")
                .SetGold(100)
                .SetEXP(100)
                .SetStat(new Stat(100, 7, 7, 10, 7));


            BuilderList.Add( orangeMushroomBuilder );

        }

        // TODO : 몬스터 구현 
        public override void Render()
        {
            Console.SetCursorPosition(0, 0);
            foreach (string str in monster.Art)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine("{0} / {1} : 몬스터의 체력", monNowHp, monster.Stat.Hp);
            Console.WriteLine("{0} / {1} : 플레이어의 체력", playerNowHp, player.Stat.Hp);
        }

        public override void Input()
        {
            if (IsFirst)
            {
                Util.ReadyPlayer();
                IsFirst = false;
            }

        }


        public override void Update()
        {
            int playerAttackRepeat = Util.CalculateRepeat(player.Stat, monster.Stat);
            int monsterAttackRepeat = Util.CalculateRepeat(monster.Stat, monster.Stat);
            int playerDamage = Util.CalculateDamage(player.Stat, monster.Stat , playerAttackRepeat);
            int monsterDamage = Util.CalculateDamage(monster.Stat, player.Stat, monsterAttackRepeat);

            Console.WriteLine("player의 공격! {0}의 데미지!, {1}번 공격했다! ", playerDamage, playerAttackRepeat);
            if (playerDamage >= monNowHp)
            {
                IsBattleEnd = true;
                IsEnemyDead = true;
                Console.WriteLine("플레이어의 공격이 몬스터를 잡았다!");
                player.GetGold(monster.Gold);
                player.GetExp(monster.Exp);
                Util.ReadyPlayer();
                return;
            }
            else 
            {                 
                monNowHp -= playerDamage;

            }
            Console.WriteLine("{0}의 공격! {1}의 데미지!, {2}번 공격했다! ", monster.Name, monsterDamage, monsterAttackRepeat);
            if (monsterDamage >= playerNowHp)
            {
                IsBattleEnd = true;
                IsPlayerDead = true;

                Console.WriteLine("몬스터의 공격이 플레이어를 쓰러뜨렸다...");
                Util.ReadyPlayer();
                return;
            }
            else
            {               
                playerNowHp -= monsterDamage;
            }
            Util.ReadyPlayer();

        }
        public override void Result()
        {
            if (IsBattleEnd)
            {
                Game.ChangeScene("map");
            }
        }

        public override void Enter()
        {
            IsFirst = true;
            IsBattleEnd = false;
            IsPlayerDead = false;
            IsEnemyDead = false;

            int rand = random.Next(0,1);
            
            MonsterBuilder monsterBuilder = BuilderList[rand];
            monsterBuilder.SetArt(monsterBuilder.Name)
                          .SetLevel(MapNumber);
            monster = monsterBuilder.Build();
            monNowHp = monster.Stat.Hp;
            playerNowHp = player.Stat.Hp;
        }

        public override void SceneDic()
        {
            Game.InsertDic("Battle", this);
        }
    }
}
