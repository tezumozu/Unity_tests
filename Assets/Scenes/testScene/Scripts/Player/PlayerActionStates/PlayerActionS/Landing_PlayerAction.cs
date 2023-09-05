using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class Landing_PlayerAction : PlayerActionState{
    
    float testCurrentTime;

    public Landing_PlayerAction (I_2DPlayerUpdatable player) : base(player){
        ownState = E_PlayerAction.LANDING;
        testCurrentTime = 0.0f;
    }

    public override E_PlayerAction stateUpdate(){
        E_PlayerAction nextState = E_PlayerAction.LANDING;
        Debug.Log("LANDING");

        testCurrentTime += Time.deltaTime;

        Debug.Log(testCurrentTime);
        if(testCurrentTime > 10.0f * 1.0f / 60.0f){
            nextState = E_PlayerAction.WAIT;
            isFinished = true;
        }

        return nextState;
    }


    public override void stateEntrance(){
        bufferedInpuitAvailable = false;
        isFinished = false;
        inputStandBy = true;
        isAir = false;

        //テスト用
        testCurrentTime = 0.0f;
    }


    public override E_PlayerAction stateExit(){
        return E_PlayerAction.WAIT;
    }

    protected override E_PlayerAction toLand(){
        return E_PlayerAction.LANDING;
    }

    protected override E_PlayerAction toAir(){
        return E_PlayerAction.FALL;
    }

    protected override E_PlayerAction inputStateTransition(E_InputType input){
        E_PlayerAction nextState = E_PlayerAction.LANDING;

        switch (input){
            case E_InputType.WALK_LEFT_PERFORMED:
                playerDirection = PlayerDirection.LEFT;
                isWalkLeft = true;
                nextState = E_PlayerAction.WALK;

            break;


            case E_InputType.WALK_RIGHT_PERFORMED:
                playerDirection = PlayerDirection.RIGHT;
                isWalkRight = true;
                nextState = E_PlayerAction.WALK;

            break;


            case E_InputType.JUMP:
                nextState = E_PlayerAction.JUMP;
                toAir();

            break;


            case E_InputType.ATTACK:
                nextState = E_PlayerAction.ATTACK;
            
            break;


                /*
                    case E_InputType.CHARGE_ATTACK_PEFORMED:
                        nextState = E_PlayerAction.CHARGE_ATTACK;
                        break;

                    case E_InputType.DUSH:
                        nextState = E_PlayerAction.DUSH;
                        break;

                    case E_InputType.GUARD_PEFORMED:
                        nextState = E_PlayerAction.GUARD;
                        break;
                */
            default:
                break;
        }

        return nextState;
    }

}
