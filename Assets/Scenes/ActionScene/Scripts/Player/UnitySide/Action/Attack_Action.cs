using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateManagement_ver3;

public class Attack_Action : PlayerAction {

    I_AttackEffectGeneratable normalAttackEffect_Land;
    I_AttackEffectGeneratable normalAttackEffect_Air;

    I_AttackEffectGeneratable effect;
    S_ActionFrameConfig config;

    E_ActionPhase currentPhase;
    float currentFrame;

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

        currentFrame = 0.0f;
        currentPhase = E_ActionPhase.RADY;

        if(target.getStateData.isAir){
            effect = normalAttackEffect_Air;
            config = target.getActionConfig().normalAttack_Air;
        }else{
            effect = normalAttackEffect_Land;
            config = target.getActionConfig().normalAttack_Land;
        }
    }

    override public void actionUpdate(){

        currentFrame += Time.deltaTime;

            switch(currentPhase){
                case E_ActionPhase.RADY:
                    //一定時間経過で遷移
                    if(currentFrame > config.rady * 1.0f/60.0f){
                        currentPhase = E_ActionPhase.ACTION;
                        currentFrame = currentFrame - config.rady * 1.0f/60.0f;
                        effect.generateEffect(target.getStateData.playerDirection);
                    }
                break;

                case E_ActionPhase.ACTION:
                    //一定時間経過で遷移
                    if(currentFrame > config.action * 1.0f/60.0f){
                        currentPhase = E_ActionPhase.FOLLOW_THROUGH;
                        currentFrame = currentFrame - config.action * 1.0f/60.0f;
                    }
                break;

                case E_ActionPhase.FOLLOW_THROUGH:
                    //一定時間経過で遷移
                break;
            }

        
    }
}
