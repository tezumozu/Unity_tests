using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager{
    private bool isAir;
    private const float gravityAccel = 0.98f;

    private float currentGravity;

    public float getCurrentGravity{
        get { return currentGravity;}
    }    

    public bool getIsAir {
        get { return isAir; }
    }

    public GravityManager (){
        isAir = false;
        currentGravity = 0.0f;
    }

    public Vector2 addGravity(Vector2 targetPos){
        return targetPos;
    }

    public void resetGravity(){
        currentGravity = 0.0f;
    }

    public void toAir(){
        isAir = true;
        currentGravity = 0.0f;
    }

    public void landing(){
        isAir = false;
        currentGravity = 0.0f;
    }
}
