using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

using MyInputSystems;

using UniRx;

public abstract class GameModeManager {
    protected Dictionary<E_GameMode , GameMode> gameModeList;
    protected Dictionary<E_GameMode , string> gameModeInputList;
    protected E_GameMode currentGameMode;
    protected E_GameMode nextGameMode;

    protected bool isHaveNeedChangeGameMode;

    protected GameObject loadingUI;
    protected Subject<bool> setActiveSubject;
    public GameMode GetCurrentGameMode{
       get{ return gameModeList[currentGameMode];}
    }


    public GameModeManager (){
        isHaveNeedChangeGameMode = false;
        loadingUI = GameObject.Find("LoadingUI");
        gameModeList = new Dictionary<E_GameMode, GameMode>();
        gameModeInputList = new Dictionary<E_GameMode, string>();
    }


    //各GameModeManager固有の初期化処理
    protected abstract UniTask OnInitialize();

    //各GameModeManager固有の終了処理
    protected abstract UniTask OnFinal();

    //自信をUpdateする
    public virtual void UpdateOwn(){
        if(isHaveNeedChangeGameMode){
            //現状のモードを無効にする
            gameModeList[currentGameMode].SetActive(false);

            //次のモードを有効にする
            gameModeList[nextGameMode].SetActive(true);
            currentGameMode = nextGameMode;
        }
    }

    async public void InitScene(){
        //各ゲームモードの初期化
        List<UniTask> taskList = new List<UniTask>();

        var keys = gameModeList.Keys;

        foreach( var key in keys ){

            //上書き対策
            var currentKey = key;

            var task = UniTask.RunOnThreadPool(()=>{
                gameModeList[currentKey].InitObject();
                gameModeList[currentKey].OwnGameManager.SubscribeChangeGameModeAlert(ChangeGameMode);
            });

            taskList.Add(task);
        }

        await taskList;

        //各GameModeManager固有の初期化処理
        await OnInitialize();
    }


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

        //固有の終了処理
        await OnFinal();
    }


    protected virtual void ChangeGameMode(E_GameMode nextMode){
        isHaveNeedChangeGameMode = true;
        nextGameMode = nextMode;
    }

    public virtual void SetLoadingActive(bool flag){
        loadingUI.SetActive(flag);
    }

    public virtual List<I_LoadSceneAreltable> GetLoadSceneAreltableObjects (){
        var result = new List<I_LoadSceneAreltable>();
        foreach(var key in gameModeList.Keys){
            result.Add(gameModeList[key].OwnGameManager);
        }
        return result;
    }
}
