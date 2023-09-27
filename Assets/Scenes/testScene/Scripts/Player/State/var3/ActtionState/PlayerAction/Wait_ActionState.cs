using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class Wait_ActionState : ActionState{

        const E_ActionState ownState = E_ActionState.WAIT;


        override public void updateState (){
        }


        override protected S_StateData inputStateTransration(E_InputType input , S_StateData state){

            //移動関係
            switch (input){
                case E_InputType.WALK_RIGHT_PERFORMED:
                    isRightMove = true;
                    state.playerDirection = E_PlayerDirection.RIGHT;
                    state.isRanning = true;
                break;


                case E_InputType.WALK_RIGHT_CANCELED:
                    isRightMove = false;

                    //同時押しされていた場合
                    if(isLeftMove){
                        state.playerDirection = E_PlayerDirection.LEFT;
                    }else{
                        state.isRanning = false;
                    }
                    
                break;


                case E_InputType.WALK_LEFT_PERFORMED:
                    isLeftMove = true;
                    state.playerDirection = E_PlayerDirection.LEFT;
                    state.isRanning = true;
                break;


                case E_InputType.WALK_LEFT_CANCELED:
                    isLeftMove = false;
                    //同時押しされていた場合
                    if(isRightMove){
                        state.playerDirection = E_PlayerDirection.RIGHT;
                    }else{
                        state.isRanning = false;
                    }
                break;

                case E_InputType.JUMP:
                    state.isAir = true;
                    state.moveState = E_MoveState.JUMP;
                break;

                default:
                break;
            }

            //アクション関係
            switch (input){
                case E_InputType.JUMP:
                    state.actionState = E_ActionState.JUMP;
                break;

                case E_InputType.ATTACK:
                break;

                case E_InputType.DUSH:
                break;

                default:
                break;
            }
            return state;
        }


        override protected S_StateData bufferStateTransration(E_InputType input , S_StateData state){
            //呼ばれないので呼ばれたらバグ
            Debug.Log("Wait_PlayerAction bufferStateTransration called!");
            return state;
        }


        override public S_StateData getNextState(S_StateData state){
            return state;
        }

        override public void stateEnter(){
        }

    }
}
