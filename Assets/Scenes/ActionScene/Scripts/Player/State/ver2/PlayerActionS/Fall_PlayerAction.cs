using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver2 {
    public class Fall_PlayerAction : PlayerActionState{
        private const float moveDistance = 10.0f;


        public Fall_PlayerAction (I_2DPlayerUpdatable player) : base(player){
            ownState = E_PlayerAction.FALL;
        }


        //状態の処理
        public override void stateUpdate(){
            Debug.Log("Fall");

            var moveVec = new Vector2 (0.0f,0.0f);

            moveVec = gravityManager.addGravity(moveVec);

            //落下する
            player.addPos(moveVec);

            //左右への移動
            horizontalMove();
        }


        public override void stateEntrance(){
            bufferedInpuitAvailable = false;
            isFinished = false;
            inputStandBy = true;
            isAir = true;
            gravityManager.resetGravity();
        }


        public override E_PlayerAction stateExit(){
            return ownState;
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
            E_PlayerAction nextState = E_PlayerAction.FALL;

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
                default:
                    break;
            }

            return nextState;
        }
    }
}
