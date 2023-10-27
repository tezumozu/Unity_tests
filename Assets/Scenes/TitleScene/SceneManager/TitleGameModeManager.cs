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


    async public override void InitScene(){
        //各ゲームモードの初期化
        List<UniTask> taskList = new List<UniTask>();

        var keys = gameModeList.Keys;

        foreach( var key in keys ){

            var task = UniTask.RunOnThreadPool(()=>{
                gameModeList[key].ObjectInit();
                gameModeList[key].SubscribeCangeGameMode(ChangeGameMode);
            });

            taskList.Add(task);
        }

        await taskList;

        gameModeList[E_GameMode.TITLE].SetActive(true);
    }

    //ゲームモードを破棄してオブジェクトリリース　明示的にリリースしたほうがよい？
    async public override void ReleaseObject(){
        gameModeList.Clear();
    }


}
