﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    public class MonsterBuilder
    {
        public string Name;
        private int level;
        private int gold;
        private int EXP;
        private int Exp;
        private int min;
        private int max;
        private Item item;
        private int rate;
        public int Min { get { return min; } }
        public int Max { get { return max; } }
        private Stat Setstat;
        private Stat stat;
        private string[] art = new string[] { };
        private Monsters.MonsterArt monsterASCII = new Monsters.MonsterArt();
        public static int count = 0;

        public MonsterBuilder()
        {
            count++;
        }

        public Monster Build()
        {
            Monster monster = new Monster(Name, level, gold, EXP, Setstat, art,item, min, max, rate);
            return monster;
        }

        public MonsterBuilder SetName(string _name)
        {
            Name = _name;
            return this;
        }

        public MonsterBuilder SetLevel(int _level)
        {
            level = _level;
            stat = (Setstat * level);
            EXP = (int)(Exp * (1+level*0.4));
            return this;
        }
        
        public MonsterBuilder SetGold(int _gold)
        {
            gold = _gold;
            return this;
        }

        public MonsterBuilder SetEXP(int _Exp)
        {
            Exp = _Exp;
            return this;
        }

        public MonsterBuilder SetStat(Stat _stat)
        {
            Setstat = _stat;
            return this;
        }

        public MonsterBuilder SetArt(string monsterName) 
        {
            try { art = monsterASCII.monsterDict[monsterName];
            }
            catch { art = new string[] { };
            }
            return this;
        }

        public MonsterBuilder SetMap(int _min, int _max)
        {
            min = _min;
            max = _max;
            return this;
        }

        public MonsterBuilder SetItem(Item _item, int _rate)
        {
            item = _item;
            rate = _rate;
            return this;
        }

    }
}
