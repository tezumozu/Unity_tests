using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class temp_PlayerAction : I_StateUpdatable{
        static E_ActionState ownState;

        public  temp_PlayerAction (){
            ownState = E_ActionState.WAIT;
        }
        

        public void stateUpdate (){
        }
        

        //State開始時の初期化処理
        public void stateEnter (){
        }


        //State終了時に次にどのStateへ遷移するか
        public E_ActionState getNextState(){
            return ownState;
        }

        public bool getIsFinished(){
            return false;
        }


        //入力確認
        public E_ActionState checkTransrationForInput(E_InputType type){
            return ownState;
        }


        //入力バッファ確認
        public E_ActionState checkTransrationForBuffer(E_InputType type){
            return ownState;
        }

    }
}
