using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public class Wait_PlayerAction : ActionState{

        const E_ActionState ownState = E_ActionState.WAIT;


        override public void updateState (){
            //Debug.Log("WAIT");
        }


        override protected S_StateData inputStateTransration(E_InputType input , S_StateData state){
            return state;
        }


        override protected S_StateData bufferStateTransration(E_InputType input , S_StateData state){
            return state;
        }


        override public S_StateData getNextState(S_StateData state){
            return state;
        }


        override public void stateEnter(){

        }
    }
}
