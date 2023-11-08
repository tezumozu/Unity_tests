using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataLoader : I_PlayerDataLoadable , I_PlayerDataUpdatable , I_GameInitializable {

    PlayerData playerData;

    public void GameInitialize(){

    }

    public void UpdateTresure(E_Tresure tresuerName){

    }

    public void ClearStage(E_Stage stageName){

    }

    public PlayerData LoadData(){
        return playerData;
    }
}
