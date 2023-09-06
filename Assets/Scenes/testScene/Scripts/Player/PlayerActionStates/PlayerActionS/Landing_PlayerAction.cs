using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class Landing_PlayerAction : PlayerActionState{
    
    float testCurrentTime;
    private const float moveDistance = 5.0f;

    public Landing_PlayerAction (I_2DPlayerUpdatable player) : base(player){
        ownState = E_PlayerAction.LANDING;
        testCurrentTime = 0.0f;
    }

    public override E_PlayerAction stateUpdate(){
        E_PlayerAction nextState = E_PlayerAction.LANDING;
        //Debug.Log("LANDING");

        testCurrentTime += Time.deltaTime;

        if(testCurrentTime > 2.0f * 1.0f / 60.0f){
            nextState = E_PlayerAction.WAIT;

            if(isWalkLeft||isWalkRight){
                nextState = E_PlayerAction.WALK;
            }

            isFinished = true;
        }

        Vector3 moveVec = new Vector2 (0.0f,0.0f);

        //左右への移動
        if(isWalkLeft||isWalkRight){
            if(playerDirection == PlayerDirection.LEFT){
                moveVec.x = -moveDistance * Time.deltaTime;
            }else{
                moveVec.x = moveDistance * Time.deltaTime;
            }

        }

        //移動する
        player.addPos(moveVec);

        Debug.Log(nextState);
        return nextState;
    }


    public override void stateEntrance(){
        bufferedInpuitAvailable = true;
        isFinished = false;
        inputStandBy = true;
        isAir = false;

        //テスト用
        testCurrentTime = 0.0f;
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
        E_PlayerAction nextState = E_PlayerAction.LANDING;

        switch (input){
            case E_InputType.WALK_LEFT_PERFORMED:
                playerDirection = PlayerDirection.LEFT;
                isWalkLeft = true;

                break;


            case E_InputType.WALK_LEFT_CANCELED :
                isWalkLeft = false;

                //右が同時押しされている場合
                if(isWalkRight){
                    playerDirection = PlayerDirection.RIGHT;
                }
                break;


            case E_InputType.WALK_RIGHT_PERFORMED:
                playerDirection = PlayerDirection.RIGHT;
                isWalkRight = true;
                break;
                    

            case E_InputType.WALK_RIGHT_CANCELED :
                isWalkRight = false;

                //左が同時押しされている場合
                if(isWalkLeft){
                    playerDirection = PlayerDirection.LEFT;
                }

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
