using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
using UniRx;

namespace StateManagement_ver3{

    abstract public class ActionState : I_StateUpdatable{

        protected static bool isLeftMove;

        protected static bool isRightMove;

        protected bool isBufferCheck;
        protected bool isFinished;

        protected bool isUpdateAction;
        protected bool isUpdateMove;

        protected static I_PlayerStateUpdatable player;

        //Subject
        protected static Subject<E_MoveState> updateMove;
        protected static Subject<E_ActionState> updateAction;  

        public ActionState (I_PlayerStateUpdatable target){
            isBufferCheck = false;
            isFinished = false;
            isLeftMove = false;
            isRightMove = false;
            isUpdateAction = false;
            isUpdateMove = false;

            if(player == null){
               player = target;
            }

            if(updateAction == null){
                updateAction = new Subject<E_ActionState>();
            }

            if(updateMove == null){
                updateMove = new Subject<E_MoveState>();
            }
        }  

        public S_StateData checkInput( S_StateData state , InputData[] input){

            //バッファのチェックが有効な場合
            if(isBufferCheck){
                input = InputManager.instance.getInputBuffer;
            }

            //入力があるかどうか
            if(input.Length > 0){

                for(int i = 0; i < input.Length; i++){

                    //移動関係の入力処理
                    switch (input[i].type){
                        case E_InputType.WALK_RIGHT_PERFORMED:
                            isRightMove = true;
                            state.playerDirection = E_PlayerDirection.RIGHT;
                            state.isRanning = true;
                        break;


                        case E_InputType.WALK_RIGHT_CANCELED:
                            isRightMove = false;

                            //同時押しされていた場合
                            if(isLeftMove){
                                state.playerDirection = E_PlayerDirection.LEFT;
                            }else{
                                state.isRanning = false;
                            }
                            
                        break;


                        case E_InputType.WALK_LEFT_PERFORMED:
                            isLeftMove = true;
                            state.playerDirection = E_PlayerDirection.LEFT;
                            state.isRanning = true;
                        break;


                        case E_InputType.WALK_LEFT_CANCELED:
                            isLeftMove = false;
                            //同時押しされていた場合
                            if(isRightMove){
                                state.playerDirection = E_PlayerDirection.RIGHT;
                            }else{
                                state.isRanning = false;
                            }
                        break;

                        default:
                        break;
                    }


                    //バッファーを確認する場合
                    //アクション、固有の移動系の遷移
                    if(isBufferCheck){
                        state = bufferStateTransration(input[i].type , state);
                    }else{
                        state = inputStateTransration(input[i].type , state);
                    }

                    if(isUpdateAction){
                        isUpdateAction = false;
                        updateAction.OnNext(state.actionState);
                    }

                    if(isUpdateMove){
                        isUpdateMove = false;
                        updateMove.OnNext(state.moveState);
                    }
                }

            }

            //バッファを確認していた場合
            if(isBufferCheck){
                isBufferCheck = false;
            }

            return state;
        }

        public bool getIsFinished(){
            return isFinished;
        }

        public virtual S_StateData falling(S_StateData state){
            state.moveState = E_MoveState.FALL;
            state.isAir = true;
            
            updateMove.OnNext(state.moveState);
            return state;
        }

        public virtual S_StateData landing(S_StateData state){
            state.actionState = E_ActionState.LANDING;
            state.moveState = E_MoveState.LAND;

            state.isAir = false;
            updateAction.OnNext(state.actionState);
            updateMove.OnNext(state.moveState);
            return state;
        }

        protected abstract S_StateData inputStateTransration(E_InputType input , S_StateData state);
        protected abstract S_StateData bufferStateTransration(E_InputType input , S_StateData state);

        public abstract void updateState();

        public abstract S_StateData getNextState(S_StateData state);

        public abstract void stateEnter(S_StateData state);

        public static void subscribeUpdateAction(Action< E_ActionState > method){
            updateAction.Subscribe(x => method(x));
        }

        public static void subscribeUpdateMove(Action<E_MoveState> method){
            updateMove.Subscribe(x => method(x));
        }

    }
}
