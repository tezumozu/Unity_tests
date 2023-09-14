using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    abstract public class BottomActionState : PlayerActionState{

        protected E_BottomActionState ownState;


        public BottomActionState (I_2DPlayerUpdatable player):base(player){
            ownState = E_BottomActionState.WAIT;
        } 


        public abstract E_BottomActionState stateExit();


        public abstract E_BottomActionState checkInput(E_InputType type);
    }



}
