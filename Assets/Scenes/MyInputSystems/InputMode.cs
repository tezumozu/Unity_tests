using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class InputMode : MonoBehaviour{
    // Update is called once per frame

    protected bool isActive = false;

    virtual public void Init(){

    }
    
    virtual public void inputUpdate(){
        
    }

    virtual public void SetInputActive(bool flag){
        isActive = flag;
    }
}
