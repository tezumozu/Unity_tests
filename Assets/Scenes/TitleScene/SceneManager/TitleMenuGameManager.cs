using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class TitleMenuGameManager : GameManager{
    public TitleMenuGameManager (TitleMenuUIManager uiManager){
        uiManager.StartGameArelt.Subscribe( x => {
            loadSceneAlert(E_GameScene.ActionScene);
        });
    }
    
}
