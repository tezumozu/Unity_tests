using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;
using StateManagement_ver3;

public class ActionGameMode : GameMode{
    PlayerManager playerManager;
    I_PlayerUpdatable player;
    StageManager stageManager;


    public ActionGameMode (){
        inputMode = GameObject.Find("IM_Action").GetComponent<InputMode>();
        player = GameObject.Find("Player").GetComponent<I_PlayerUpdatable>();
        inputMode.Init();
    }


    public override void InitObject(){
       
        //プレイヤーマネージャ生成
        playerManager = new PlayerManager(player);

        //ステージマネージャ生成
        stageManager = new StageManager();

        //ゲームマネージャを生成
        OwnGameManager = new ActionGameManager(playerManager);

    }


    public override void UpdateManager(InputData[] inputs){

    }


    public override void UpdatePlayer(InputData[] inputs){

    }


    public override void UpdateObject(){

    }


    public override void ReleaseObject(){

    }

}
