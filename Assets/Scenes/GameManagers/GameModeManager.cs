using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

using MyInputSystems;

public abstract class GameModeManager {
    private Dictionary<E_GameMode , GameMode> GameModeList;
    private E_GameMode currentGameMode;
    private GameObject loadingUI;

    public abstract void SceneInit();

    public abstract void ObjectRelease();

    public virtual void ChangeGameMode(E_GameMode nextMode){

    }

    public virtual GameMode GetCurrentGameMode (){
        return GameModeList[currentGameMode];
    }

    public virtual void SetLoadingActive(bool flag){
        loadingUI.SetActive(flag);
    } 
}
