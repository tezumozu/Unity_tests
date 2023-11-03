using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;

using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class TitleGameModeManager : GameModeManager{

    public TitleGameModeManager () {
        //各ゲームモードのインスタンス生成（初期化しない）
        gameModeList[E_GameMode.TITLE] = new TitleMenu();

    }


    //Titleシーン固有の初期化処理
    protected override UniTask OnInitialize(){
        return UniTask.RunOnThreadPool(()=>{
            currentGameMode = E_GameMode.TITLE;
            gameModeList[E_GameMode.TITLE].SetActive(true);
        });
    }


    //Titleシーン固有の終了処理
    protected override UniTask OnFinal(){
        return UniTask.RunOnThreadPool(()=>{
            
        });
    }


}
