using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver2 {
    public class Jump_PlayerAction : PlayerActionState{
        
        const float jumpAccel = 24.0f;
        const float moveDistance = 10.0f;

        public Jump_PlayerAction (I_2DPlayerUpdatable player) : base(player){
            ownState = E_PlayerAction.JUMP;
            gravityManager.resetGravity();
        }

        public override void stateUpdate(){

            var moveVec = new Vector2 (0.0f,0.0f);

            moveVec = gravityManager.addGravity(moveVec);

            moveVec.y = moveVec.y + jumpAccel * Time.deltaTime;

            //ジャンプが最高地点に到達したら
            if(moveVec.y < 0){
                isFinished = true;
            }

            //左右への移動
            horizontalMove();

            player.addPos(moveVec);
            Debug.Log("JUMP");
        }


        public override void stateEntrance(){
            bufferedInpuitAvailable = false;
            isFinished = false;
            inputStandBy = true;
            isAir = true;
            gravityManager.resetGravity();
        }


        public override E_PlayerAction stateExit(){
            return E_PlayerAction.FALL;
        }
    /*
        protected override E_PlayerAction toLand(){
            return E_PlayerAction.LANDING;
        }

        protected override E_PlayerAction toAir(){
            return E_PlayerAction.FALL;
        }
    */

        protected override E_PlayerAction inputStateTransition(E_InputType input){
            E_PlayerAction nextState = E_PlayerAction.JUMP;

            switch (input){
                case E_InputType.WALK_LEFT_PERFORMED :
                    playerDirection = E_PlayerDirection.LEFT;
                    isWalkLeft = true;
                    break;


                case E_InputType.WALK_LEFT_CANCELED :
                    isWalkLeft = false;

                    //右が同時押しされている場合
                    if(isWalkRight){
                        playerDirection = E_PlayerDirection.RIGHT;
                    }
                    break;


                case E_InputType.WALK_RIGHT_PERFORMED:
                    playerDirection = E_PlayerDirection.RIGHT;
                    isWalkRight = true;
                    break;
                        

                case E_InputType.WALK_RIGHT_CANCELED :
                    isWalkRight = false;

                    //左が同時押しされている場合
                    if(isWalkLeft){
                        playerDirection = E_PlayerDirection.LEFT;
                    }

                    break;

                /*
                case E_InputType.ATTACK:
                    nextState = E_PlayerAction.ATTACK;
                    
                    break;
                */

                case E_InputType.LITTLE_JUMP : ///ジャンプを小ジャンプへ切り替える
                    nextState = E_PlayerAction.FALL;

                    break;

                default:
                    break;
            }

            return nextState;
        }

    }
}
