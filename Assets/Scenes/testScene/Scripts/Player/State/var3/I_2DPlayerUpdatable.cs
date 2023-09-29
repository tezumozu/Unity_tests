using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace StateManagement_ver3{

    public delegate void SubscrivableMethod();

    public interface I_2DPlayerUpdatable {

        void playerUpdate ();

        void checkLanding();

        void stateUpdate(S_StateData nextState);

        void actionEnter(E_ActionState nextState);

        void moveEnter(E_MoveState nextState);

        void playerInit();

        void subscribeFall (SubscrivableMethod method);

        void subscribeLanding (SubscrivableMethod method);

        void subscribeDamaged (SubscrivableMethod method);    

        S_ActionConfig getActionConfig();
    }
}
