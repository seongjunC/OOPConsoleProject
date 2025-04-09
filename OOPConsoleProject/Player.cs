using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    public class Player
    {
        public bool[,] map;
        public int Level;
        public int Gold;
        private int EXP;
        private int ExpBar;
        public int statPoint;
        public int statPerLevel;
        private Stat stat;
        public Stat Stat {  get { return stat; } }
        public int Exp { get => EXP; }
        private Vector2 Position;
        public Vector2 position {get => Position; set => Position = value; }
        delegate void ExpManager();
        event ExpManager LevelManager;

        public Player() {
            stat = new Stat(100, 10, 10, 10, 10);
            Level = 1;
            statPoint = 0;
            statPerLevel = 4;
            EXP = 0;
            ExpBar = 10;
            Gold = 0;
            position = new Vector2(1, 1);
            LevelManager += LevelUp;
            LevelManager += StatPointUse;
        }


        public void Print()
        {
            Console.SetCursorPosition(position.x, position.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write('P');
            Console.ResetColor();
        }

        public void Action(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:    
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    Move(key);
                    break;
            }
        }

        private void GetExp(int expAmount)
        {    
            EXP += expAmount;
            if (EXP >= ExpBar)
            {
                LevelManager();
            }
        }

        private void LevelUp()
        {
            if(EXP > ExpBar)
            {
                Level += 1;
                statPoint += statPerLevel;
                EXP -= ExpBar;
                ExpBar += ( (int)Math.Pow(3, Math.Sqrt(Level)/2) );
                LevelUp();
            }
            return;
        }

        private void StatPointUse() { 
            while(statPoint > 0)
            {
                Console.WriteLine("어떤 스탯을 올리시겠습니까?");
                Console.WriteLine("스탯 올리기를 종료하시려면 6키를 눌러주세요.");
                stat.PrintStat();
                Console.WriteLine("HP = 1, ATK = 2, DEF = 3, AGI = 4, LUC = 5");
                ConsoleKey statName = Console.ReadKey(true).Key;
                if (statName == ConsoleKey.D6) break;
                Console.WriteLine("스탯 포인트를 얼마나 올리시겠습니까?");
                Console.WriteLine("현재의 스탯 포인트 : {0}", statPoint);

                if(int.TryParse(Console.ReadLine(), out int statAmount))
                {
                     if(statPoint >= statAmount)
                    {
                        stat.StatUp(statAmount, statName);
                    }
                    else Console.WriteLine("스탯 포인트를 초과하여 입력하였습니다. 다시 입력해주세요.");
                }
                else Console.WriteLine("유효하지 않은 입력입니다. 숫자만 입력해주세요.");
            }
        }

        public void Move(ConsoleKey input)
        {
            Vector2 targetPos = Position;

            switch (input) {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    targetPos.y--;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    targetPos.y++;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    targetPos.x--;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    targetPos.x++;
                    break;
            }

            if (map[targetPos.y, targetPos.x] == true)
            {
                position = targetPos;
            }

        }


    }
}
