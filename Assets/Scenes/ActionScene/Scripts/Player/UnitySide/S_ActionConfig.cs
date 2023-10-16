using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct S_ActionConfig {
    public float jumpMaxAccel;
    public float walkMaxSpeed;
    public float littleJump;
    public S_ActionFrameConfig landing;
    public S_ActionFrameConfig normalAttack_Air;
    public S_ActionFrameConfig normalAttack_Land;
    public S_ActionFrameConfig chargeAttack_Air;
    public S_ActionFrameConfig chargeAttack_Land;
    public S_ActionFrameConfig dush;
    public S_ActionFrameConfig dodge;
}

[System.Serializable]
public struct S_ActionFrameConfig{
    public float rady;
    public float action;
    public float followThrough;
}
