using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class Action_Fall : ActionState
{
    float testCurrentFrame;
    const float testFrame = 1.0f;
    public Action_Fall () {
        //遷移を表すマップの作成
        testCurrentFrame = 0.0f;
    }

    override public E_ActionState checkInput(){
        E_ActionState nextState = E_ActionState.FALL;

        if (isInputStandBy){
            //入力確認処理
            var inputData = inputManager.getInputData(6.0f * 1.0f / 60.0f);

            if(inputData.Count < 1){
                return nextState;
            }

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
                    
                    case E_InputType.ATTACK:
                        nextState = E_ActionState.ATTACK;
                        break;

                    case E_InputType.WALK_RIGHT_CANCELED :
                        isWalkRight = false;

                        //左が同時押しされている場合
                        if(isWalkLeft){
                            playerDirection = PlayerDirection.LEFT;
                        }

                        break;

                    default:
                        break;
                }
            }          
        }

        return nextState;  

    }
    

    override public E_ActionState stateUpdate (){
        E_ActionState nextState = E_ActionState.FALL;

        testCurrentFrame += Time.deltaTime;

        //着地 ※テスト用
        if(testCurrentFrame > testFrame){ 
            testCurrentFrame = 0.0f;
            nextState = E_ActionState.LANDING;
            getPlayer.toLand();
        }

        //Debug.Log("FALL:" + testCurrentFrame);

        return nextState;   
    }
}
