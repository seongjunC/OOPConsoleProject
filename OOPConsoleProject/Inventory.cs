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
        Player player = Game.Player;

        public Inventory() { 
            items = new List<Item>();
        }

        public Item TakeItem(int index)
        {
            Item item = items[index];
            items.RemoveAt(index);
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
                Console.WriteLine($"{selectindex + 1}. {item}");
            }
        }

        public void PrintItem(int index)
        {
            Console.Clear();
            items[index].PrintItem();
        }

        public void ProcessKey(ConsoleKey key, ConsoleKey index)
        {
            if ((int)key == 1)
            {
                player.Equip(items[(int)index]);
            }
            else if((int)key == 2){
                TakeItem((int)index);
                return;
            }
            else if((int)key == 3)
            {
                return;
            }
        }

    }
}
