using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class PlayerStateManager {
        private Dictionary<E_ActionState,I_StateUpdatable> actionStateMap;
        private I_2DPlayerUpdatable player;

        private S_StateData currentState;


        public PlayerStateManager(I_2DPlayerUpdatable player){
            //状態の初期化
            currentState = new S_StateData();

            currentState.actionState = E_ActionState.WAIT;
            currentState.moveState = E_MoveState.WAIT;
            currentState.isAir = false;
            currentState.playerDirection = E_PlayerDirection.RIGHT;


            this.player = player;
            player.stateUpdate(currentState);

            actionStateMap = new Dictionary<E_ActionState, I_StateUpdatable>();
            actionStateMap[E_ActionState.WAIT] = new Wait_PlayerAction();

            //Subscrive
            player.subscribeFall(falling);
        }

        public void initManager(){
            player.stateUpdate(currentState);

            //状態の初期化
            currentState.actionState = E_ActionState.WAIT;
            currentState.moveState = E_MoveState.WAIT;
            currentState.isAir = false;
            currentState.playerDirection = E_PlayerDirection.RIGHT;
        }


        public void managerUpdate(InputData[] input){
            S_StateData nextState = currentState; 


            //状態が終了していたら
            if(actionStateMap[currentState.actionState].getIsFinished()){
                nextState = actionStateMap[currentState.actionState].getNextState(currentState);
                actionStateMap[currentState.actionState].stateEnter();   
            }


            //入力を確認
            nextState = actionStateMap[currentState.actionState].checkInput(currentState,input);


            //状態の変化があれば状態を更新
            if(currentState.actionState != nextState.actionState){
                actionStateMap[nextState.actionState].stateEnter();
            }

            //状態の更新
            actionStateMap[nextState.actionState].updateState();


            //状態の更新
            currentState = nextState;


            //プレイヤーの状態を更新
            player.stateUpdate(nextState);

        }

        public void falling(){
            Debug.Log("Falling!");
            currentState.isAir = true;
        }

        public void landing(){
            Debug.Log("Landing!");
        }
    }
}
