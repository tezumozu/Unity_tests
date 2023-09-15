using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateManagement_ver3{
    public abstract class PlayerActionState {
        
        protected I_2DPlayerUpdatable player;
        protected bool isInputStandBy;
        protected bool isBufferedInpuitAvailable;
        protected bool isFinished;
        GravityManager gravityManager;


        public bool getIsInputStandBy {
            get { return isInputStandBy; }
        }


        public bool getIsBufferedInpuitAvailable{
            get { return isBufferedInpuitAvailable; }
        }


        public bool getIsFinished{
            get { return isFinished; }
        }


        public PlayerActionState(I_2DPlayerUpdatable player){
            this.player = player;
            isInputStandBy = true;
            isBufferedInpuitAvailable = true;
            isFinished = false;
            gravityManager = new GravityManager();

        }


        public abstract void stateEnter();


        public abstract void updateState();
        


    }
}
