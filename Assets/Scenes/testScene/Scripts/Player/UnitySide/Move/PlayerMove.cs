using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMove {
    
    protected Player target;

    public PlayerMove (Player target){
        this.target = target;
    }

    abstract public void moveInit();

    abstract public void moveUpdate();
}

