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

        public int Hp { get => HP; set { HP = value; } }
        public int Atk { get => ATK; set { ATK = value; } }
        public int Def { get => DEF; set { DEF = value; } }
        public int Agi { get => AGI; set { AGI = value; } }
        public int Luc { get => LUC; set { LUC = value; } }

        public Stat(int _hp, int _atk, int _def, int _agi, int _luc)
        {
            HP = _hp;
            ATK = _atk;
            DEF = _def;
            AGI = _agi;
            LUC = _luc;
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
}
