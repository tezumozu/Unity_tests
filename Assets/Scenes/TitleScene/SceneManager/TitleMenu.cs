using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;

public class TitleMenu : GameMode{

    public TitleMenu(){
        
    }   

    public override void ObjectInit(){
        var loadResult = GenericLoader<InputMode,E_InputMode>.loadAsetResouse(E_InputMode.IM_Title);
        inputMode = loadResult.GetAwaiter().GetResult();
    }

    public override void ManagerUpdate(InputData[] inputs){
        if(true){

        }
    }

    public override void UIUpdate(InputData[] inputs){

    }
}
