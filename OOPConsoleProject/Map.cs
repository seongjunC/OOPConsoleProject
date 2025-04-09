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
        public List<int[]> movingPoint = new List<int[]>();
        public Player player = Game.Player;

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
                        movingPoint.Add(new int[4] {MapNumber, x,y,SceneNum});
                    }
                }
            }

            // 만들어진 맵을 플레이어에게 전달한다.
            SetMap(map);
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
            player.Print();
        }

        //플레이어의 맵을 현재 맵으로 세팅한다.
        public void SetMap(bool[,] map)
        {
            player.map = map;
        }

        // 플레이어의 현재 위치가 다른 포인트로의 이동 지점인지 판단한다.
        // 현재 맵 번호를 받고 다음 맵 번호를 전달한다.
        public bool Ismoving(ref int MapNumber, out int SceneNum)
        {
            foreach (int[] moving in movingPoint)
            {
                // 만약 현재 맵의 이동 지점인 경우
                if (moving[0] == MapNumber)
                {
                    // 플레이어의 위치가 해당 이동 지점의 x,y  값인 경우
                    if(player.position == new Vector2(moving[1], moving[2]))
                    {
                        foreach (int[] moved in movingPoint)
                        {
                            // 다시 이동 지점들의 좌표를 순회하여
                            // 이동할 맵 번호가 해당 맵이며
                            // //
                            if(moved[0] == moving[3] && moved[3] == moving[0])
                            {
                                MapNumber = moved[0];
                                player.position = new Vector2(moved[1], moved[2]);
                                SceneNum = MapNumber;
                                return true;
                            }
                        }
                        Console.WriteLine("에러: 이동할 맵을 찾지 못했음");
                    }
                }
            }
            SceneNum = MapNumber;
            return false;
        }


    }
}
