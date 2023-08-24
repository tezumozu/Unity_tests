using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class Action_Fall : ActionState {
    public Action_Fall () {
    }

    override public E_ActionState checkInput(){
        E_ActionState nextState = E_ActionState.FALL;

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
                        isWalkLeft = true;
                        break;


                    case E_InputType.WALK_LEFT_CANCELED :
                        isWalkRight = false;
    
                        //右が同時押しされている場合
                        if(isWalkRight){
                            playerDirection = PlayerDirection.RIGHT;
                        }
                        break;


                    case E_InputType.WALK_RIGHT_PERFORMED:
                        playerDirection = PlayerDirection.RIGHT;
                        isWalkRight = true;
                        break;
                    
                    case E_InputType.ATTACK:
                        nextState = E_ActionState.ATTACK;
                        break;

                    case E_InputType.WALK_RIGHT_CANCELED :
                        isWalkRight = false;

                        //左が同時押しされている場合
                        if(isWalkLeft){
                            playerDirection = PlayerDirection.LEFT;
                        }

                        break;

                    default:
                        break;
                }
            }          
        }

        return nextState;  

    }
    

    override public E_ActionState stateUpdate (){
        E_ActionState nextState = E_ActionState.FALL;

        return nextState;   
    }
}
