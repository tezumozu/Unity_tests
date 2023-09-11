using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class Attack_PlayerAction : PlayerActionState{

    E_AnimState currentState;
    float currentTime;

    public Attack_PlayerAction (I_2DPlayerUpdatable player) : base(player){
        ownState = E_PlayerAction.ATTACK;
    }


    public override E_PlayerAction stateUpdate(){
        E_PlayerAction nextState = E_PlayerAction.ATTACK;
        currentTime += Time.deltaTime;

        //アニメーションの状態ごとに処理
        switch (currentState) {
            case E_AnimState.RADY :

                //一定フレーム経過したら
                if(currentTime > 3.0f * GameValue.g_FrameTime){
                    player.setPlayerDirection(playerDirection);
                    player.attack(isAir);
                    currentState = E_AnimState.ACTION;
                    currentTime -= 3.0f * GameValue.g_FrameTime;
                }

                break;
            
            case E_AnimState.ACTION :
                //一定フレーム経過したら
                if(currentTime > 10.0f * GameValue.g_FrameTime){
                    inputStandBy = true; 
                    bufferedInpuitAvailable = true;
                    currentState = E_AnimState.FOLLOW_THROUGH;
                    currentTime -= 10.0f * GameValue.g_FrameTime;
                }
                break;

            case E_AnimState.FOLLOW_THROUGH :

                if(isWalkLeft||isWalkRight){
                    nextState = E_PlayerAction.WALK;
                }

                if(currentTime > 5.0f * GameValue.g_FrameTime){
                    isFinished = true;
                }

                break;
        }
        return nextState;
    }


    public override void stateEntrance(){
        bufferedInpuitAvailable = false;
        isFinished = false;
        inputStandBy = false;
        currentState  = E_AnimState.RADY;
        currentTime = 0.0f;
    }


    public override E_PlayerAction stateExit(){
        E_PlayerAction nextState;

        nextState = E_PlayerAction.WAIT;

        if(isWalkLeft||isWalkRight){
            nextState = E_PlayerAction.WALK;
        }

        return nextState;
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
