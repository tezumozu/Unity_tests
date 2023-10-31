using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;

using UniRx;

abstract public class GameMode {
    protected InputMode inputMode;
    protected bool isActive;

    protected Subject<E_GameMode> changeGameModeSubject;
    protected Subject<bool> setActiveSubject;

    public GameMode(){
        changeGameModeSubject = new Subject<E_GameMode>();
        setActiveSubject = new Subject<bool>();
        isActive = false;
    }

    public abstract void ObjectInit();

    public abstract void ManagerUpdate(InputData[] inputs);

    public abstract void UIUpdate(InputData[] inputs);

    public virtual void PlayerUpdate(InputData[] inputs){

    }

    public virtual void ObjectUpdate(){

    }

    public virtual void ObjectRelease(){

    }

    public virtual void SetActive(bool flag){
        isActive = flag;
        inputMode.SetActive(flag);
    }

    public virtual void SubscribeCangeGameMode(Action<E_GameMode> method){
        changeGameModeSubject.Subscribe(
            (x) => {
                method(x);
            }
        );
    }

    public virtual void SubscribeSetActive(Action<bool> method){
        setActiveSubject.Subscribe(
            (x) => {
                method(x);
            }
        );
    }
}
