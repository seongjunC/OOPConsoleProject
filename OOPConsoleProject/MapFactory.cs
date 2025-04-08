using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    // MapFactory, Map이 필요한 곳에서 맵 넘버를 받아 해당 넘버의 맵을 출력해준다.
    public class MapFactory
    {
        public Map Create(int MapNum)
        {
            // MapNum에 따라 Map 객체를 생성한다.
            switch (MapNum)
            {
                case 1:
                    return new Map(1);
                case 2:
                    return new Map(2);
                case 3:
                    return new Map(3);
                // 만일 맵 클래스에 해당 num이 없다면 null을 반환한다.
                default:
                    Console.WriteLine("해당 맵이 없습니다.");
                    return null;
            }
        }
    }
}
