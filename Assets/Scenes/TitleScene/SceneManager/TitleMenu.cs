using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;

public class TitleMenu : GameMode{

    private TitleMenuUIManager UIManager;

    public TitleMenu(){
        //入力を初期化する
        inputMode = GameObject.Find("IM_TitleMenu").GetComponent<InputMode>();
        inputMode.Init();
    }   

    public override void InitObject(){

        //UIマネージャを生成
        UIManager = new TitleMenuUIManager();
       
        //ゲームマネージャを生成
        OwnGameManager = new TitleMenuGameManager(UIManager);

    }

    public override void UpdateManager(InputData[] inputs){

    }

    public override void ReleaseObject(){

    }

    public override void UpdateUI(InputData[] inputs){
        UIManager.UpdateUI(inputs);
    }
}
