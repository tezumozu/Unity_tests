using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateManagement_ver3{
    public class PlayerStateManager {
        private Dictionary<E_UpActionState,UpActionState> upActionStateMap;
        private Dictionary<E_BottomActionState,BottomActionState> bottomActionStateMap;
        private ActionStateData currentState;
        private I_2DPlayerUpdatable player;

        public PlayerStateManager(I_2DPlayerUpdatable player){
            //マップの初期化
            upActionStateMap = new Dictionary<E_UpActionState, UpActionState>();
            bottomActionStateMap = new Dictionary<E_BottomActionState, BottomActionState>();


            //UPState
            upActionStateMap[E_UpActionState.WAIT] = new Wait_UpPlayerAction(player);


            //BottomState
            bottomActionStateMap[E_BottomActionState.WAIT] = new Wait_BottomPlayerAction(player);


            //状態管理を初期化
            currentState = new ActionStateData(E_UpActionState.WAIT,E_BottomActionState.WAIT);
        }


        public void updateState(){
            
            ActionStateData nextState = currentState;

            //着地確認

            //入力確認
            nextState = checkInput(nextState);

            //状態が変化していたら
            //up
            if(nextState.up != currentState.up){
                upActionStateMap[nextState.up].stateEnter();
            }

            //bottom
            if(nextState.bottom != currentState.bottom){
                bottomActionStateMap[nextState.bottom].stateEnter();
            }

            //状態の更新
            upActionStateMap[nextState.up].updateState();
            bottomActionStateMap[nextState.bottom].updateState();

            //状態の終了を確認
            //up
            if(upActionStateMap[nextState.up].getIsFinished){
                //終了していたら
                nextState.up = upActionStateMap[nextState.up].stateExit();
                upActionStateMap[nextState.up].stateEnter();
                
            }
            //bottom
            if(bottomActionStateMap[nextState.bottom].getIsFinished){
                //終了していたら
                nextState.bottom = bottomActionStateMap[nextState.bottom].stateExit();
                bottomActionStateMap[nextState.bottom].stateEnter();
            }

            //currentStateを更新
            currentState = nextState;
        }


        private ActionStateData checkInput(ActionStateData stateData){
            return stateData;
        }

    }
}
