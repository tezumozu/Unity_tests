using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;
using StateManagement_ver3;

public class ActionGameManager : GameManager{

    I_PlayerDataLoadable playerDataLoader;

    public ActionGameManager (I_GameOverAlertable playerManager, I_StageClearAlertable stageManager, I_PlayerDataLoadable dataLoader){
        //残機を監視
        //ステージクリアを監視

        //プレイヤーデータの取得先
        playerDataLoader = dataLoader;
    }
}
