using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject.Scenes
{
    public class MapScene : BaseScene
    {
        public static int MapNumber = 1;
        public Map field;
        private ConsoleKey input;
        private Random random = new Random();
        private bool IsBattle;
        private int monsterRate;

        public MapScene()
        {
            MapFactory mapFact = new MapFactory();

            field = mapFact.Create(MapNumber);
            monsterRate = 0;
        }

        public override void Render()
        {
            field.Print();
        }


        public override void Input()
        {
            input = Console.ReadKey(true).Key;
        }


        public override void Result()
        {
            if (IsBattle) Game.ChangeScene("Battle");
        }

        public override void SceneDic()
        {
            Game.InsertDic("map", this);
        }

        public override void Update()
        {
            Game.Player.Action(input);
            field.Moving(ref MapNumber);
            if (random.Next(0, 12) < monsterRate)
            {
                IsBattle = true;
                monsterRate = 0;
            }
            else 
            { 
                IsBattle = false; 
                monsterRate += 1;
            }

        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
            field.Moving(ref MapNumber);
        }
    }
}
