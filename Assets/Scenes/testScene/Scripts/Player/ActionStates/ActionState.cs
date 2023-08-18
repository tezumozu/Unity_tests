using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

abstract public class ActionState {
    
    protected E_AnimState currentAnimState;
    protected static InputManager inputManager;
    protected bool isInputStandBy;
    protected bool isAir;
    protected static PlayerDirection playerDirection;
    protected static bool isMove;

    public ActionState (){
        inputManager = InputManager.instance;
        currentAnimState = E_AnimState.RADY;
        isInputStandBy = true;
        isAir = false;
        isMove = false;
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