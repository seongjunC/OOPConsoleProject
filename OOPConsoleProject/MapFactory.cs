using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    public class MapFactory
    {
        public Map Create(int SceneNum)
        {
            switch (SceneNum)
            {
                case 1:
                    return new Map(1);
                case 2:
                    return new Map(2);
                case 3:
                    return new Map(3);
                default:
                    Console.WriteLine("해당 맵이 없습니다.");
                    return null;
            }
        }
    }
}
