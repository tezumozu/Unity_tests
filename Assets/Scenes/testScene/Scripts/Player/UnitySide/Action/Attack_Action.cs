using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateManagement_ver3;

public class Attack_Action : PlayerAction {

    I_AttackEffectGeneratable normalAttackEffect_Land;
    I_AttackEffectGeneratable normalAttackEffect_Air;

    public Attack_Action (Player player):base(player){
        //プレハブの作成と取得
        GameObject land = (GameObject)Resources.Load("testScene/Prefub/AttackEffect/PlayerAttackEffect_Land");
        normalAttackEffect_Land = GameObject.Instantiate( land , new Vector3(0,0,0), Quaternion.identity).GetComponent<I_AttackEffectGeneratable>();

        GameObject air = (GameObject)Resources.Load("testScene/Prefub/AttackEffect/PlayerAttackEffect_Air");
        normalAttackEffect_Air = GameObject.Instantiate(air,new Vector3(0,0,0),Quaternion.identity).GetComponent<I_AttackEffectGeneratable>();

        //親子付け
        normalAttackEffect_Land.setParent(player);
        normalAttackEffect_Air.setParent(player);
    }

    override public void actionInit(){
        if(target.getStateData.isAir){
            normalAttackEffect_Air.generateEffect(target.getStateData.playerDirection);
        }else{
            normalAttackEffect_Land.generateEffect(target.getStateData.playerDirection);
        }
    }

    override public void actionUpdate(){
        
    }
}
