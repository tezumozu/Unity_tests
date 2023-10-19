using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;

using Cysharp.Threading.Tasks;

public class TitleGameManager : MyGameManager{

    protected override async void managerInit(){
        await UniTask.Delay(TimeSpan.FromSeconds(3));
        LoadingUI.SetActive(false);
        currentGameState = E_GameState.PLAY;
    }

    protected override void managerUpdate(InputData[] inputs){
        //入力を確認
        for(int i = 0; i < inputs.Length; i++){
            if(inputs[i].type == E_InputType.DECIDE){
                translateScene(E_GameScene.ActionScene);
            }
        }
    }

    protected override void managerExit(){
        
    }

}
