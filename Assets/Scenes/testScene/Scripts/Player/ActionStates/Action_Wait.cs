using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class Action_Wait: ActionState{
    public Action_Wait () {
        //遷移を表すマップの作成
        
    }

    override public E_ActionState checkInput(){
        E_ActionState nextState = E_ActionState.WAIT;

        if (isInputStandBy){
            //入力確認処理
            var inputData = inputManager.getInputData(6.0f * 1.0f / 60.0f);

            //入力がなかった場合
            if(inputData.Count < 1){
                return nextState;
            }

            foreach(var data in inputData) {
                switch (data.type){
                    case E_InputType.WALK_LEFT_PERFORMED:
                        playerDirection = PlayerDirection.LEFT;
                        isWalkLeft = true;
                        nextState = E_ActionState.WALK;
                        break;

                    case E_InputType.WALK_RIGHT_PERFORMED:
                        playerDirection = PlayerDirection.RIGHT;
                        isWalkRight = true;
                        nextState = E_ActionState.WALK;
                        break;
                        
                    case E_InputType.JUMP:
                        nextState = E_ActionState.JUMP;
                        getPlayer.toAir();

                        break;
                
                    case E_InputType.ATTACK:
                        nextState = E_ActionState.ATTACK;
                        break;
                        
                /*
                    case E_InputType.CHARGE_ATTACK_PEFORMED:
                        nextState = E_ActionState.CHARGE_ATTACK;
                        break;

                    case E_InputType.DUSH:
                        nextState = E_ActionState.DUSH;
                        break;

                    case E_InputType.GUARD_PEFORMED:
                        nextState = E_ActionState.GUARD;
                        break;
                */
                    default:
                        break;
                }
            }          
        }

        return nextState;  
    }

    override public E_ActionState stateUpdate (){
        E_ActionState nextState = E_ActionState.WAIT;
        //Debug.Log("WAIT");
        return nextState;
    }
}
