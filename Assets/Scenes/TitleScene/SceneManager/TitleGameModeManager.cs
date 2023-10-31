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


    protected override UniTask OnInitialize(){
        return UniTask.RunOnThreadPool(()=>{
            currentGameMode = E_GameMode.TITLE;
            gameModeList[E_GameMode.TITLE].SetActive(true);
        });
    }


    //ゲームモードを破棄してオブジェクトリリース　明示的にリリースしたほうがよい？
    protected override UniTask OnExit(){
        return UniTask.RunOnThreadPool(()=>{
            currentGameMode = E_GameMode.TITLE;
            gameModeList[E_GameMode.TITLE].SetActive(true);
        });
    }


}
