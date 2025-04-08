﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject.Scenes
{
    public class MapScene : BaseScene
    {
        public static int MapNumber = 1;
        public override void Render()
        {
            MapFactory map = new MapFactory();

            map.Create(MapNumber);
        }


        public override void Input()
        {
            throw new NotImplementedException();
        }


        public override void Result()
        {
            throw new NotImplementedException();
        }

        public override void SceneDic()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
