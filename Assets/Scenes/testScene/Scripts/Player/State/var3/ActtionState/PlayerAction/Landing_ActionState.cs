using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
using MyGameManagers;

namespace StateManagement_ver3{
    public class Landing_ActionState : ActionState{

        const E_ActionState ownState = E_ActionState.LANDING;

        float currentFrame;

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
                    state.actionState = E_ActionState.ATTACK;
                break;

                case E_InputType.DUSH:
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


        override public S_StateData getNextState(S_StateData state){
            state.actionState = E_ActionState.WAIT;
            state.moveState = E_MoveState.LAND;

            return state;
        }

        override public void stateEnter(){
            isFinished = false;
            isBufferCheck = true;
            currentFrame = 0.0f;
            
        }

    }
}
