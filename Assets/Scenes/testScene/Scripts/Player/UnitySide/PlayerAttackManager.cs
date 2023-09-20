using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateManagement_ver3;

public class PlayerAttackManager {
    // Start is called before the first frame update

    I_AttackEffectGeneratable normalAttackEffect_Land;
    I_AttackEffectGeneratable normalAttackEffect_Air;
    I_AttackEffectGeneratable chageAttackEffect_Land;
    I_AttackEffectGeneratable chageAttackEffect_Air;


    public PlayerAttackManager (Player player){
        //プレハブの作成と取得
        GameObject land = (GameObject)Resources.Load("testScene/Prefub/AttackEffect/PlayerAttackEffect_Land");
        normalAttackEffect_Land = GameObject.Instantiate( land , new Vector3(0,0,0), Quaternion.identity).GetComponent<I_AttackEffectGeneratable>();

        GameObject air = (GameObject)Resources.Load("testScene/Prefub/AttackEffect/PlayerAttackEffect_Air");
        normalAttackEffect_Air = GameObject.Instantiate(air,new Vector3(0,0,0),Quaternion.identity).GetComponent<I_AttackEffectGeneratable>();

        //親子付け
        normalAttackEffect_Land.setParent(player);
        normalAttackEffect_Air.setParent(player);
    }

    public void normalAttack(bool isAir,E_PlayerDirection playerDirection){
        if(isAir){
            normalAttackEffect_Air.generateEffect(playerDirection);
        }else{
            normalAttackEffect_Land.generateEffect(playerDirection);
        }
    }

    public void chageAttack(bool isAir , E_PlayerDirection playerDirection){
        if(isAir){
            
        }else{
           
        }
    }
}
