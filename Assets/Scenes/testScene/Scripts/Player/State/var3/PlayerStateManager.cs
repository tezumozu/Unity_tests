using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class PlayerStateManager {
        private Dictionary<E_ActionState,I_StateUpdatable> actionStateMap;
        private I_2DPlayerUpdatable player;

        private S_StateData stateData;


        public PlayerStateManager(I_2DPlayerUpdatable player){
            this.player = player;
            stateData = new S_StateData();

            //状態の初期化
            stateData.actionState = E_ActionState.WAIT;
            stateData.moveState = E_MoveState.WAIT;
            stateData.isAir = false;
            stateData.playerDirection = E_PlayerDirection.RIGHT;
        }


        public void managerUpdate(){

            //入力を確認
            player.stateEnter(stateData.actionState);

            //状態の変化があれば状態を更新
            
        }

    }
}
