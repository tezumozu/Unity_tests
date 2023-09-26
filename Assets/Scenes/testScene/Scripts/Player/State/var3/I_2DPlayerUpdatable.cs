using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace StateManagement_ver3{

    public delegate void SubscrivableMethod();

    public interface I_2DPlayerUpdatable {

        void playerUpdate ();

        void stateUpdate(S_StateData nextState);

        void playerInit();

        void subscribeFall (SubscrivableMethod method);

        void subscribeLanding (SubscrivableMethod method);

        void subscribeDamaged (SubscrivableMethod method);    
    }
}
