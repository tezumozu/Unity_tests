using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver1{

    public class Action_Jump : ActionState {
        const float jumpAcce =  25.0f;

        public Action_Jump () {
        }

        override public E_ActionState checkInput(){
            E_ActionState nextState = E_ActionState.JUMP;

            if (isInputStandBy){
                //入力確認処理
                var inputData = inputManager.getInputData(6.0f * 1.0f / 60.0f);

                if(inputData.Length < 1){
                    return nextState;
                }

                foreach(var data in inputData) {
                    switch (data.type){
                        case E_InputType.WALK_LEFT_PERFORMED :
                            playerDirection = PlayerDirection.LEFT;
                            isWalkLeft = true;
                            break;


                        case E_InputType.WALK_LEFT_CANCELED :
                            isWalkLeft = false;

                            //右が同時押しされている場合
                            if(isWalkRight){
                                playerDirection = PlayerDirection.RIGHT;
                            }
                            break;


                        case E_InputType.WALK_RIGHT_PERFORMED:
                            playerDirection = PlayerDirection.RIGHT;
                            isWalkRight = true;
                            break;
                        

                        case E_InputType.WALK_RIGHT_CANCELED :
                            isWalkRight = false;

                            //左が同時押しされている場合
                            if(isWalkLeft){
                                playerDirection = PlayerDirection.LEFT;
                            }

                            break;

                        case E_InputType.ATTACK:
                            nextState = E_ActionState.ATTACK;
                            break;


                        case E_InputType.LITTLE_JUMP : ///ジャンプを小ジャンプへ切り替える
                            playerObject.resetGravity();
                            nextState = E_ActionState.FALL;

                            break;
                        

                        default:
                            break;
                    }
                }          
            }

            return nextState;  

        }
        

        override public E_ActionState stateUpdate (){
            E_ActionState nextState = E_ActionState.JUMP;

            Vector3 moveVec = new Vector3 (0.0f,jumpAcce,0.0f) * Time.deltaTime;

            //重力がジャンプのベクトルを上回ったら落下へ遷移
            if(playerObject.getCurrentGravityAcce > jumpAcce * 1.0f / 60.0f){ 
                playerObject.resetGravity();
                nextState = E_ActionState.FALL;
            }

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
