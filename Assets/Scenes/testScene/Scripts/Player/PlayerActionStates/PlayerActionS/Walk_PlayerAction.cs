using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class Walk_PlayerAction : PlayerActionState{

    private const float moveDistance = 1.0f;

    public Walk_PlayerAction (GravityManager gM, I_2DPlayerUpdatable player) : base(gM,player){
        
    }

    public override E_ActionState stateUpdate(){
        E_ActionState nextState = E_ActionState.WAIT;
        Debug.Log("WALK : " + playerDirection);

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


    public override void stateInit(){

    }


    public override void stateTermination(){

    }


    public override E_ActionState checkInput(E_InputType input){
        E_ActionState nextState = E_ActionState.WALK;

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
                    nextState = E_ActionState.WALK;
                }else{
                    nextState = E_ActionState.WAIT;
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
                    nextState = E_ActionState.WALK;
                }else{
                    nextState = E_ActionState.WAIT;
                }
                break;
                        

            /*
            case E_InputType.JUMP:
                nextState = E_ActionState.JUMP;
                PlayerActinState.toAir();
                break;

            case E_InputType.ATTACK:
                nextState = E_ActionState.ATTACK;
                break;
                */
            
            default:
                break;

        }

        return nextState;
    }
}
