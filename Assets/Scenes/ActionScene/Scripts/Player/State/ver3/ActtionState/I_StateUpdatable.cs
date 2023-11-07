using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver3{
    public interface I_StateUpdatable {
        public S_StateData checkInput(S_StateData state , InputData[] input);

        public void updateState();

        public S_StateData getNextState(S_StateData state);

        public bool getIsFinished();

        public void stateEnter(S_StateData state);

        public S_StateData falling(S_StateData state);

        public S_StateData landing(S_StateData state);
    }
}
