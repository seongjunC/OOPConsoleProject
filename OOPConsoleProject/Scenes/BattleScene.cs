﻿using System;
using System.Collections.Generic;

namespace OOPConsoleProject.Scenes
{
    public class BattleScene : BaseScene
    {
        bool IsFirst;
        bool IsBattleEnd;
        Monster monster;
        List<MonsterBuilder> BuilderList = new List<MonsterBuilder>();
        int monNowHp;
        int playerNowHp;
        Player player = Game.Player;
        Random random = new Random();
        int MapNumber;

        public BattleScene()
        {
            Item sword = new Item("검", 5, type.Weapon);

            MonsterBuilder orangeMushroomBuilder = new MonsterBuilder();
            orangeMushroomBuilder
                .SetName("주황 버섯")
                .SetGold(100)
                .SetEXP(15)
                .SetStat(new Stat(50, 7, 7, 10, 7))
                .SetMap(1,2)
                .SetItem(sword, 20);

            Item ribbon = new Item("리본", 10, type.Armor);

            MonsterBuilder ribbonPigBuilder = new MonsterBuilder();
            ribbonPigBuilder
                .SetName("리본 돼지")
                .SetGold(80)
                .SetEXP(20)
                .SetStat(new Stat(80, 6, 6, 8, 10))
                .SetMap(2,3)
                .SetItem(ribbon, 10);

            Item tail = new Item("늑대 꼬리", 10, type.AccessoryAgi );

            MonsterBuilder wolfBuilder = new MonsterBuilder();
            wolfBuilder
                .SetName("늑대")
                .SetGold(50)
                .SetEXP(30)
                .SetStat(new Stat(100, 10, 3, 12, 4))
                .SetItem(tail, 5);

            MonsterBuilder slimeBuilder = new MonsterBuilder();
            slimeBuilder
                .SetName("슬라임")
                .SetGold(120)
                .SetEXP(25)
                .SetStat(new Stat(150, 10, 7, 15, 12))
                .SetMap(2,3);


            MonsterBuilder bearBuilder = new MonsterBuilder();
            bearBuilder
                .SetName("곰")
                .SetGold(200)
                .SetEXP(50)
                .SetStat(new Stat(220, 15, 10, 20, 5))
                .SetMap(3, 3);



            BuilderList.Add(orangeMushroomBuilder);
            BuilderList.Add(ribbonPigBuilder);
            BuilderList.Add(wolfBuilder);
            BuilderList.Add(slimeBuilder);
            BuilderList.Add(bearBuilder);
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
                monNowHp = 0;
                PrintEachHp();
                PrintAttckResult("플레이어", playerDamage, playerAttackRepeat, IsCrit);
                Console.WriteLine("플레이어의 공격이 몬스터를 잡았다!");
                Util.ReadyPlayer();
                player.GetGold(monster.Gold);
                player.GetExp(monster.Exp);
                player.UseBp(1);
                if (Util.ItemDropCalculate(player.Stat, monster.ItemRate))
                {
                    Console.Clear();
                    Console.WriteLine("몬스터에게 장비가 떨어졌다!");
                    Console.WriteLine("습득하시겠습니까? (1:Y / else:N)");
                    ConsoleKey choice = Console.ReadKey(true).Key;
                    if ((int)choice == 49)
                    {
                        player.AddInventory(monster.Item);
                    }
                }
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
                playerNowHp = 0;
                PrintEachHp();
                PrintAttckResult(monster.Name, monsterDamage, monsterAttackRepeat,IsCrit);
                Console.WriteLine("몬스터의 공격이 플레이어를 쓰러뜨렸다...");
                Util.ReadyPlayer();
                player.UseBp(3);
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
                if(player.bp == 0)
                {
                    Game.gameOver = true;
                }
                Game.ChangeScene("map");
            }
        }

        public override void Enter()
        {
            IsFirst = true;
            IsBattleEnd = false;
            bool IsHere = true;
            MapNumber = MapScene.MapNumber;

            while (IsHere)
            {
                int rand = random.Next(0, MonsterBuilder.count);
                MonsterBuilder monsterBuilder = BuilderList[rand];

                if (monsterBuilder.Max == 0 || MapNumber <= monsterBuilder.Max && monsterBuilder.Min <= MapNumber )
                {
                    monsterBuilder.SetLevel(Map.levelList[MapNumber - 1]);

                    monster = monsterBuilder.Build();
                    IsHere = false;
                }
            }



            Util.ReadyPlayer();

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
                Console.WriteLine("{0}의 공격! 크리티컬!!, {1}의 데미지!, {2}번 공격했다! ", name, damage, repeat);
            }
                else
            {
                Console.WriteLine("{0}의 공격! {1}의 데미지!, {2}번 공격했다! ", name, damage, repeat);
            }
        }


    }
}
