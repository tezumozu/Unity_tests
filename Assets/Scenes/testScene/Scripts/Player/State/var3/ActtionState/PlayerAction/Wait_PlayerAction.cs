using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class Wait_PlayerAction : PlayerActionState{
        private bool isToLanding; 

        public  Wait_PlayerAction (I_2DPlayerUpdatable player) : base(player){
            ownState = E_PlayerAction.WAIT;
        }

        //Stateの処理
        override public void stateProsses (){
            //Debug.Log("WAIT");
            moveStateMap[currentMoveState].movePlayer();
        }


        //State開始時の初期化処理
        override public void stateEnter (){
            isBufferCheck = false;
            isFinished = false;
            ownState = E_PlayerAction.WAIT;
            isToLanding = false;
        }


        //State終了時に次にどのStateへ遷移するか
        override public E_PlayerAction getNextState(){
            if(isToLanding){
                return E_PlayerAction.LANDING;
            }else{
                return ownState;
            }
        }


        //入力確認
        override protected E_PlayerAction checkTransrate(E_PlayerAction currentState, E_InputType type){
            return currentState;
        }


        //入力バッファ確認
        override protected E_PlayerAction checkTransrateForBuffer(E_PlayerAction currentState, E_InputType type){
            return currentState;
        }


        //着地時処理
        override protected void landing (){
            isToLanding = true;
            isFinished = true;
            return;
        }


        //落下時処理
        override protected void falling (){
            currentMoveState = E_MoveState.FALL;
            return;
        }

    }
}
