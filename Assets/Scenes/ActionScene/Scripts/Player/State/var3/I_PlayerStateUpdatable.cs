using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

namespace StateManagement_ver3{

    public interface I_PlayerStateUpdatable {

        void stateUpdate(S_StateData nextState);

        void actionEnter(E_ActionState nextState);

        void moveEnter(E_MoveState nextState);

        void subscribeFall (Action method);

        void subscribeLanding (Action method);

        void subscribeDamaged (Action method);    

        S_ActionConfig getActionConfig();
    }

    
}
