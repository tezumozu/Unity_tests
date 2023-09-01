using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public abstract class PlayerActionState{
    protected GravityManager gravityManager;
    protected I_2DPlayerUpdatable player;
    protected bool inputStandBy;
    protected bool bufferedInpuitAvailable;

    static protected PlayerDirection playerDirection;
    static protected bool isWalkLeft;
    static protected bool isWalkRight;
    static protected bool isAir;
    static protected InputManager inputManager;

    public bool isInputStandBy {
        get { return inputStandBy; }
    }

    public bool isBufferedInpuitAvailable{
        get { return bufferedInpuitAvailable; }
    }

    static public bool getIsAir {
        get { return isAir; }
    }

    public PlayerActionState (GravityManager gM,I_2DPlayerUpdatable player){
        gravityManager = gM;
        this.player = player;
        inputStandBy = true;
        inputManager = InputManager.instance;
        playerDirection = PlayerDirection.RIGHT;
        isWalkLeft = false;
        isWalkRight = false;
        bufferedInpuitAvailable = false;
    }

    abstract public E_ActionState stateUpdate();
    abstract public void stateInit();
    abstract public void stateTermination();
    abstract public E_ActionState checkInput(E_InputType input);

    static public void toAir(){
        isAir = true;
    }

    static public void landing(){
        isAir = false;
    }
}
