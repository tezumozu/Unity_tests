using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyInputSystems {
    public class TitleInput : InputMode{
        private Dictionary < E_InputType , bool> isHoldDic;
        private InputManager inputManager;
        //private E_InputType currentMoveDirection;

        override public void init() {

            //inputSystemを取得
            inputManager = InputManager.instance;

            //ホールドの状態管理用辞書の初期化
            isHoldDic = new Dictionary < E_InputType , bool>();
            isHoldDic.Add(E_InputType.CURSOR_MOVE_UP_PERFORMED,false);
            isHoldDic.Add(E_InputType.CURSOR_MOVE_DOWN_PERFORMED,false);
            isHoldDic.Add(E_InputType.CURSOR_MOVE_LEFT_PERFORMED,false);
            isHoldDic.Add(E_InputType.CURSOR_MOVE_RIGHT_PERFORMED,false);

            isActive = true;                   
        }


        override public void inputUpdate() {
            /*移動ボタン同時押しも考慮
            if(isHoldDic[E_InputType.WALK_LEFT] || isHoldDic[E_InputType.WALK_RIGHT]){
                inputManager.setInputData(currentMoveDirection);
            }

           if(isHoldDic[E_InputType.CHARGE_ATTACK]){
                inputManager.setInputData(E_InputType.CHARGE_ATTACK);

           }else if(isHoldDic[E_InputType.GUARD]){
                inputManager.setInputData(E_InputType.GUARD);
           }*/
        }


        //カーソル移動・UP
        public void CursorMoveButtonUp (InputAction.CallbackContext context){
            if(context.started || !isActive) return;

            if(context.performed){
                isHoldDic[E_InputType.CURSOR_MOVE_UP_PERFORMED] = true;
                inputManager.setInputData(E_InputType.CURSOR_MOVE_UP_PERFORMED);

            } else if(context.canceled){
                isHoldDic[E_InputType.CURSOR_MOVE_UP_PERFORMED] = false;
                inputManager.setInputData(E_InputType.CURSOR_MOVE_UP_CANCELED);
            }
        }


        //カーソル移動・DOWN
        public void CursorMoveButtonDown (InputAction.CallbackContext context){
            if(context.started || !isActive) return;

            if(context.performed){
                isHoldDic[E_InputType.CURSOR_MOVE_DOWN_PERFORMED] = true;
                inputManager.setInputData(E_InputType.CURSOR_MOVE_DOWN_PERFORMED);

            } else if(context.canceled){
                isHoldDic[E_InputType.CURSOR_MOVE_DOWN_PERFORMED] = false;
                inputManager.setInputData(E_InputType.CURSOR_MOVE_DOWN_CANCELED);
            }
        }


        //カーソル移動・LEFT
        public void CursorMoveButtonLeft (InputAction.CallbackContext context){
            if(context.started || !isActive) return;

            if(context.performed){
                isHoldDic[E_InputType.CURSOR_MOVE_LEFT_PERFORMED] = true;
                inputManager.setInputData(E_InputType.CURSOR_MOVE_LEFT_PERFORMED);

            } else if(context.canceled){
                isHoldDic[E_InputType.CURSOR_MOVE_LEFT_PERFORMED] = false;
                inputManager.setInputData(E_InputType.CURSOR_MOVE_LEFT_CANCELED);
            }
        }


        //カーソル移動・RIGHT
        public void CursorMoveButtonRight (InputAction.CallbackContext context){
            if(context.started || !isActive) return;
                
            if(context.performed){
                isHoldDic[E_InputType.CURSOR_MOVE_RIGHT_PERFORMED] = true;
                inputManager.setInputData(E_InputType.CURSOR_MOVE_RIGHT_PERFORMED);

            } else if(context.canceled){
                isHoldDic[E_InputType.CURSOR_MOVE_RIGHT_PERFORMED] = false;
                inputManager.setInputData(E_InputType.CURSOR_MOVE_RIGHT_CANCELED);
            }
        }


        //カーソル移動・スティック
        public void CursorMoveStick (InputAction.CallbackContext context){
            if(!isActive) return;

            Vector2 stickVec = context.ReadValue<Vector2>();

            //ホールド時
            if(isHoldDic[E_InputType.CURSOR_MOVE_LEFT_PERFORMED] || isHoldDic[E_InputType.CURSOR_MOVE_RIGHT_PERFORMED] || isHoldDic[E_InputType.CURSOR_MOVE_UP_PERFORMED] || isHoldDic[E_InputType.CURSOR_MOVE_DOWN_PERFORMED] ){
                if(Vector2.Distance(stickVec, Vector2.zero) <= 0.5){
                    isHoldDic[E_InputType.CURSOR_MOVE_LEFT_PERFORMED] = false;
                    inputManager.setInputData(E_InputType.CURSOR_MOVE_LEFT_CANCELED);
                    isHoldDic[E_InputType.CURSOR_MOVE_RIGHT_PERFORMED] = false;
                    inputManager.setInputData(E_InputType.CURSOR_MOVE_RIGHT_CANCELED);
                    isHoldDic[E_InputType.CURSOR_MOVE_UP_PERFORMED] = false;
                    inputManager.setInputData(E_InputType.CURSOR_MOVE_UP_CANCELED);
                    isHoldDic[E_InputType.CURSOR_MOVE_DOWN_PERFORMED] = false;
                    inputManager.setInputData(E_InputType.CURSOR_MOVE_DOWN_CANCELED);
                }

            //未入力時
            }else{
                if(Vector2.Distance(stickVec, Vector2.zero) > 0.5){
                    //向きを確認
                    if(Vector2.Dot(stickVec,Vector2.one) > 0){
                        if(Vector2.Dot(stickVec,Vector2.left + Vector2.up) > 0){ 
                            //上を向いている
                            isHoldDic[E_InputType.CURSOR_MOVE_UP_PERFORMED] = true;
                            inputManager.setInputData(E_InputType.CURSOR_MOVE_UP_PERFORMED);

                        } else {
                            //右を向いている
                            isHoldDic[E_InputType.CURSOR_MOVE_RIGHT_PERFORMED] = true;
                            inputManager.setInputData(E_InputType.CURSOR_MOVE_RIGHT_PERFORMED);

                        }

                    }else{
                        if(Vector2.Dot(stickVec,Vector2.left + Vector2.up) > 0){ 
                            //左を向いている
                            isHoldDic[E_InputType.CURSOR_MOVE_LEFT_PERFORMED] = true;
                            inputManager.setInputData(E_InputType.CURSOR_MOVE_LEFT_PERFORMED);

                        } else {
                            //下を向いている
                            isHoldDic[E_InputType.CURSOR_MOVE_DOWN_PERFORMED] = true;
                            inputManager.setInputData(E_InputType.CURSOR_MOVE_DOWN_PERFORMED);

                        }
                    }
                }
                
            }
        }


         //決定
        public void DecideInputs (InputAction.CallbackContext context){
            if(context.started || context.canceled || !isActive) return;

            if(context.performed){
                inputManager.setInputData(E_InputType.DECIDE);
            }
        }


        //キャンセル
        public void CancelInputs (InputAction.CallbackContext context){
            if(context.started || context.canceled || !isActive) return;

            if(context.performed){
                inputManager.setInputData(E_InputType.CANCEL);
            }
        }

    }
}
