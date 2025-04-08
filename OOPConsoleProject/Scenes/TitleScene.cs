using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject.Scenes
{
    public class TitleScene : BaseScene
    {
        

        public override void Render()
        {
            Console.WriteLine("************************");
            Console.WriteLine("*      Auto RPG        *");
            Console.WriteLine("************************");
            Console.WriteLine("\n\n    아무키나 눌러 게임 시작하기     ");
            Console.WriteLine("");
        }
        public override void Input()
        {
            Console.ReadKey(true);
        }
        public override void Update()
        {
            
        }

        public override void Result()
        {
            Game.ChangeScene("map");
        }

        public override void SceneDic()
        {
            Game.InsertDic("title", this);
        }
    }
}
