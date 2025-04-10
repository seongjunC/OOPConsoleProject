using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    public class Monster
    {
        public string Name { get; set; }
        private int level;
        public int Level { get { return level; } }

        private int gold;
        public int Gold { get { return gold; } }

        private int EXP;
        public int Exp { get => EXP; }

        private string[] art;
        public string[] Art { get { return art; } }

        private Stat stat;
        public Stat Stat { get { return stat; } }

        private int min;
        public int Min { get { return min; } }

        private int max;
        public int Max { get { return max; } }

        private Item item;
        public Item Item { get { return item; } }

        private int itemRate;
        public int ItemRate { get { return itemRate; } }

        public Item _item = new Item();

        public Monster(string _name, int _level, int _gold, int _Exp, Stat _stat, 
            string[] art, Item _item, int _min = 0, int _max = 0, int _itemRate = 0)
        {
            if (_item == null) { _item = this._item; }
            this.Name = _name;
            this.level = _level;
            this.gold = _gold;
            this.EXP = _Exp;
            this.stat = _stat;
            this.art = art;
            min = _min;
            max = _max;
            item = _item;
            itemRate = _itemRate;
        }
    }
}
