using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    public struct Stat
    {

        private int HP;
        private int ATK;
        private int DEF;
        private int AGI;
        private int LUC;

        public int Hp { get => HP; }
        public int Atk { get => ATK; }
        public int Def { get => DEF; }
        public int Agi { get => AGI; }
        public int Luc { get => LUC; }

        public Stat(int _hp, int _atk, int _def, int _agi, int _luc)
        {
            HP = _hp;
            ATK = _atk;
            DEF = _def;
            AGI = _agi;
            LUC = _luc;
        }

        //플레이어의 스탯을 보여준다.
        public void PrintStat()
        {
            Console.WriteLine("현재의 스탯 상황");
            Console.Write("HP  : {0}  ", Hp);
            Console.Write("ATK : {0}  ", Atk);
            Console.Write("Def : {0}  ", Def);
            Console.Write("AGI : {0}  ", Agi);
            Console.WriteLine("LUC : {0}\n", Luc);
        }


        // player 객체의 statPointUse로 부터 접근
        // 받아온 스탯 값을 해당 종류의 스탯을 올린다.
        // 한번에 한 종류의 스탯만 받아올 수 있다.
        // 만약 해당 스탯이 없다면 오류를 출력한다.
        public void StatUp(int statPoint, ConsoleKey stat)
        {
            switch (stat)
            {
                case ConsoleKey.D1:
                    HP += statPoint * 10;
                    Console.WriteLine($"HP 스탯 {statPoint * 10} 증가");
                    break;
                case ConsoleKey.D2:
                    ATK += statPoint * 2;
                    Console.WriteLine($"ATK 스탯 {statPoint * 2} 증가");
                    break;
                case ConsoleKey.D3:
                    DEF += statPoint * 2;
                    Console.WriteLine($"DEF 스탯 {statPoint * 2} 증가");
                    break;
                case ConsoleKey.D4:
                    AGI += statPoint;
                    Console.WriteLine($"AGI 스탯 {statPoint} 증가");
                    break;
                case ConsoleKey.D5:
                    LUC += statPoint;
                    Console.WriteLine($"LUC 스탯 {statPoint} 증가");
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다 올릴 스탯을 골라주세요");
                    break;
            }
        }

        public static Stat operator *(Stat stat, int _level)
        {
            stat.HP = (int)(stat.Hp * (1+_level *0.3));
            stat.ATK = (int)(stat.ATK * (1 + _level * 0.3));
            stat.DEF = (int)(stat.DEF * (1 + _level * 0.3));
            return stat;
        }

        public int this[int index]
        {
            get {
                switch (index)
                {
                    case 0:
                        return HP;
                    case 1: 
                        return ATK;
                    case 2:
                        return DEF;                    
                    case 3:
                        return AGI;
                    case 4:
                        return LUC;
                    default:
                        Console.WriteLine("잘못된 스탯 값을 요청하였음");
                        return 0;
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        HP = value;
                        break;
                    case 1:
                        ATK = value;
                        break;
                    case 2:
                        DEF = value;
                        break;
                    case 3:
                        AGI = value;
                        break;
                    case 4:
                        DEF = value;
                        break;
                    default:
                        Console.WriteLine("잘못된 스탯 값을 요청하였음");
                        break;
                }
            }
        }

    }

    public struct Vector2
    {
        public int x;
        public int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator != (Vector2 a, Vector2 b) {
            return !(a.x == b.x && a.y == b.y); 
        }

    }
    public enum type
    {
        Weapon = 1, Armor = 2, AccessoryAgi = 3, AccessoryLuc = 4, none = 5
    }

    public struct Equipment
    {

        public Stat EquipStat;
        public type equiptype;
        public int value;
        public string statName;
        public Equipment(int i)
        {
            EquipStat = new Stat(0,0,0,0,0);
            equiptype = type.none;
            value = 0;
            statName = "none";
        }

        public Equipment(type T, int value)
        {
            EquipStat = new Stat(0, 0, 0, 0, 0);
            equiptype = T;
            this.value = value;
            switch (T)
            {
                case type.Weapon:
                    EquipStat[1] = value;
                    statName = "ATK";
                    break;
                case type.Armor:
                    EquipStat[2] = value;
                    statName = "DEF";
                    break;
                case type.AccessoryAgi:
                    EquipStat[3] = value;
                    statName = "AGI";
                    break;
                case type.AccessoryLuc:
                    EquipStat[4] = value;
                    statName = "LUC";
                    break;
                default:
                    statName = null;
                    break;
            }
        }

        public Stat Equip()
        {
            return EquipStat;
        }

        public void Print()
        {
            Console.WriteLine("이 장비의 타입 : {0}", equiptype);
            Console.WriteLine("이 장비의 능력치 {0} {1}", statName, this[(int)equiptype]);
        }

        public int this[int index]{
            get
            {
                if( index >= 1 && index <=4)
                {
                    return EquipStat[index];
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
