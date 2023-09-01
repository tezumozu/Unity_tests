using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager{
    private const float gravityAccel = -9.8f;

    private float currentGravity;

    public float getCurrentGravity{
        get { return currentGravity;}
    }    

    public GravityManager (){
        currentGravity = 0.0f;
    }

    //毎フレーム呼び出すことで重力を再現する
    public Vector2 addGravity(Vector2 targetPos){
        currentGravity += gravityAccel * Time.deltaTime;
        Debug.Log(currentGravity);
        targetPos.y += currentGravity;
        return targetPos;
    }

    public void resetGravity(){
        currentGravity = 0.0f;
    }
}
