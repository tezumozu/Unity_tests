using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class Wait_UpPlayerAction : UpActionState{

        public Wait_UpPlayerAction (I_2DPlayerUpdatable player) : base(player){
            ownState = E_UpActionState.WAIT;
        }


        override public void updateState(){

        }


        override public void stateEnter(){

        }


        override public E_UpActionState stateExit(){
            return ownState;
        }


        override public E_UpActionState checkInput(E_InputType type){
            return ownState;
        }

    }
}
