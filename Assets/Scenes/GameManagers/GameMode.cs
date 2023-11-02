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

    public GameManager GameManager { get; protected set;}


    public GameMode(){
        setActiveSubject = new Subject<bool>();
        isActive = false;
    }


    public virtual void SetActive(bool flag){
        isActive = flag;
        inputMode.SetActive(flag);
    }


    //必須メソッド
    public abstract void ObjectInit();

    public abstract void ManagerUpdate(InputData[] inputs);

    public abstract void ReleaseObject();



    //必須ではないメソッド
    //UI更新用
    public virtual void UIUpdate(InputData[] inputs){

    }

    //プレイヤーキャラクター更新用
    public virtual void PlayerUpdate(InputData[] inputs){

    }

    //ステージギミックやNPC更新用
    public virtual void ObjectUpdate(){

    }
}
