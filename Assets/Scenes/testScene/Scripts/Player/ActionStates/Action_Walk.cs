using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class Action_Walk : MovableActionState {
    
    public Action_Walk () {
        
    }

    override public E_ActionState checkInput(){
        E_ActionState nextState = E_ActionState.WALK;

        if (isInputStandBy){
            //入力確認処理
            var inputData = inputManager.getInputData(6.0f * 1.0f / 60.0f);

            if(inputData.Count < 1){
                return nextState;
            }

            foreach(var data in inputData) {
                switch (data.type){
                    case E_InputType.WALK_LEFT_PERFORMED :
                        playerDirection = PlayerDirection.LEFT;
                        MovableActionState.moveToActive();
                        nextState = E_ActionState.WALK;
                        break;

                    case E_InputType.WALK_RIGHT_PERFORMED:
                        playerDirection = PlayerDirection.RIGHT;
                        MovableActionState.moveToActive();
                        nextState = E_ActionState.WALK;
                        break;
                        
                    case E_InputType.JUMP:
                        nextState = E_ActionState.WALK;
                        break;
                }
            }

            return nextState;            
        }

        return E_ActionState.WAIT;

    }

    override public E_ActionState stateUpdate (){
        E_ActionState nextState = E_ActionState.WAIT;
        Debug.Log("WALK");
        return nextState;

    }
}
