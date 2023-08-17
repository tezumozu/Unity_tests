using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ChageAttack : MovableActionState {
    
    public Action_ChageAttack () {
        //遷移を表すマップの作成
        
    }

    override public E_ActionState checkInput(){
        E_ActionState nextState = E_ActionState.WAIT;
        return nextState;

    }

    override public E_ActionState stateUpdate (){
        E_ActionState nextState = E_ActionState.WAIT;
        return nextState;
    }
}
