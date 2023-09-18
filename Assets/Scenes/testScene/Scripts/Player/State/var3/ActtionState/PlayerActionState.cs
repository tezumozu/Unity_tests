using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public abstract class PlayerActionState {

        private static bool isAir;

        protected static E_PlayerDirection direction;
        protected static Dictionary <E_MoveState , MoveActionState> moveStateMap;
        protected static E_MoveState currentMoveState;

        protected bool isFinished; 
        protected bool isBufferCheck;
        protected E_PlayerAction ownState;
        protected I_2DPlayerUpdatable player;

        const float availableInputTime = 6.0f * GameValue.g_FrameTime;

        public bool getIsFinished{
            get {
                return isFinished;
            }
        }

        protected bool getIsAir{
            get {
                return isAir;
            }
        }


        public PlayerActionState (I_2DPlayerUpdatable player){
            this.player = player;
            direction = E_PlayerDirection.RIGHT;
            isAir = true;
            isFinished = false;
            ownState = E_PlayerAction.WAIT;

            //移動関係の状態遷移初期化
            if(moveStateMap == null){
                moveStateMap = new Dictionary<E_MoveState, MoveActionState>();




                currentMoveState = E_MoveState.WAIT;
            }
        }

        
        abstract public void stateProsses ();


        abstract public void stateEnter ();


        abstract public E_PlayerAction getNextState();


        abstract protected E_PlayerAction checkTransrate(E_PlayerAction currentState, E_InputType type);


        abstract protected E_PlayerAction checkTransrateForBuffer(E_PlayerAction currentState, E_InputType type);


        virtual protected void landing (){
            return;
        }


        virtual protected void falling (){
            return;
        }


        public void stateUpdate (){
            //着地判定を確認
            checkLanding();

            //状態ごとの処理
            stateProsses();
        }


        //着地、落下判定
        protected void checkLanding(){
            //着地
            if(isAir){
                if(player.isLanding()){
                    isAir = false;
                    landing(); 
                }

            //落下
            }else{
                if(!player.isLanding()){
                    isAir = true;
                    falling();
                }
            }
        }


        public E_PlayerAction checkInpuit(){
            E_PlayerAction nextState = ownState;

            //バッファを確認（先行入力など）
            if(isBufferCheck){
                //入力を取得
                var inputData = InputManager.instance.getInputBuffer;

                //入力があれば
                if(inputData.Length > 0){
                    for (int i = 0; i < inputData.Length; i++){
                        nextState = checkTransrateForBuffer(nextState , inputData[i].type);
                    }
                }

            //通常の入力
            }else{
                //入力を取得
                var inputData = InputManager.instance.getInputList;

                //入力があれば
                if(inputData.Length > 0){
                    for (int i = 0; i < inputData.Length; i++){
                        nextState = checkTransrate(nextState , inputData[i].type);
                    }
                }

            }

            return ownState;

        }
    }
}
