using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;
using StateManagement_ver3;

using Cysharp.Threading.Tasks;
using System.Threading.Tasks;


public class ActionSceneManager : SceneObjectManager{
    public override void ObjectInit(){
        Debug.Log("test");
    }

    public override void ObjectRelease(){

    }

    public override void ManagerUpdate(InputData[] inputs){

    }

    public override void UIUpdate(){
        
    }

    public override void PlayerUpdate(InputData[] inputs){

    }

    public override void ObjectUpdate(){

    } 
}
