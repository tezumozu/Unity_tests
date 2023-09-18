using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class PlayerStateManager {
        private Dictionary<E_PlayerAction,PlayerActionState> actionStateMap;
        private E_PlayerAction currentState;
        private I_2DPlayerUpdatable player;

        public PlayerStateManager(I_2DPlayerUpdatable player){

            //マップの初期化
            actionStateMap = new Dictionary<E_PlayerAction,PlayerActionState>();

            //UPState
            actionStateMap[E_PlayerAction.WAIT] = new Wait_PlayerAction(player);

            //状態管理を初期化
            currentState = E_PlayerAction.WAIT;
        }


        public void updateState(){
            E_PlayerAction nextState = currentState;

            //アクションが終了したときの処理
            if(actionStateMap[currentState].getIsFinished){
                nextState = actionStateMap[currentState].getNextState();
                actionStateMap[nextState].stateEnter();
            }  

            //入力を確認
            nextState = actionStateMap[nextState].checkInpuit();

            //アクションごとの処理
            actionStateMap[nextState].stateUpdate();
        }

    }
}
