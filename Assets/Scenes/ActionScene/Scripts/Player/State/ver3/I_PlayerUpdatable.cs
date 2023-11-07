using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace StateManagement_ver3{

    public interface I_PlayerUpdatable : I_PlayerStateUpdatable {

        void playerUpdate ();

        void checkLanding();

        void playerInit();
    }
}
