using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAction {
    
    protected Player target;

    public PlayerAction (Player target){
        this.target = target;
    }

    abstract public void actionInit();

    abstract public void actionUpdate();
}

