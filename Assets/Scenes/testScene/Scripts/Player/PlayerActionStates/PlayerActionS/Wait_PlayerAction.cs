using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class Wait_PlayerAction : PlayerActionState{

    public Wait_PlayerAction (GravityManager gM, I_2DPlayerUpdatable player) : base(gM,player){
        
    }

    public override E_ActionState stateUpdate(){
        E_ActionState nextState = E_ActionState.WAIT;
        Debug.Log("WAIT");

        return nextState;
    }


    public override void stateInit(){

    }


    public override void stateTermination(){

    }


    public override E_ActionState checkInput(E_InputType input){
        E_ActionState nextState = E_ActionState.WAIT;

        switch (input){
            case E_InputType.WALK_LEFT_PERFORMED:
                playerDirection = PlayerDirection.LEFT;
                isWalkLeft = true;
                nextState = E_ActionState.WALK;

            break;


            case E_InputType.WALK_RIGHT_PERFORMED:
                playerDirection = PlayerDirection.RIGHT;
                isWalkRight = true;
                nextState = E_ActionState.WALK;

            break;


            case E_InputType.JUMP:
                nextState = E_ActionState.JUMP;
                PlayerActionState.toAir();

            break;


            case E_InputType.ATTACK:
                nextState = E_ActionState.ATTACK;
            
            break;


                /*
                    case E_InputType.CHARGE_ATTACK_PEFORMED:
                        nextState = E_ActionState.CHARGE_ATTACK;
                        break;

                    case E_InputType.DUSH:
                        nextState = E_ActionState.DUSH;
                        break;

                    case E_InputType.GUARD_PEFORMED:
                        nextState = E_ActionState.GUARD;
                        break;
                */
            default:
                break;
        }

        return nextState;
    }

}
