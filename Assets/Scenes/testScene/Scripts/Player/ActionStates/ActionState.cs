using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

abstract public class ActionState {
    
    protected E_AnimState currentAnimState;
    protected bool isInputStandBy;

    protected static InputManager inputManager;
    protected static PlayerDirection playerDirection;
    protected static bool isAir;
    protected static bool isWalkLeft;
    protected static bool isWalkRight;

    public ActionState (){
        inputManager = InputManager.instance;
        currentAnimState = E_AnimState.RADY;
        isInputStandBy = true;
        isAir = false;
        isWalkLeft = false;
        isWalkRight = false;
        playerDirection = PlayerDirection.LEFT;
    }

    // Start is called before the first frame update
    virtual public void init(){
        
    }

    // Update is called once per frame
    virtual public E_ActionState  stateUpdate(){
        return E_ActionState.WALK;
    }

    //入力確認
    virtual public E_ActionState checkInput(){
        return E_ActionState.WALK;
    }

}

public enum E_AnimState {
    RADY,
    ACTION,
    FOLLOW_THROUGH
}

public enum E_ActionState {//アニメーション単位
    WAIT,
    WALK,
    ATTACK,
    CHARGE_ATTACK,
    JUMP,
    DUDGE,
    GUARD,
    DUSH
}

public enum PlayerDirection{
    LEFT,
    RIGHT
}