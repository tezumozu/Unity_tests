using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

using MyInputSystems;

using UniRx;

public abstract class GameModeManager {
    protected Dictionary<E_GameMode , GameMode> gameModeList;
    protected E_GameMode currentGameMode;
    private GameObject loadingUI;
    private Subject<bool> setActiveSubject;
    public GameMode GetCurrentGameMode{
       get{ return gameModeList[currentGameMode];}
    }

    public GameModeManager (){
        loadingUI = GameObject.Find("LoadingUI");
        gameModeList = new Dictionary<E_GameMode, GameMode>();
    }

    async public void InitScene(){
        //各ゲームモードの初期化
        List<UniTask> taskList = new List<UniTask>();

        var keys = gameModeList.Keys;

        foreach( var key in keys ){

            //上書き対策
            var currentKey = key;

            var task = UniTask.RunOnThreadPool(()=>{
                gameModeList[currentKey].ObjectInit();
                gameModeList[currentKey].GameManager.SubscribeChangeGameModeAlert(ChangeGameMode);
            });

            taskList.Add(task);
        }

        await taskList;

        //各GameModeManager固有の初期化処理
        await OnInitialize();
    }

    protected abstract UniTask OnInitialize();

    public async void ReleaseObject(){

        //各ゲームモードのオブジェクトを破棄
        List<UniTask> taskList = new List<UniTask>();

        var keys = gameModeList.Keys;

        foreach( var key in keys ){

            //上書き対策
            var currentKey = key;

            var task = UniTask.RunOnThreadPool(()=>{
                gameModeList[currentKey].ReleaseObject();
            });

            taskList.Add(task);
        }

        await taskList;

        await OnExit();

        gameModeList.Clear();
    }

    protected abstract UniTask OnExit();

    protected virtual void ChangeGameMode(E_GameMode nextMode){
        gameModeList[currentGameMode].SetActive(false);
        gameModeList[nextMode].SetActive(true);
        currentGameMode = nextMode;
    }

    public virtual void SetLoadingActive(bool flag){
        loadingUI.SetActive(flag);
    }
}
