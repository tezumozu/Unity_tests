using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public abstract class GameManager : I_ChangeGameModeAreltable , I_LoadSceneAreltable{

    protected Subject<E_GameMode> changeGameModeAreltSubject;
    protected Subject<E_GameScene> loadSceneAreltSubject;

    public GameManager (){
        changeGameModeAreltSubject = new Subject<E_GameMode>();
        loadSceneAreltSubject = new Subject<E_GameScene>();
    }

    //シーンロード
    protected virtual void loadSceneAlart(E_GameScene nextScene){
        loadSceneAreltSubject.OnNext(nextScene);
    }

    //シーンロードをサブスクライブ
    public virtual void SubscribeLoadSceneAlart(Action<E_GameScene> method){
        loadSceneAreltSubject.Subscribe((x) => {
            method(x);
        });
    }


    //ゲームモード切り替え
    protected virtual void changeGameModeAlart(E_GameMode nextMode){
        changeGameModeAreltSubject.OnNext(nextMode);
    }

    //ゲームモード切り替えをサブスクライブ
    public virtual void SubscribeChangeGameModeAlert(Action<E_GameMode> method){
        changeGameModeAreltSubject.Subscribe((x) => {
            method(x);
        });
    }
}
