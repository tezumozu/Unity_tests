using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;

using UniRx;

abstract public class GameMode {
    protected List<InputMode> InputModeList;
    protected bool IsActive;

    protected Subject<E_GameMode> ChangeGameModeSubject;

    public bool IsSceneFinished {get; protected set;} = false;

    public E_GameScene GetNextScene {get; protected set;}


    public GameMode(){
        List<InputMode> InputModeList = new List<InputMode>();
        ChangeGameModeSubject = new Subject<E_GameMode>();
    }


    public abstract void ObjectInit();

    public abstract void ManagerUpdate(InputData[] inputs);

    public abstract void UIUpdate(InputData[] inputs);

    public virtual void PlayerUpdate(InputData[] inputs){

    }

    public virtual void ObjectUpdate(){

    }

    public virtual void SetActive(bool flag){
        IsActive = flag;
    }

    public virtual void CangeGameModeSubscribe(Action<E_GameMode> method){
        ChangeGameModeSubject.Subscribe(
            (x) => {
                method(x);
            }
        );
    }
}
