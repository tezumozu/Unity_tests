using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class Action_Landing : ActionState{
    const float landingFrame =  1.0f;
    float currentFrame;

    bool isBufferCheck;

    public Action_Landing () {
        //遷移を表すマップの作成
        currentFrame = 0.0f;
        isBufferCheck = true;
    }

    public override void resetState(){
        currentFrame = 0.0f;
        isBufferCheck = true;    
    } 


    override public E_ActionState checkInput(){
        E_ActionState nextState = E_ActionState.LANDING;

        if (isInputStandBy){
            //入力確認処理
            var inputData = inputManager.getInputData(6.0f * 1.0f / 60.0f);

            //入力あれば
            if(inputData.Count > 0){

                foreach(var data in inputData) {
                    switch (data.type){
                        case E_InputType.WALK_LEFT_PERFORMED :
                            playerDirection = PlayerDirection.LEFT;
                            isWalkLeft = true;
                            break;


                        case E_InputType.WALK_LEFT_CANCELED :
                            isWalkRight = false;
                            nextState = E_ActionState.WAIT;

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
                            nextState = E_ActionState.JUMP;
                            getPlayer.toAir();
                            resetState();
                            break;
                        
                        case E_InputType.ATTACK:
                            nextState = E_ActionState.ATTACK;
                            resetState();
                            break;
                    /*
                        case E_InputType.ATTACK:
                            nextState = E_ActionState.ATTACK;
                            break;

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
                }

            }

            
            //先行入力の確認
            if(isBufferCheck && nextState == E_ActionState.LANDING ){
                var bufferList = inputManager.getInputBuffer;
                Debug.Log(bufferList.Length);
                for (int i = 0; i < bufferList.Length; i++){
                    switch (bufferList[i].type){
                        case E_InputType.JUMP:
                        Debug.Log("test");
                        nextState = E_ActionState.JUMP;
                        getPlayer.toAir();
                        resetState();
                        break;
                    
                    case E_InputType.ATTACK:
                        nextState = E_ActionState.ATTACK;
                        resetState();
                        break;
                    
                    default:
                        break;
                    }
                }
            }

            isBufferCheck = false;
        }

        return nextState;  
    }

    override public E_ActionState stateUpdate (){
        E_ActionState nextState = E_ActionState.LANDING;

        currentFrame += Time.deltaTime;

        if (currentFrame > landingFrame){
            nextState = E_ActionState.WAIT;
            resetState();

            //移動が入力されている場合　
            if(isWalkLeft || isWalkRight){
                nextState = E_ActionState.WALK;
            }
        }

        

        return nextState;
    }
}
