using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyInputSystems {
    public enum E_InputType {
        //Action
        WALK_LEFT,
        WALK_LEFT_PERFORMED,
        WALK_LEFT_CANCELED,

        WALK_RIGHT,
        WALK_RIGHT_PERFORMED,
        WALK_RIGHT_CANCELED,

        ATTACK,

        CHARGE_ATTACK,
        CHARGE_ATTACK_PEFORMED,
        CHARGE_ATTACK_CANCELED,

        GUARD,
        GUARD_PEFORMED,
        GUARD_CANCELED,

        JUMP,
        LITTLE_JUMP,        
        DUSH,
        POUSE,
        CAMERA_CHANGE,

        //Title

        //Camera
        CAMERA_MOVE_UP,
        CAMERA_MOVE_DOWN
    }
}
