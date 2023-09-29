using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
using MyGameManagers;

namespace StateManagement_ver3{
    public class Attack_ActionState : ActionState{

        const E_ActionState ownState = E_ActionState.ATTACK;
        bool isInputStandby;

        float currentFrame;

        E_ActionPhase currentPhase;

        bool isLanding;

        S_ActionFrameConfig config;

        public Attack_ActionState (I_2DPlayerUpdatable player): base(player){
            
        }

        override public void updateState (){

            currentFrame += Time.deltaTime;
            //Debug.Log(currentPhase);

            switch(currentPhase){
                case E_ActionPhase.RADY:
                    //一定時間経過で遷移
                    if(currentFrame > config.rady * 1.0f/60.0f){
                        currentPhase = E_ActionPhase.ACTION;
                        currentFrame = currentFrame - config.rady * 1.0f/60.0f;
                    }
                break;

                case E_ActionPhase.ACTION:
                    //一定時間経過で入力を受け付けるように
                    if(currentFrame > config.action * 1.0f/60.0f){
                        isInputStandby = true;
                        isBufferCheck = true;
                        currentPhase = E_ActionPhase.FOLLOW_THROUGH;
                        currentFrame = currentFrame - config.action * 1.0f/60.0f;
                    }
                break;

                case E_ActionPhase.FOLLOW_THROUGH:
                    //一定時間経過で入力を受け付けるように
                    if(currentFrame > config.followThrough * 1.0f/60.0f){
                        isFinished = true;
                    }
                break;
            }
        }


        override protected S_StateData inputStateTransration(E_InputType input , S_StateData state){

            if(isInputStandby){

                switch (input){

                    case E_InputType.JUMP:
                        if(!state.isAir){
                            state.moveState = E_MoveState.JUMP;
                            isUpdateMove = true;
                        }
                    break;

                    default:
                    break;
                }

                //アクション関係
                switch (input){
                    case E_InputType.JUMP:
                        if(!state.isAir){
                            state.isAir = true;
                            state.actionState = E_ActionState.JUMP;
                            isUpdateAction = true;
                        }
                    break;

                    case E_InputType.ATTACK:
                        isUpdateAction = true;
                    break;

                    case E_InputType.DUSH:
                        isUpdateAction = true;
                    break;

                    default:
                    break;
                }
            
            }

            return state;
        }


        override protected S_StateData bufferStateTransration(E_InputType input , S_StateData state){
            //アクション関係
            switch (input){

                case E_InputType.JUMP:
                    if(!state.isAir){
                        state.moveState = E_MoveState.JUMP;
                        isUpdateMove = true;
                    }
                break;

                default:
                break;
            }

            //アクション関係
            switch (input){
                case E_InputType.JUMP:
                    if(!state.isAir){
                        state.isAir = true;
                        state.actionState = E_ActionState.JUMP;
                        isUpdateAction = true;
                    }
                break;

                case E_InputType.ATTACK:
                    isUpdateAction = true;
                break;

                case E_InputType.DUSH:
                    isUpdateAction = true;
                break;

                default:
                break;
            }
            return state;
        }


        override public S_StateData getNextState(S_StateData state){
            state.actionState = E_ActionState.WAIT;
            if(isLanding){
                state.actionState = E_ActionState.LANDING;
            }

            
            updateAction.OnNext(state.actionState);
            return state;
        }

        override public void stateEnter(S_StateData state){
            Debug.Log("test");
            isInputStandby = false;
            currentFrame = 0.0f;
            currentPhase = E_ActionPhase.RADY;
            isFinished = false;
            isLanding = false;

            //空中なら空中攻撃として処理
            if(state.isAir){
                config = GameManager.instance.getActionConfig.normalAttack_Air;
            }else{
                config = GameManager.instance.getActionConfig.normalAttack_Land;
            }
        }

        //空中攻撃を中断しない
        public override S_StateData landing(S_StateData state){
            state.moveState = E_MoveState.LAND;
            state.isAir = false;
            isLanding = true;

            updateMove.OnNext(state.moveState);
            return state;
        }


    }
}
