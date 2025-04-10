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
            Console.WriteLine("방향키를 눌러 움직일 수 있습니다.");
            Console.WriteLine("숫자 키를 눌러 아이템의 정보를 볼 수 있습니다.");
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

        // Game의 딕셔너리에 title을 등록한다.
        public override void SceneDic()
        {
            Game.InsertDic("title", this);
        }
    }
}
