using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public abstract class PlayerActionState{
    protected E_PlayerAction ownState;
    protected GravityManager gravityManager;
    protected I_2DPlayerUpdatable player;
    protected bool inputStandBy;
    protected bool bufferedInpuitAvailable;
    protected bool isFinished;

    static protected E_PlayerDirection playerDirection;
    static protected bool isWalkLeft;
    static protected bool isWalkRight;
    static protected bool isAir;

    static protected float availableInputTime;

    public bool isInputStandBy {
        get { return inputStandBy; }
    }

    public bool isBufferedInpuitAvailable{
        get { return bufferedInpuitAvailable; }
    }

    public bool getIsFinished{
        get { return isFinished; }
    }

    static public bool getIsAir{
        get { return isAir; }
    }


    public PlayerActionState (I_2DPlayerUpdatable player){
        ownState = E_PlayerAction.WAIT; //初期化のため
        gravityManager = new GravityManager();
        this.player = player;
        inputStandBy = true;
        playerDirection = E_PlayerDirection.RIGHT;
        isWalkLeft = false;
        isWalkRight = false;
        bufferedInpuitAvailable = false;
        isFinished = false;
        availableInputTime = 6.0f * 1.0f / 60.0f;
    }


    abstract public E_PlayerAction stateUpdate();

    abstract public void stateEntrance();

    abstract protected E_PlayerAction inputStateTransition(E_InputType input);

    abstract public E_PlayerAction stateExit();

    /*

    abstract protected E_PlayerAction toAir();

    abstract protected E_PlayerAction toLand();
    */

    private E_PlayerAction comebackCheckInput(InputData[] input, int count, E_PlayerAction state){ 
        
        if(count > 0){
            state = comebackCheckInput( input , count-1 , state ); 
            E_PlayerAction nextState = inputStateTransition(input[count-1].type);

            if(ownState != nextState){
                return nextState;
            }

            return state;

        }else{
            return state;
        }

        /*
        */
    }

    private E_PlayerAction comebackCheckInput(InputData[] input, E_PlayerAction state){ 
        return comebackCheckInput(input, input.Length, state);
    }

    public E_PlayerAction checkInput(){
        var nextState = ownState;
        if(isInputStandBy){
            //入力を取得
            var inputData = InputManager.instance.getInputData(availableInputTime);

            //入力があれば
            if(inputData.Length > 0){
                //入力をもとに遷移を確認
                nextState = comebackCheckInput(inputData , nextState);
            }

            //先行入力を確認
            if(isBufferedInpuitAvailable){
                var bufferList = InputManager.instance.getInputBuffer;
                nextState = comebackCheckInput(bufferList , nextState);
                bufferedInpuitAvailable = false;
            }
        }
        return nextState;
    }

}

public enum E_PlayerAction{
    WAIT,
    WALK,
    ATTACK,
    JUMP,
    FALL,
    LANDING,
    DAMAGED
}

public enum E_PlayerUpperAction {
    ATTACK,
    JUMP,
    FALL
}

public enum E_PlayerLowerAction{
    WALK,
    FALL,
    LANDING,
    WAIT
}

public enum E_AnimState {
    RADY,
    ACTION,
    FOLLOW_THROUGH
}

public enum E_PlayerDirection{
    LEFT,
    RIGHT
}
