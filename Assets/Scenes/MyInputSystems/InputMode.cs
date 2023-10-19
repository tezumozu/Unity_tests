using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class InputMode : MonoBehaviour{
    // Update is called once per frame

    protected bool isActive = false;

    virtual public void init(){

    }
    
    virtual public void inputUpdate(){
        
    }
}
