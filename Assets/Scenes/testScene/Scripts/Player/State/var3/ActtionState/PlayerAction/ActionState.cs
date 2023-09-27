using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    abstract public class ActionState : I_StateUpdatable{

        protected static bool isLeftMove;

        protected static bool isRightMove;

        protected bool isBufferCheck;
        protected bool isFinished;

        public ActionState (){
            isBufferCheck = false;
            isFinished = false;
            isLeftMove = false;
            isRightMove = false;
        }  

        public S_StateData checkInput( S_StateData state , InputData[] input){

            //バッファのチェックが有効な場合
            if(isBufferCheck){
                input = InputManager.instance.getInputBuffer;
            }

            //入力があるかどうか
            if(input.Length > 0){

                for(int i = 0; i < input.Length; i++){
                    //バッファーを確認する場合
                    if(isBufferCheck){
                        state = bufferStateTransration(input[i].type , state);
                    }else{
                        state = inputStateTransration(input[i].type , state);
                    }
                }

            }

            if(isBufferCheck){
                isBufferCheck = false;
            }

            return state;
        }

        public bool getIsFinished(){
            return isFinished;
        }

        public virtual S_StateData falling(S_StateData state){
            state.moveState = E_MoveState.FALL;
            state.isAir = true;
            return state;
        }

        public virtual S_StateData landing(S_StateData state){
            state.actionState = E_ActionState.LANDING;
            state.moveState = E_MoveState.LAND;

            state.isAir = false;
            return state;
        }

        protected abstract S_StateData inputStateTransration(E_InputType input , S_StateData state);
        protected abstract S_StateData bufferStateTransration(E_InputType input , S_StateData state);

        public abstract void updateState();

        public abstract S_StateData getNextState(S_StateData state);

        public abstract void stateEnter();

    }



}
