using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class tempUpState : UpActionState{

        public tempUpState (I_2DPlayerUpdatable player) : base(player){
            ownState = E_UpActionState.WAIT;
        }

        override public void stateUpdate(){

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