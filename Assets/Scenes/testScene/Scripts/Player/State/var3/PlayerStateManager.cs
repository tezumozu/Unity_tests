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
            currentState.moveState = E_MoveState.FALL;
            currentState.playerDirection = E_PlayerDirection.RIGHT;
            currentState.isAir = true;
            currentState.isRanning = false;

            this.player = player;
            player.stateUpdate(currentState);

            //状態の初期化
            actionStateMap = new Dictionary<E_ActionState, I_StateUpdatable>();
            actionStateMap[E_ActionState.WAIT] = new Wait_ActionState();
            actionStateMap[E_ActionState.LANDING] = new Landing_ActionState();
            actionStateMap[E_ActionState.JUMP] = new Jump_ActionState();

            //Subscrive
            player.subscribeFall(falling);
            player.subscribeLanding(landing);
        }

        public void initManager(){
            player.stateUpdate(currentState);

            //状態の初期化
            currentState.actionState = E_ActionState.WAIT;
            currentState.moveState = E_MoveState.LAND;
            currentState.isAir = false;
            currentState.playerDirection = E_PlayerDirection.RIGHT;
        }


        public void managerUpdate(InputData[] input){
            S_StateData nextState = currentState; 


            //状態が終了していたら
            if(actionStateMap[currentState.actionState].getIsFinished()){
                nextState = actionStateMap[currentState.actionState].getNextState(currentState);
            }


            //入力を確認
            nextState = actionStateMap[nextState.actionState].checkInput(nextState,input);


            //状態の変化があれば状態を更新
            if(currentState.actionState != nextState.actionState){
                actionStateMap[nextState.actionState].stateEnter();
            }

            //状態の更新
            actionStateMap[nextState.actionState].updateState();

            //状態を上書き
            currentState = nextState;

            //プレイヤーの状態を更新
            player.stateUpdate(nextState);

        }

        public void falling(){
            currentState = actionStateMap[currentState.actionState].falling(currentState);
            actionStateMap[currentState.actionState].stateEnter();
            player.stateUpdate(currentState);
        }

        public void landing(){
            currentState = actionStateMap[currentState.actionState].landing(currentState);
            actionStateMap[currentState.actionState].stateEnter();
            player.stateUpdate(currentState);
            
        }
    }
}
