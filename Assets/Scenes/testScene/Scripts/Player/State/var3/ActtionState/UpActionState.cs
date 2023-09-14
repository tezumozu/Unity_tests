using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    abstract public class UpActionState : PlayerActionState{

        protected E_UpActionState ownState;


        public UpActionState (I_2DPlayerUpdatable player):base(player){
            ownState = E_UpActionState.WAIT;
        } 


        abstract public E_UpActionState stateExit();

        
        abstract public E_UpActionState checkInput(E_InputType type);


    }



}
