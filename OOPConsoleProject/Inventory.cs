using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    public class Inventory : List<Item>
    {
        private int selectindex;
        List<Item> items;

        public Inventory() { 
            items = new List<Item>();
        }

        public void Add(Item item)
        {
            items.Add(item);
            
        }

        public Item TakeItem(int index)
        {
            Item item = items[index-49];
            items.RemoveAt(index-49);
            return item;
        }

        public Item InfoItem(int index)
        {
            Item item = items[index];
            return item;
        }

        public void InsertItem(Item item)
        {
            if (items.Count == 0)
            {
                items.Add(item);
            }
            else
            {
                selectindex = 0;
                foreach(Item inItem in items)
                {
                    if(inItem.value < item.value)
                    {
                        items.Insert(selectindex, item);
                        return;
                    }
                    selectindex++;
                }
                items.Add(item);
                return;
            }
        }

        public void PrintItems()
        {
            selectindex = 0;
            foreach (Item item in items)
            {
                Console.WriteLine($"{selectindex + 1}. {item.name}");
            }
        }

        public bool PrintItem(ConsoleKey index)
        {
            Console.Clear();
            if (items.Count == 0) return false;
            items[(int)index-49].PrintItem();
            return true;
        }

        public void ProcessKey(ConsoleKey key, ConsoleKey index)
        {
            Player player = Game.Player;
            if ((int)key == 49)
            {
                Console.Clear();
                player.Equip(items[(int)index-49]);
                TakeItem((int)index);
            }
            else if((int)key == 50){
                TakeItem((int)index);
                return;
            }
            else if((int)key == 51)
            {
                return;
            }
        }

    }
}
