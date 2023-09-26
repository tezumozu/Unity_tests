using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    abstract public class ActionState : I_StateUpdatable{

        protected bool isBufferCheck;

        protected bool isFinished;

        public ActionState (){
            isBufferCheck = false;
            isFinished = false;
        }  

        abstract protected S_StateData inputStateTransration(E_InputType input , S_StateData state);
        abstract protected S_StateData bufferStateTransration(E_InputType input , S_StateData state);

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

            return state;
        }

        public bool getIsFinished(){
            return isFinished;
        }


        abstract public void updateState();

        abstract public S_StateData getNextState(S_StateData state);

        abstract public void stateEnter();
    }



}
