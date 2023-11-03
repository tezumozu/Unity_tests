using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;

using UniRx;

abstract public class GameMode : I_GameObjectUpdatable{
    protected InputMode inputMode;
    protected bool isActive;
    protected Subject<bool> setActiveSubject;

    public GameManager OwnGameManager { get; protected set;}


    public GameMode(){
        setActiveSubject = new Subject<bool>();
        isActive = false;
    }


    public virtual void SetActive(bool flag){
        isActive = flag;
        inputMode.SetInputActive(flag);
        setActiveSubject.OnNext(flag);
    }


    //必須メソッド
    public abstract void InitObject();

    public abstract void UpdateManager(InputData[] inputs);

    public abstract void ReleaseObject();



    //必須ではないメソッド
    //UI更新用
    public virtual void UpdateUI(InputData[] inputs){

    }

    //プレイヤーキャラクター更新用
    public virtual void UpdatePlayer(InputData[] inputs){

    }

    //ステージギミックやNPC更新用
    public virtual void UpdateObject(){

    }
}
