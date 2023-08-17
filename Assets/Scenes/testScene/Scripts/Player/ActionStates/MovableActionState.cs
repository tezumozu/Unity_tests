using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class MovableActionState : ActionState {
    protected static bool isMove;

    // Update is called once per frame
    public MovableActionState(){
        isMove = false;
    }

    public static void moveToActive(){
        isMove = true;
    }
}
