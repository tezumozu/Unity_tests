using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class Wait_PlayerAction : PlayerActionState{

    public Wait_PlayerAction (I_2DPlayerUpdatable player) : base(player){
        ownState = E_PlayerAction.WAIT;
    }

    public override E_PlayerAction stateUpdate(){
        E_PlayerAction nextState = E_PlayerAction.WAIT;
        //Debug.Log("WAIT");

        return nextState;
    }


    public override void stateEntrance(){
        bufferedInpuitAvailable = false;
        isFinished = false;
        inputStandBy = true;
    }


    public override E_PlayerAction stateExit(){
        return ownState;
    }

/*
    protected override E_PlayerAction toLand(){
        return E_PlayerAction.LANDING;
    }

    protected override E_PlayerAction toAir(){
        return E_PlayerAction.FALL;
    }
*/
    protected override E_PlayerAction inputStateTransition(E_InputType input){
        E_PlayerAction nextState = E_PlayerAction.WAIT;
        switch (input){
            case E_InputType.WALK_LEFT_PERFORMED:
                playerDirection = E_PlayerDirection.LEFT;
                isWalkLeft = true;
                nextState = E_PlayerAction.WALK;

            break;


            case E_InputType.WALK_RIGHT_PERFORMED:
                playerDirection = E_PlayerDirection.RIGHT;
                isWalkRight = true;
                nextState = E_PlayerAction.WALK;

            break;


            case E_InputType.JUMP:
                nextState = E_PlayerAction.JUMP;

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
