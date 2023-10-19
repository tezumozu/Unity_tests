using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : GenericSingletonObject<PlayerData>{

    public int Life {
        get { return Life; }
        private set { Life = value; }
    }

    public E_Stage CurrentStage {
        get { return CurrentStage; }
        private set { CurrentStage = value; }
    }

    private Dictionary<E_GameFlag , bool>  FlagList;

    public bool GetIsFlag (E_GameFlag flag) {
        return FlagList[flag];
    }

    public override void OnInitialize(){
        Life = 3;
        CurrentStage = E_Stage.ZUNDA;
    }
}
