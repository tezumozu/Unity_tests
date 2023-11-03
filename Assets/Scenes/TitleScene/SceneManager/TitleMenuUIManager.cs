using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;

using UniRx;

public class TitleMenuUIManager {

    public Subject<Unit> StartGameArelt {get; private set;}

    public TitleMenuUIManager(){
        StartGameArelt = new Subject<Unit>();
    }

    public void UpdateUI(InputData[] inputs){
        for(int i = 0; i < inputs.Length; i++){
            Debug.Log(inputs[i].type);

            if(inputs[i].type == E_InputType.DECIDE){
                StartGameArelt.OnNext(Unit.Default);
            }
        }

    }
}
