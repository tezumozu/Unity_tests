using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class temp_PlayerAction : PlayerActionState{

        public  temp_PlayerAction (I_2DPlayerUpdatable player) : base(player){
            ownState = E_PlayerAction.WAIT;
        }

        //Stateの処理
        override public void stateProsses (){

        }


        //State開始時の初期化処理
        override public void stateEnter (){

        }


        //State終了時に次にどのStateへ遷移するか
        override public E_PlayerAction getNextState(){
            return ownState;
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
            return;
        }


        //落下時処理
        override protected void falling (){
            return;
        }

    }
}
