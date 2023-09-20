using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class Wait_PlayerAction : I_StateUpdatable{

        static E_ActionState ownState;
        private I_2DPlayerUpdatable player;

        public  Wait_PlayerAction (){
            ownState = E_ActionState.WAIT;
        }
        

        public void stateUpdate (S_StateData stateData){
            player.stateEnter(stateData.actionState);
            player.moveEnter(stateData.moveState,stateData.playerDirection);
        }
        

        //State開始時の初期化処理
        public void stateEnter (){
        }

        //Stateの完了を確認
        public bool getIsFinished(){
            return false;
        }

        //State終了時に次にどのStateへ遷移するか
        public E_ActionState getNextState(){
            return ownState;
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
