using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    public class Player
    {
        public bool[,] map;
        private int level;
        public int Level { get { return level; } }

        private int gold;
        public int Gold { get { return gold; } }

        private int EXP;
        public int Exp { get => EXP; }

        private int ExpBar;
        private int statPoint;
        private int statPerLevel;

        private Stat stat;
        public Stat Stat {  get { return stat; } }

        private Vector2 Position;
        public Vector2 position { get => Position; set => Position = value; }

        private delegate void ExpManager();
        event ExpManager LevelManager;

        public Player() {
            // 플레이어의 스탯의 초기 값 설정
            stat = new Stat(100, 10, 10, 10, 10);
            // 초기 레벨은 1
            level = 1;
            // 추후 레벨업시 스탯을 올리기위해 필요한 포인트, 초기 값은 0 
            statPoint = 0;
            // 레벨 당 주는 스탯의 포인트, 추후 아이템 등으로 조정할 여지가 있음
            statPerLevel = 4;
            // 경험치 량, 몬스터를 잡을 시 획득
            EXP = 0;
            // 경험치 바, 경험치가 가득차면 레벨 업을 한다.
            ExpBar = 10;
            // 골드, 물건을 구매하는 데 사용
            gold = 0;
            // 플레이어의 위치를 Vector2 변수로 1,1로 초기 설정
            position = new Vector2(1, 1);
            // event 객체에 level과 관련된 함수들을 체이닝
            LevelManager += LevelUp;
            LevelManager += StatPointUse;
        }


        public void Print()
        {
            Console.SetCursorPosition(position.x, position.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write('P');
            Console.ResetColor();
        }

        // 플레이어 객체의 움직임, input에서 consoleKey를 받아서 실행된다.
        public void Action(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:    
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    Move(key);
                    break;
            }
        }

        // Exp를 얻을 때 실행할 함수
        // 만약 현재 경험치가 경험치 바를 넘긴다면 levelManager를 실행한다.
        public void GetExp(int expAmount)
        {    
            EXP += expAmount;
            if (EXP >= ExpBar)
            {
                LevelManager();
            }
        }

        // level이 올랐을 때 실행하는 함수
        // private로 외부에서 선언할 수 없고
        // GetExp를 통해서 레벨이 올랐을 때 levelmaneger를 통해서만 사용가능하다.
        // 추후 다른 수단으로 level이 오르는 방법이 생길 경우
        // 추가 메서드를 통해 levelManager를 실행
        private void LevelUp()
        {
            // EXP가 Exp Bar만큼 찬 경우
            if (EXP >= ExpBar)
            {
                // level을 1 올려준다.
                level += 1;
                // statPoint를 statPerLevel만큼 올려준다.
                statPoint += statPerLevel;
                // level이 올랐으므로 해당 양만큼 EXP에서 빼 준다.
                EXP -= ExpBar;
                // ExpBar의 경험치량을 지수함수 적으로 올려준다.
                ExpBar += ((int)Math.Pow(3, Math.Sqrt(Level) / 2));
                // 재귀함수로 ExpBar보다 EXP가 적을때 까지 실행한다.
                LevelUp();
            }
            // Exp가 이제 Exp바보다 작을 때 return하여 호출 스택을 내린다.
            return;
        }

        // StatPoint를 사용해서 스탯을 올린다.
        // 현재는 levelManager를 통해서 level이 오르면 자동 실행되지만
        // TODO : 옵션 메뉴를 만들어서 스탯창을 연다든지 설정을 통해 
        // level이 오를 때 자동으로 스탯 분배 창이 켜질 지를 선택하게 구현할 수 있다. 
        private void StatPointUse() { 
            // statPoint가 0보다 클 경우 (작으면 stat을 분배할 수 없으므로)
            while(statPoint > 0)
            {
                Console.SetCursorPosition(0, map.GetLength(0) + 2);
                Console.WriteLine("어떤 스탯을 올리시겠습니까?");
                Console.WriteLine("스탯 올리기를 종료하시려면 6키를 눌러주세요.");
                // stat 구조체에서 플레이어의 스탯을 보여준다.
                stat.PrintStat();
                Console.WriteLine("HP = 1, ATK = 2, DEF = 3, AGI = 4, LUC = 5");
                // 플레이어가 어떤 스탯을 올릴 지 선택한 결과를 저장한다.
                ConsoleKey statName = Console.ReadKey(true).Key;
                //  6번 키를 누르면 statPoint 사용을 중지한다.
                if (statName == ConsoleKey.D6) break;
                Console.WriteLine("스탯 포인트를 얼마나 올리시겠습니까?");
                Console.WriteLine("현재의 스탯 포인트 : {0}", statPoint);

                // 사용자에게 투자할 스탯 포인트를 한줄로 입력받아서
                // 만약 int 값으로 제대로 입력 받았다면 실행한다.
                if(int.TryParse(Console.ReadLine(), out int statAmount))
                {
                     // 남은 스탯 포인트가 입력받은 양 이상일 경우
                     if(statPoint >= statAmount)
                     {
                        // Stat 구조체의 메서드인 StatUP을 실행한다.
                        // 이후는 stat 구조체에서 실행
                        stat.StatUp(statAmount, statName);
                     }
                     // 만약 스탯포인트가 입력받은 값보다 적을 경우 에러 메시지 출력
                     else Console.WriteLine("스탯 포인트를 초과하여 입력하였습니다. 다시 입력해주세요.");

                }
                // 만약 숫자가 아닌 값을 입력했을 경우 오류 메시지 출력
                else Console.WriteLine("유효하지 않은 입력입니다. 숫자만 입력해주세요.");
            }
        }

        public void Move(ConsoleKey input)
        {
            Vector2 targetPos = Position;

            switch (input) {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    targetPos.y--;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    targetPos.y++;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    targetPos.x--;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    targetPos.x++;
                    break;
            }

            if (map[targetPos.y, targetPos.x] == true)
            {
                position = targetPos;
            }

        }


    }
}
