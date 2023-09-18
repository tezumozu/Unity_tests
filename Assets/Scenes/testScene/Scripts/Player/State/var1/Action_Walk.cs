using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver1{

    public class Action_Walk : ActionState {

        public Action_Walk () {
            
        }

        override public E_ActionState checkInput(){
            E_ActionState nextState = E_ActionState.WALK;

            if (isInputStandBy){
                //入力確認処理
                var inputData = inputManager.getInputList;

                if(inputData.Length < 1){
                    return nextState;
                }

                foreach(var data in inputData) {
                    switch (data.type){
                        case E_InputType.WALK_LEFT_PERFORMED :
                            playerDirection = PlayerDirection.LEFT;
                            isWalkLeft = true;
                            nextState = E_ActionState.WALK;
                            break;


                        case E_InputType.WALK_LEFT_CANCELED :
                            isWalkLeft = false;
                            nextState = E_ActionState.WAIT;

                            //右が同時押しされている場合
                            if(isWalkRight){
                                playerDirection = PlayerDirection.RIGHT;
                                nextState = E_ActionState.WALK;
                            }
                            break;


                        case E_InputType.WALK_RIGHT_PERFORMED:
                            playerDirection = PlayerDirection.RIGHT;
                            isWalkRight = true;
                            nextState = E_ActionState.WALK;
                            break;

                        case E_InputType.WALK_RIGHT_CANCELED :
                            
                            isWalkRight = false;
                            nextState = E_ActionState.WAIT;

                            //左が同時押しされている場合
                            if(isWalkLeft){
                                playerDirection = PlayerDirection.LEFT;
                                nextState = E_ActionState.WALK;
                            }

                            break;
                            

                        case E_InputType.JUMP:
                            nextState = E_ActionState.JUMP;
                            getPlayer.toAir();
                            break;

                        case E_InputType.ATTACK:
                            nextState = E_ActionState.ATTACK;
                            break;
                    }
                }

                return nextState;            
            }

            return E_ActionState.WAIT;

        }

        override public E_ActionState stateUpdate (){
            E_ActionState nextState = E_ActionState.WALK;
            
            Vector3 moveVec = new Vector3 (0.0f,0.0f,0.0f) * Time.deltaTime;

            //左右への移動
            if(isWalkLeft||isWalkRight){
                if(playerDirection == PlayerDirection.LEFT){
                    moveVec.x = -moveDistance * Time.deltaTime;
                }else{
                    moveVec.x = moveDistance * Time.deltaTime;
                }
            }

            //移動する
            playerObject.transform.position += moveVec;
            
            return nextState;

        }
    }
}
