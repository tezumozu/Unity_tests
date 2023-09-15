using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class Wait_BottomPlayerAction : BottomActionState {
        public Wait_BottomPlayerAction (I_2DPlayerUpdatable player) : base(player){
            ownState = E_BottomActionState.WAIT;
        }


        override public void updateState(){

        }


        override public void stateEnter(){

        }


        override public E_BottomActionState stateExit(){
            return ownState;
        }


        override public E_BottomActionState checkInput(E_InputType type){
            return ownState;
        }
    }
}
