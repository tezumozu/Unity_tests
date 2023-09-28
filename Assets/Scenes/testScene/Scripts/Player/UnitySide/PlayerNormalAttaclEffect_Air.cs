using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateManagement_ver3;

public class PlayerNormalAttaclEffect_Air : MonoBehaviour , I_ToEnemyDamageInflict , I_AttackEffectGeneratable {
    float currentTime;
    Player player;

    public void generateEffect(E_PlayerDirection direction){
        currentTime = 0.0f;
        //有効化
        gameObject.SetActive (true);

        if(direction == E_PlayerDirection.RIGHT){
            Vector3 pos = new Vector3 (player.getPlayerSize.x / 2 , transform.localPosition.y / 2 , 0.0f );
            transform.localPosition = pos;

        }else{
            Vector3 pos = new Vector3 (-player.getPlayerSize.x / 2 , transform.localPosition.y / 2 , 0.0f );
            transform.localPosition = pos;

        }
    }

    // Update is called once per frame
    void Update(){
        currentTime += Time.deltaTime;

        //一定時間経過で無効
        if(currentTime > player.getActionConfig().attack.action * 1.0f / 60.0f ){
            gameObject.SetActive (false);
        }

    }

    public void damageInflict (I_E_DamageApplicable obj){

    }

    public void setParent (Player player){
        this.player = player; 
        transform.parent = player.gameObject.transform;
        Vector3 pos = new Vector3 (player.getPlayerSize.x / 2 , player.getPlayerSize.y / 2 , 0.0f );
        transform.localPosition = pos;
    }
}
