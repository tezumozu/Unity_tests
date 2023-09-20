using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace StateManagement_ver3{
    public interface I_2DPlayerUpdatable {
        public static Subject<Unit>  isDamaged;
        public static Subject<Unit>  isLanding;
        public static Subject<Unit>  isFall;

        void playerUpdate ();

        void stateEnter (E_ActionState state);
        
        void moveEnter (E_MoveState state,E_PlayerDirection direction);
    }
}
