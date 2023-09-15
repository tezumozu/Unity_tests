using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateManagement_ver3{
    public struct ActionStateData {
        public E_UpActionState up;
        public E_BottomActionState bottom;

        public ActionStateData (E_UpActionState up, E_BottomActionState bottom){
            this.up = up;
            this.bottom = bottom;
        }
    }
}
