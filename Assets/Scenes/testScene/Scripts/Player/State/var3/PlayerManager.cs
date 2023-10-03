using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class PlayerManager {

        private I_PlayerUpdatable player;
        private PlayerStateManager stateManager;

        public PlayerManager (I_PlayerUpdatable player){
            this.player = player;
            player.playerInit();
            stateManager = new PlayerStateManager(player);
        }


        public void playerUpdate(InputData[] input){

            //落下・着地の確認
            player.checkLanding();

            //状態を更新
            stateManager.managerUpdate(input);

            //プレイヤーの更新
            player.playerUpdate();
        }

    }
}


