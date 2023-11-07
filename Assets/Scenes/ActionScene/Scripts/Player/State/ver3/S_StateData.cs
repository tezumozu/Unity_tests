using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateManagement_ver3{
    public struct S_StateData {
        public E_ActionState actionState;
        public E_MoveState moveState;

        public bool isRanning;

        public bool isAir;
        public E_PlayerDirection playerDirection;
    }
}

