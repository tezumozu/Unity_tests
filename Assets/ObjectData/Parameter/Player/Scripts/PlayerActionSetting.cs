using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionFrameSetting{
    // Preliminary Action Frame.
    public float Preliminary; 

    // Main Action Frame.
    public float Action;

    // Follow Through ActionFrame.
    public float FollowThrough;

    //アクションがキャンセル可能になるまでの時間
    public float CancelFrame;

    // Stamina Cost of Action.
    public float StaminaCost;
}


[CreateAssetMenu(menuName = "ScriptableObject/PlayerAction_Setting", fileName = "PlayerActionSetting")]
public class PlayerActionSetting : ScriptableObject{
    
    public float JumpAceel;
    public float WalkSpeed;
    public float DushSpeed;
    public float AcceptLittleJump;
    public ActionFrameSetting Jump;
    public ActionFrameSetting Landing;
    public ActionFrameSetting NormalAttack_Land;
    public ActionFrameSetting NormalAttack_Air;
    public ActionFrameSetting ChageAttack_Land;
    public ActionFrameSetting ChageAttack_Air;
    public ActionFrameSetting Dush;
    public ActionFrameSetting Dodge;
}

