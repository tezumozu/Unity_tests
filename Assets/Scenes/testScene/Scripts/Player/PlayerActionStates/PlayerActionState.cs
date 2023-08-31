using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public abstract class PlayerActionState{
    protected GravityManager gravityManager;
    protected bool inputStandBy;

    static protected PlayerDirection playerDirection;
    static protected bool isWalkLeft;
    static protected bool isWalkRight;

    static protected InputManager inputManager;

    public bool isInputStandBy {
        get { return inputStandBy;}
    }

    public PlayerActionState (GravityManager gM){
        gravityManager = gM;
        inputStandBy = true;
        inputManager = InputManager.instance;
        playerDirection = PlayerDirection.RIGHT;
        isWalkLeft = false;
        isWalkRight = false;
    }

    abstract public E_ActionState stateUpdate();
    abstract public void stateInit();
    abstract public void stateTermination();
    abstract public E_ActionState checkInput();
    abstract public E_ActionState checkBuffer();
    abstract protected E_ActionState inputChecker(E_InputType input);
}
