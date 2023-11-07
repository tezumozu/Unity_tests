using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    abstract public class MoveState{
        private I_PlayerUpdatable player;

        public MoveState (I_PlayerUpdatable target){
            player = target;
        }  

        abstract public void movePlayer();
    }



}
