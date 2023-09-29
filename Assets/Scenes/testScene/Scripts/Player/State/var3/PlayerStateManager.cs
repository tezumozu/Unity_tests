using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
using UniRx;

namespace StateManagement_ver3{
    public class PlayerStateManager {
        private Dictionary<E_ActionState,I_StateUpdatable> actionStateMap;

        private S_StateData currentState;

        private I_2DPlayerUpdatable player;

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
            actionStateMap[E_ActionState.WAIT] = new Wait_ActionState(player);
            actionStateMap[E_ActionState.LANDING] = new Landing_ActionState(player);
            actionStateMap[E_ActionState.JUMP] = new Jump_ActionState(player);
            actionStateMap[E_ActionState.ATTACK] = new Attack_ActionState(player);

            //Subscrive
            player.subscribeFall(falling);
            player.subscribeLanding(landing);

            ActionState.subscribeUpdateAction(updateAction);
            ActionState.subscribeUpdateMove(updateMove);
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

            //状態が終了していたら
            if(actionStateMap[currentState.actionState].getIsFinished()){
                currentState = actionStateMap[currentState.actionState].getNextState(currentState);
            }

            //入力を確認
            currentState = actionStateMap[currentState.actionState].checkInput(currentState,input);

            //状態の更新
            actionStateMap[currentState.actionState].updateState();

             //プレイヤーの状態をセット
            player.stateUpdate(currentState);
        }



        public void falling(){
            currentState = actionStateMap[currentState.actionState].falling(currentState);
            player.stateUpdate(currentState);
        }

        public void landing(){
            currentState = actionStateMap[currentState.actionState].landing(currentState);
            player.stateUpdate(currentState);
        }

        public void updateAction(E_ActionState state){
            Debug.Log(state);
            actionStateMap[state].stateEnter(currentState);
            player.actionEnter(state);
        }

        public void updateMove(E_MoveState state){
            player.moveEnter(state);
        }
    }
}
