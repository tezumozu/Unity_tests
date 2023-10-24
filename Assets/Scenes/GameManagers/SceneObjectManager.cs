using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

using MyInputSystems;

public abstract class SceneObjectManager {
    private List<InputMode> InputModeList;

    private GameObject loadingUI;

    public bool IsFinished {get; protected set;} = false;

    public E_GameScene GetNextScene {get; protected set;}

    public abstract void ObjectInit();

    public abstract void ObjectRelease();

    public abstract void ManagerUpdate(InputData[] inputs);

    public abstract void UIUpdate();

    public virtual void PlayerUpdate(InputData[] inputs){

    }

    public virtual void ObjectUpdate(){

    }

    public virtual void SetLoadingActive(bool flag){
        loadingUI.SetActive(flag);
    } 
}
