using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;
using StateManagement_ver3;

using Cysharp.Threading.Tasks;
using System.Threading.Tasks;


public class ActionSceneManager : GameModeManager{
    protected override UniTask OnInitialize(){
        return UniTask.RunOnThreadPool(()=>{
            Debug.Log("test");
        });
    }

    protected override UniTask OnExit(){
        return UniTask.RunOnThreadPool(()=>{
            Debug.Log("test");
        });
    }
 
}
