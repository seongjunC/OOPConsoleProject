using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    public class Item
    {
        public string name;
        public int value;
        public Equipment region;

        public Item() { }

        public Item(string _name, int _value, type T) {
            name = _name;
            value = _value;
            region = new Equipment(T, _value);
        }

        public void PrintItem()
        {
            Console.WriteLine("이름 : {0}",name);
            region.Print();
        }


    }
}
