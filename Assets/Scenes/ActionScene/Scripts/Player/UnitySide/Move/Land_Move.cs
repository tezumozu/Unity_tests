using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateManagement_ver3;

public class Land_Move : PlayerMove {

    public Land_Move (Player player):base(player){

    }

    override public void moveInit(){
    }

    override public void moveUpdate(){
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
