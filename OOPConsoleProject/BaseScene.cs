using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleProject
{
    public abstract class BaseScene
    {

        // 씬이 실행될 때 기본으로 출력될 화면
        public abstract void Render();

        // 콘솔 기반이므로 사용자에게 입력을 받는다.

        public abstract void Input();

        // 사용자에게 받은 입력을 기준으로 화면이 업데이트 되어야 할 때
        public abstract void Update();

        // 해당 씬이 끝났을 때 처리되는 행동
        public abstract void Result();

        // 씬의 입장 시에 필요한 행동이 있으면 진행한다. (초기 설정) 
        // 필요하다면 하위 씬에서 구현한다.
        public virtual void Enter() { }

        // 씬의 퇴장 시에 필요한 행동이 있으면 진행한다. (초기화 등)
        // 필요하다면 하위 씬에서 구현한다.
        public virtual void Exit() { }

        // Game의 딕셔너리에 해당 씬의 title을 등록한다.
        public abstract void SceneDic();

    }
}
