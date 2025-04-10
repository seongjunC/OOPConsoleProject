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

        private static List<int> LevelList = new List<int>();
        public static List<int> levelList { get => LevelList; }
        private int thismap; 
        

        // bool[,] 변수로 이동 가능 여부를 체크한다.
        public bool[,] map;
        // 만약 맵에 다른 지점으로 이동가능한 포인트가 있다면 해당 위치를 저장한다.
        public List<int[]> movingPoint = new List<int[]>();
        public Player player = Game.Player;


        public Map(int MapNumber)
        {
            // 맵 인스턴스가 생성될 때의 초기화를 진행
            if (mapList.Count == 0)
            {
                Init();
            }

            // 맵 번호가 바뀔 때 진행하는 초기화를 진행한다.
            MapInit(MapNumber);

        } 

        public void MapInit(int MapNumber)
        {
            thismap = MapNumber;

            // mapList에 있는 현재 맵 객체의 string[] 값을 받는다. 
            string[] mapdetail = mapList[MapNumber - 1];

            // MapList내의 수직축 (string의 개수)를 구한다.
            int vertical = mapdetail.Length;

            // MapList내의 string[]의 내부 길이를 구한다.
            // 일단 모든 맵은 내부 string의 길이는 같게 생성한다.
            int horizontal = mapdetail[0].Length;

            // 위에서 구한 값을 바탕으로 bool[,] map의 길이를 먼저 정해준다. 
            map = new bool[vertical, horizontal];


            for (int y = 0; y < vertical; y++)
            {
                for (int x = 0; x < horizontal; x++)
                {
                    // 만약 지금 위치의 값이 #이 아니면 true로 #이면 false로 지정한다.
                    map[y, x] = mapdetail[y][x] != '#';
                }
            }

            // 만들어진 맵을 플레이어에게 전달한다.
            SetMap();
        }


        // 맵 인스턴스가 생성될 때의 초기화
        public void Init() {

            string[] mapData_1 = new string[] {
                "########",
                "#      #",
                "#      #",
                "#   ## #",
                "#   # 2#",
                "########"
            };

            string[] mapData_2 = new string[] {
                "########",
                "#1     #",
                "#      #",
                "#   ## #",
                "#  3#  #",
                "########"
            };

            string[] mapData_3 = new string[] {
                "########",
                "#  2   #",
                "#      #",
                "#   ## #",
                "#   #  #",
                "########"
            };


            MapList.Add(mapData_1);
            MapList.Add(mapData_2);
            MapList.Add(mapData_3);

            LevelList.Add(1);
            LevelList.Add(3);
            LevelList.Add(6);

            // MapList에 있는 이동 지점들을 찾는다.
            int dataNum = 1;
            foreach (string[] mapData in MapList)
            {
                // 맵의 가로와 세로축 지정
                int verticalMap = mapData.Length;
                int horizontalMap = mapData[0].Length;

                for (int y = 0; y < verticalMap; y++)
                {
                    for (int x = 0; x < horizontalMap; x++)
                    {
                        // 만약 mapData[y][x]가 숫자라면 해당 지점의 숫자를 받아
                        // 현재 위치, x좌표, y좌표, 목적지 순으로 movingPoint에 넣는다.
                        if (int.TryParse(mapData[y][x].ToString(), out int SceneNum))
                        {
                            movingPoint.Add(new int[4] { dataNum, x, y, SceneNum });
                        }
                    }
                }
                dataNum++;

            }
        }

        // 맵을 출력한다.
        public void Print()
        {
            // 맵을 출력하기 위해 현재의 커서를 0,0으로 둔다. 
            Console.SetCursorPosition(0, 0);

            // 이동 지점들을 정리해두는 list를 선언한다.
            List<Vector2> moveVec = new List<Vector2>();
            // 해당 이동 지점의 목적지를 저장하는 list 
            List<int> moveTo = new List<int>();

            foreach (int[] move in movingPoint)
            {
                // 해당 맵에 존재하는 이동 지점일 경우
                if (move[0] == thismap)
                {
                    // moveVec에 Vector2 형태로 이동 지점의 위치를 저장한다.
                    moveVec.Add(new Vector2(move[1], move[2]));
                    // moveTo에 목적지의 위치를 저장한다.
                    moveTo.Add(move[3]);
                }
            }

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    // 맵의 값이 true면 아래를 진행한다.
                    if (map[y,x] == true)
                    {
                        // 위에서 moveVec에 저장한 값중 현재의 좌표가 있는지 확인한다.
                        int to = moveVec.IndexOf(new Vector2(x, y));
                        // 없다면 공백을 출력한다.
                        if ( to == -1)
                        {
                            Console.Write(' ');
                        }
                        // 있다면 moveTo에 저장된 해당 인덱스의 값을 출력한다.
                        else
                        {
                            Console.Write(moveTo[to]);
                        }
                    }
                    // 맵의 값이 false면 벽으로 #을 출력한다.
                    else
                    {
                        Console.Write('#');
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n플레이어의 남은 BP : {0}", player.bp);
            player.Inventory.PrintItems();
            player.Print();

        }

        //플레이어의 맵을 현재 맵으로 세팅한다.
        public void SetMap()
        {
            player.map = map;
        }

        // 플레이어의 현재 위치가 다른 포인트로의 이동 지점인지 판단한다.
        // 현재 맵 번호를 인자로 받는다.
        public void Moving(ref int MapNumber)
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
                            // 해당 지점이 연결되어 있을 경우
                            if(moved[0] == moving[3] && moved[3] == moving[0])
                            {
                                // 맵 번호를 해당 맵으로 세팅한다.
                                // ref이므로 내부 값이 변하면 원래의 값도 영향을 준다
                                MapNumber = moved[0];
                                // 플레이어의 위치를 이동할 곳의 이동 지점 좌표로 설정한다.
                                player.position = new Vector2(moved[1], moved[2]);
                                MapInit(MapNumber);
                                return;
                            }
                        }
                        // 만약 연결된 지점을 찾지 못하면 에러를 발생시킨다.
                        Console.WriteLine("에러: 이동할 맵을 찾지 못했음");
                    }
                }
            }
        }


    }
}
