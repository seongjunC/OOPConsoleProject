using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    public class Map
    {
        // 맵을 저장할 리스트를 선언한다.
        private static List<string[]> MapList = new List<string[]>();
        public static List<string[]> mapList { get => MapList; }


        // bool[,] 변수로 이동 가능 여부를 체크한다.
        public bool[,] map;
        // 만약 맵에 다른 지점으로 이동가능한 포인트가 있다면 해당 위치를 저장한다.
        public List<int[]> movingPoint;

        public Map(int MapNumber)
        {
            // mapList에 맵이 생성이 안된 경우에 초기화를 진행한다.
            if (mapList.Count == 0)
            {
                Init();
            }
            // MapList내의 수직축 (string의 개수)를 구한다.
            int vertical = MapList[MapNumber - 1].Length;

            // MapList내의 string[]의 내부 길이를 구한다.
            // 일단 모든 맵은 내부 string의 길이는 같게 생성한다.
            int horizontal = MapList[MapNumber - 1][0].Length;

            // 위에서 구한 값을 바탕으로 bool[,] map의 길이를 먼저 정해준다. 
            map = new bool[vertical, horizontal];

            // mapList에 있는 현재 맵 객체의 string[] 값을 받는다. 
            string[] mapdetail = mapList[MapNumber - 1];


            for (int y = 0; y < vertical; y++)
            {
                for (int x = 0; x < horizontal; x++)
                {
                    // 만약 지금 위치의 값이 #이 아니면 true로 #이면 false로 지정한다.
                    map[y, x] = mapdetail[y][x] != '#';

                    // 만약 현재 위치의 값이 숫자(char형)이면 이는 다른 맵과의 연결 포인트이므로
                    // 해당 값을 movingPoint 리스트에 저장한다.
                    if (int.TryParse(mapdetail[y][x].ToString(), out int SceneNum))
                    {
                        movingPoint.Add(new int[3] { y,x,SceneNum});
                    }
                }
            }

            SetMap();
        } 


        // 맵 초기화
        // maplist는 스태틱 변수이므로 맵이 처음 실행될 때만 수행된다.
        public void Init() {

            string[] mapData_1 = new string[] {
                "########",
                "#      #",
                "#      2",
                "#   ## #",
                "#   #  #",
                "########"
            };

            string[] mapData_2 = new string[] {
                "########",
                "#      #",
                "1      3",
                "#   ## #",
                "#   #  #",
                "########"
            };

            string[] mapData_3 = new string[] {
                "########",
                "#      #",
                "2      #",
                "#   ## #",
                "#   #  #",
                "########"
            };

            MapList.Add(mapData_1);
            MapList.Add(mapData_2);
            MapList.Add(mapData_3);
        }

        // 맵을 출력한다.
        public void Print()
        {
            // 맵을 출력하기 위해 현재의 커서를 0,0으로 둔다. 
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    // 맵의 값이 true면 공백을 출력한다.
                    if (map[y,x] == true)
                    {
                        Console.Write(' ');
                    }
                    // 맵의 값이 false면 벽으로 #을 출력한다.
                    else
                    {
                        Console.Write('#');
                    }
                }
                Console.WriteLine();
            }
        }

        public void SetMap()
        {
            Game.Player.map = map;
        }



    }
}
