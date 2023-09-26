using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class PlayerManager {

        private I_2DPlayerUpdatable player;
        private PlayerStateManager stateManager;

        public PlayerManager (I_2DPlayerUpdatable player){
            this.player = player;
            player.playerInit();
            stateManager = new PlayerStateManager(player);
        }


        public void playerUpdate(InputData[] input){
            //状態を更新
            stateManager.managerUpdate(input);

            //プレイヤーの更新
            player.playerUpdate();
        }

    }
}


