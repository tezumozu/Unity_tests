using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
using MyGameManagers;

namespace StateManagement_ver3{
    public class Landing_ActionState : ActionState{

        const E_ActionState ownState = E_ActionState.LANDING;

        float currentFrame;

        public Landing_ActionState (I_PlayerStateUpdatable player): base(player){
            
        }

        override public void updateState (){
            currentFrame += Time.deltaTime;

            //さらに一定フレーム経過したら
            if(currentFrame > GameManager.instance.getActionConfig.landing.followThrough * 1.0f/60.0f){
                isFinished = true;
            }
        }


        override protected S_StateData inputStateTransration(E_InputType input , S_StateData state){

            //移動関係
            switch (input){

                case E_InputType.JUMP:
                    state.isAir = true;
                    state.moveState = E_MoveState.JUMP;
                    isUpdateMove = true;
                break;


                default:
                break;
            }

            //アクション関係
            switch (input){
                case E_InputType.JUMP:
                    state.actionState = E_ActionState.JUMP;
                    isUpdateAction = true;
                break;

                case E_InputType.ATTACK:
                    state.actionState = E_ActionState.ATTACK;
                    isUpdateAction = true;
                break;

                case E_InputType.DUSH:
                    isUpdateAction = true;
                break;

                default:
                break;
            }
            return state;
        }


        override protected S_StateData bufferStateTransration(E_InputType input , S_StateData state){

            //移動関係
            switch (input){

                case E_InputType.JUMP:
                    state.isAir = true;
                    state.moveState = E_MoveState.JUMP;
                    isUpdateMove = true;
                break;


                default:
                break;
            }

            //アクション関係
            switch (input){
                case E_InputType.JUMP:
                    state.actionState = E_ActionState.JUMP;
                    isUpdateAction = true;
                break;

                case E_InputType.ATTACK:
                    state.actionState = E_ActionState.ATTACK;
                    isUpdateAction = true;
                break;

                case E_InputType.DUSH:
                    state.actionState = E_ActionState.ATTACK;
                    isUpdateAction = true;
                break;

                default:
                break;
            }
            return state;
        }


        override public S_StateData getNextState(S_StateData state){
            state.actionState = E_ActionState.WAIT;

            player.actionEnter(state.actionState);
            return state;
        }

        override public void stateEnter(S_StateData state){
            isFinished = false;
            isBufferCheck = true;
            currentFrame = 0.0f;
            
        }

    }
}
