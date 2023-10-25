using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UniRx;

public class SceneLoader{
    private static Subject<E_GameScene> loadSubject;
    private AsyncOperation asyncLoad;

    public SceneLoader(){
        if(loadSubject == null){
            loadSubject = new Subject<E_GameScene>();
        }
    }

    public IEnumerator loadScene (E_GameScene sceneName){
        loadSubject.OnNext(sceneName);
        asyncLoad = SceneManager.LoadSceneAsync(Enum.GetName(typeof(E_GameScene),sceneName));

        asyncLoad.allowSceneActivation = false;

        while(!asyncLoad.isDone){
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }


    public static void SubscriveLoad (Action<E_GameScene> method){
        loadSubject.Subscribe((x) => {
            method(x);
        });
    }
}
