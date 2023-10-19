using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class Dush_ActionState : ActionState{

        const E_ActionState ownState = E_ActionState.DUSH;
        //float currentFrame;
        E_ActionPhase currentPhase;

        public Dush_ActionState (I_PlayerStateUpdatable player): base(player){
            //currentFrame = 0.0f;
        }


        override public void updateState (){
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
            //呼ばれないので呼ばれたらバグ
            Debug.Log("Wait_PlayerAction bufferStateTransration called!");
            return state;
        }


        override public S_StateData getNextState(S_StateData state){
            return state;
        }

        override public void stateEnter(S_StateData state){
        }

    }
}
