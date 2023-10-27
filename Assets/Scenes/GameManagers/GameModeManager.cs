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

    public abstract void InitScene();

    public abstract void ReleaseObject();

    protected virtual void ChangeGameMode(E_GameMode nextMode){
        gameModeList[currentGameMode].SetActive(false);
        gameModeList[nextMode].SetActive(true);
        currentGameMode = nextMode;
    }

    public virtual void SetLoadingActive(bool flag){
        loadingUI.SetActive(flag);
    }
}
