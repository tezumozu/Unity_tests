using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct S_ActionConfig {
    public float jumpMaxAccel;
    public float walkMaxSpeed;
    public S_ActionFrameConfig landing;
    public S_ActionFrameConfig attack;
}

[System.Serializable]
public struct S_ActionFrameConfig{
    public float rady;
    public float action;
    public float followThrough;
}
