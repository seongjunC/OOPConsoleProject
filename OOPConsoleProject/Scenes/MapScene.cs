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

        public override void Render()
        {
            field.Print();
        }


        public override void Input()
        {

        }


        public override void Result()
        {
            Game.gameOver = true;
        }

        public override void SceneDic()
        {
            Game.InsertDic("map", this);
        }

        public override void Update()
        {

        }

        public override void Enter()
        {
            MapFactory mapFact = new MapFactory();

            field = mapFact.Create(MapNumber);
        }

        public override void Exit()
        {
            field.Moving(ref MapNumber);
        }
    }
}
