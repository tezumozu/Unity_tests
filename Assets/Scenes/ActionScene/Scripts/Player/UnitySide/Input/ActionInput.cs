using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyInputSystems {
    public class ActionInput : InputMode{
        private Dictionary < E_InputType , bool> isHoldDic;
        private InputManager inputManager;
        //private E_InputType currentMoveDirection;

        override public void init() {

            //inputSystemを取得
            inputManager = InputManager.instance;

            //ホールドの状態管理用辞書の初期化
            isHoldDic = new Dictionary < E_InputType , bool>();
            isHoldDic.Add(E_InputType.WALK_LEFT,false);
            isHoldDic.Add(E_InputType.WALK_RIGHT,false);
            isHoldDic.Add(E_InputType.GUARD,false);
            isHoldDic.Add(E_InputType.CHARGE_ATTACK,false);     

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


        //移動
        public void walk_Button_Left (InputAction.CallbackContext context){
            if(context.started || !isActive) return;

            if(context.performed){
                //currentMoveDirection = E_InputType.WALK_LEFT;
                isHoldDic[E_InputType.WALK_LEFT] = true;
                inputManager.setInputData(E_InputType.WALK_LEFT_PERFORMED);

            } else if(context.canceled){
                //currentMoveDirection = E_InputType.WALK_RIGHT;
                isHoldDic[E_InputType.WALK_LEFT] = false;
                inputManager.setInputData(E_InputType.WALK_LEFT_CANCELED);
            }
        }

        public void walk_Button_Right (InputAction.CallbackContext context){
            if(context.started || !isActive) return;
                
            if(context.performed){
                //currentMoveDirection = E_InputType.WALK_RIGHT;
                isHoldDic[E_InputType.WALK_RIGHT] = true;
                inputManager.setInputData(E_InputType.WALK_RIGHT_PERFORMED);

            } else if(context.canceled){
                //currentMoveDirection = E_InputType.WALK_LEFT;
                isHoldDic[E_InputType.WALK_RIGHT] = false;
                inputManager.setInputData(E_InputType.WALK_RIGHT_CANCELED);
            }
        }

        public void walk_Stick (InputAction.CallbackContext context){
            if(!isActive) return;

            //ホールド時
            if(isHoldDic[E_InputType.WALK_LEFT] || isHoldDic[E_InputType.WALK_RIGHT]){
                if(System.Math.Abs(context.ReadValue<float>()) <= 0.5){
                    isHoldDic[E_InputType.WALK_RIGHT] = false;
                    inputManager.setInputData(E_InputType.WALK_RIGHT_CANCELED);
                    isHoldDic[E_InputType.WALK_LEFT] = false;
                    inputManager.setInputData(E_InputType.WALK_LEFT_CANCELED);
                }

            //未入力時
            }else{
                if(System.Math.Abs(context.ReadValue<float>()) > 0.5){
                    //向きを確認
                    if(context.ReadValue<float>() < 0){
                        //currentMoveDirection = E_InputType.WALK_LEFT;
                        isHoldDic[E_InputType.WALK_LEFT] = true;
                        inputManager.setInputData(E_InputType.WALK_LEFT_PERFORMED);

                    }else{
                        //currentMoveDirection = E_InputType.WALK_RIGHT;
                        isHoldDic[E_InputType.WALK_RIGHT] = true;
                        inputManager.setInputData(E_InputType.WALK_RIGHT_PERFORMED);

                    }
                }
                
            }
        }


         //ジャンプ・小ジャン
        public void JumpInputs (InputAction.CallbackContext context){
            if(context.started || !isActive) return;

            if(context.performed){
                inputManager.setInputData(E_InputType.JUMP);
            }
        }

        public void littleJumpInputs (InputAction.CallbackContext context){
            if(context.started || !isActive) return;

            if(context.performed){
                inputManager.setInputData(E_InputType.LITTLE_JUMP);
            }
        }


        //攻撃
        public void attackInputs (InputAction.CallbackContext context){
            if(context.started || !isActive) return;

            if(context.performed){
                inputManager.setInputData(E_InputType.ATTACK);
            }
        }


        //溜め攻撃
        public void chargeAttackInputs (InputAction.CallbackContext context){

            if(context.started || !isActive) return;

            if(context.performed){
                isHoldDic[E_InputType.CHARGE_ATTACK] = true;
                inputManager.setInputData(E_InputType.CHARGE_ATTACK_PEFORMED);

            }else if(context.canceled){
                isHoldDic[E_InputType.CHARGE_ATTACK] = false;
                inputManager.setInputData(E_InputType.CHARGE_ATTACK_CANCELED);

            }
        }


        //ダッシュ
        public void dashInputs (InputAction.CallbackContext context){
            if(context.started || !isActive) return;

            if(context.performed){
                inputManager.setInputData(E_InputType.DUSH);
            }
        }


        //カメラ <---> アクション 切り替え
        public void cameraChangeInputs (InputAction.CallbackContext context){
            if(context.started || !isActive) return;

            if(context.performed){
                inputManager.setInputData(E_InputType.CAMERA_CHANGE);
            }
        }

        //ガード
        public void guardInputs (InputAction.CallbackContext context){
            if(context.started || !isActive) return;

            if(context.performed){
                isHoldDic[E_InputType.GUARD] = true;
                inputManager.setInputData(E_InputType.GUARD_PEFORMED);

            }else if(context.canceled){
                isHoldDic[E_InputType.GUARD] = false;
                inputManager.setInputData(E_InputType.GUARD_CANCELED);

            }
        }

         //ポーズ
        public void pouseInputs (InputAction.CallbackContext context){
            if(context.started || !isActive) return;

            if(context.performed){
                inputManager.setInputData(E_InputType.POUSE);
            }
        }
    }
}
