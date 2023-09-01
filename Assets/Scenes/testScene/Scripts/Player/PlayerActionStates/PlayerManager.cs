using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager {
    I_2DPlayerUpdatable player;
    PlayerStateManager stateManager;

    public PlayerManager (I_2DPlayerUpdatable player){
        this.player = player;
        stateManager = new PlayerStateManager(player);
        player.setManager(this);
    }


    public void managerUpdate(){
        //状態の更新
        stateManager.managerUpdate();
        
        //プレイヤーの更新
        player.playerUpdate();
    }

}
