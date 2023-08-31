using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager 
{
    I_2DPlayerUpdatable player;
    PlayerStateManager stateManager;
    public PlayerManager (I_2DPlayerUpdatable player){
        this.player = player;
        stateManager = new PlayerStateManager(player);
    }

    public void managerUpdate(){
        player.playerUpdate();
    }


}

public interface I_2DPlayerUpdatable {
    void playerUpdate ();
    void addPos ( Vector2 vec);
    void addRotate (float rotate);
    void setActionState(E_ActionState state);
    bool isLanding();
}
