using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateManagement_ver3;

public class Fall_Move : PlayerMove {

    GravityManager gravityManager;

    public Fall_Move (Player player):base(player){
        gravityManager = new GravityManager();
    }

    override public void moveInit(){
        gravityManager.resetGravity();
    }

    override public void moveUpdate(){
        //重力の影響を受ける
        target.transform.position = gravityManager.addGravity(target.transform.position);

        if(target.getStateData.isRanning){

            Vector3 nextPos = target.transform.position;

            //右を向いていたら
            if(target.getStateData.playerDirection == E_PlayerDirection.RIGHT){
                nextPos.x += target.getActionConfig().walkMaxSpeed * Time.deltaTime;

            //左を向いていたら
            }else{
                nextPos.x -= target.getActionConfig().walkMaxSpeed * Time.deltaTime;
            }

            target.transform.position = nextPos;
        }
    }
}
