using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttaclEffect_Land : MonoBehaviour , I_ToEnemyDamageInflict , I_AttackEffectGeneratable{

    float currentTime;
    const float EffectValidTime = 10.0f * GameValue.g_FrameTime; //5フレーム分

    public void generateEffect(){
        currentTime = 0.0f;
        //有効化
        gameObject.SetActive (true);
    }

    // Update is called once per frame
    void Update(){
        currentTime += Time.deltaTime;

        //一定時間経過で無効
        if(currentTime > EffectValidTime){
            gameObject.SetActive (false);
        }

    }

    public void damageInflict (I_E_DamageApplicable obj){

    }
}
