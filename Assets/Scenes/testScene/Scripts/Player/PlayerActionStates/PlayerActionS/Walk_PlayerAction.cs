using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class Walk_PlayerAction : PlayerActionState{

    private const float moveDistance = 10.0f;

    public Walk_PlayerAction (I_2DPlayerUpdatable player) : base(player){
        ownState = E_PlayerAction.WALK;
    }

    public override E_PlayerAction stateUpdate(){
        E_PlayerAction nextState = E_PlayerAction.WALK;
        //Debug.Log("WALK : " + playerDirection);

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
        E_PlayerAction nextState = E_PlayerAction.WALK;

        switch (input){
            case E_InputType.WALK_LEFT_PERFORMED :
                playerDirection = PlayerDirection.LEFT;
                isWalkLeft = true;
                break;


            case E_InputType.WALK_LEFT_CANCELED :
                isWalkLeft = false;

                //右が同時押しされている場合
                if(isWalkRight){
                    playerDirection = PlayerDirection.RIGHT;
                    nextState = E_PlayerAction.WALK;
                }else{
                    nextState = E_PlayerAction.WAIT;
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
                    nextState = E_PlayerAction.WALK;
                }else{
                    nextState = E_PlayerAction.WAIT;
                }
                break;
                        

            case E_InputType.JUMP:
                nextState = E_PlayerAction.JUMP;
                break;

            /*
            case E_InputType.ATTACK:
                nextState = E_PlayerAction.ATTACK;
                break;
                */
            
            default:
                break;

        }

        return nextState;
    }
}
