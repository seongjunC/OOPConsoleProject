using System;
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
        private Stat stat;
        private string[] art = new string[] { };
        private Monsters.MonsterArt monsterASCII = new Monsters.MonsterArt();

        public Monster Build()
        {
            Monster monster = new Monster(Name, level, gold, EXP, stat, art);
            return monster;
        }

        public MonsterBuilder SetName(string _name)
        {
            this.Name = _name;
            return this;
        }

        public MonsterBuilder SetLevel(int _level)
        {
            this.level = _level;
            this.stat = (stat * level);
            return this;
        }
        
        public MonsterBuilder SetGold(int _gold)
        {
            this.gold = _gold;
            return this;
        }

        public MonsterBuilder SetEXP(int _Exp)
        {
            this.EXP = _Exp;
            return this;
        }

        public MonsterBuilder SetStat(Stat _stat)
        {
            this.stat = _stat;
            return this;
        }

        public MonsterBuilder SetArt(string monsterName) 
        {
            try { this.art = monsterASCII.monsterDict[monsterName];
            }
            catch { this.art = new string[] { };
            }
            return this;
        }

    }
}
