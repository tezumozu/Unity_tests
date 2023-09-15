using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateManagement_ver3{
    public class PlayerManager {

        private I_2DPlayerUpdatable player;
        private PlayerStateManager stateManager;

        public PlayerManager (I_2DPlayerUpdatable player){
            this.player = player;
            player.setManager(this);

            stateManager = new PlayerStateManager(player);
        }


        public void managerUpdate(){
            //状態を更新
            stateManager.updateState();
        }

    }
}


