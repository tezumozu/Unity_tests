using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct S_ActionConfig {
    public float jumpMaxAccel;
    public float warkMaxSpeed;

    public S_ActionConfig(float jump,float wark){
        jumpMaxAccel = jump;
        warkMaxSpeed = wark;
    }

}

