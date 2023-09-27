using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateManagement_ver3;

public class Jump_Move : PlayerMove {

    GravityManager gravityManager;

    public Jump_Move (Player player):base(player){
        gravityManager = new GravityManager();
    }

    override public void moveInit(){
        gravityManager.resetGravity();
    }

    override public void moveUpdate(){

        //ジャンプ時の加速を得る
        Vector3 nextPos = target.transform.position;
        nextPos.y += target.getActionConfig().jumpMaxAccel * Time.deltaTime;

        //重力の影響を受ける
        nextPos = gravityManager.addGravity(nextPos);


        if(target.getStateData.isRanning){

            //右を向いていたら
            if(target.getStateData.playerDirection == E_PlayerDirection.RIGHT){
                nextPos.x += target.getActionConfig().walkMaxSpeed * Time.deltaTime;

            //左を向いていたら
            }else{
                nextPos.x -= target.getActionConfig().walkMaxSpeed * Time.deltaTime;
            }
        }

        target.transform.position = nextPos;
    }
}
