using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateManagement_ver2{

    public enum E_ActionState {//アニメーション単位
        WAIT,
        WALK,
        ATTACK,
        CHARGE_ATTACK,
        JUMP,
        DUDGE,
        GUARD,
        DUSH,
        FALL,
        LANDING
    }
}
