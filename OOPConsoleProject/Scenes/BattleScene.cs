using System;
using System.Collections.Generic;

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
                .SetStat(new Stat(50, 7, 7, 10, 7));


            BuilderList.Add(orangeMushroomBuilder);

        }

        // TODO : 몬스터 구현 
        public override void Render()
        {
            //foreach (string str in monster.Art)
            //{
            //    Console.WriteLine(str);
            //}
            if (IsFirst)
            {
                Console.WriteLine("{0}이 나타났다.", monster.Name);
                Console.WriteLine("{0}의 레벨 : ", monster.Level);
                Console.SetCursorPosition(0, 0);
                PrintEachHp();
            }
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
            Console.SetCursorPosition(0, 0);
            int playerAttackRepeat = Util.CalculateRepeat(player.Stat, monster.Stat);
            int playerDamage = Util.CalculateDamage(player.Stat, monster.Stat, playerAttackRepeat, out bool IsCrit);

            if (playerDamage >= monNowHp)
            {
                IsBattleEnd = true;
                IsEnemyDead = true;
                monNowHp = 0;
                PrintEachHp();
                PrintAttckResult("플레이어", playerDamage, playerAttackRepeat, IsCrit);
                Console.WriteLine("플레이어의 공격이 몬스터를 잡았다!");
                Util.ReadyPlayer();
                player.GetGold(monster.Gold);
                player.GetExp(monster.Exp);
                return;
            }
            else
            {
                monNowHp -= playerDamage;
                PrintEachHp();
                PrintAttckResult("플레이어", playerDamage, playerAttackRepeat, IsCrit);
            }
            Util.ReadyPlayer();
            int monsterAttackRepeat = Util.CalculateRepeat(monster.Stat, monster.Stat);
            int monsterDamage = Util.CalculateDamage(monster.Stat, player.Stat, monsterAttackRepeat, out IsCrit);
            if (monsterDamage >= playerNowHp)
            {
                IsBattleEnd = true;
                IsPlayerDead = true;
                playerNowHp = 0;
                PrintEachHp();
                PrintAttckResult(monster.Name, monsterDamage, monsterAttackRepeat,IsCrit);
                Console.WriteLine("몬스터의 공격이 플레이어를 쓰러뜨렸다...");
                Util.ReadyPlayer();
                return;
            }
            else
            {
                playerNowHp -= monsterDamage;
                PrintEachHp();
                PrintAttckResult(monster.Name, monsterDamage, monsterAttackRepeat, IsCrit);
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

            int rand = random.Next(0, 1);

            MonsterBuilder monsterBuilder = BuilderList[rand];
            monsterBuilder.SetArt(monsterBuilder.Name)
                          .SetLevel(MapNumber);

            monster = monsterBuilder.Build();

            monNowHp = monster.Stat.Hp;
            playerNowHp = player.Stat.Hp;
        }

        public override void Exit()
        {
        }

        public override void SceneDic()
        {
            Game.InsertDic("Battle", this);
        }

        public void PrintEachHp()
        {
            Util.PrintHp("플레이어", playerNowHp, player.Stat);
            Util.PrintHp(monster.Name, monNowHp, monster.Stat);
        }

        public void PrintAttckResult(string name, int damage, int repeat, bool crit)
        {
            if(crit)
                {
                Console.WriteLine("{0}의 공격! Criticlal!!, {1}의 데미지!, {2}번 공격했다! ", name, damage, repeat);
            }
                else
            {
                Console.WriteLine("{0}의 공격! {1}의 데미지!, {2}번 공격했다! ", name, damage, repeat);
            }
        }
    }
}
