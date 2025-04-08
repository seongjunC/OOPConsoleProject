using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    public class Map
    {
        private static List<string[]> MapList = new List<string[]>();
        public static List<string[]> mapList { get => MapList; }


        public string[] map;

        public Map(int MapNumber) 
        {
            if (mapList == null)
            {
                Init();
            }
            map = MapList[MapNumber-1];
        } 


        public void Init() {

            string[] mapData_1 = new string[] {
                "########",
                "#      #",
                "#      #",
                "#   ## #",
                "#   #  #",
                "########"
            };
            string[] mapData_2 = new string[] { };
            string[] mapData_3 = new string[] { };

            MapList.Add(mapData_1);
            MapList.Add(mapData_2);
            MapList.Add(mapData_3);
        }




    }
}
