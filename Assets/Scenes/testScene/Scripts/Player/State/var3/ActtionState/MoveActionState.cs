using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    abstract public class MoveActionState{
        private I_2DPlayerUpdatable player;

        public MoveActionState (I_2DPlayerUpdatable target){
            player = target;
        }  

        abstract public void movePlayer();
    }



}
