using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
public class Player: MonoBehaviour , I_P_DamageApplicable , I_2DPlayerUpdatable {
    [SerializeField]
    LayerMask groundLayer;
    I_AttackEffectGeneratable normalAttackEffect_Land;
    I_AttackEffectGeneratable normalAttackEffect_Air;
    I_AttackEffectGeneratable chageAttackEffect_Land;
    I_AttackEffectGeneratable chageAttackEffect_Air;
    

    static PlayerManager playerManager;

    void Start ( ){
        //プレハブの作成と取得
        GameObject land = (GameObject)Resources.Load("testScene/Prefub/AttackEffect/PlayerAttackEffect_Land");
        normalAttackEffect_Land = Instantiate( land , new Vector3(0,0,0), Quaternion.identity).GetComponent<I_AttackEffectGeneratable>();

        GameObject air = (GameObject)Resources.Load("testScene/Prefub/AttackEffect/PlayerAttackEffect_Air");
        normalAttackEffect_Air = Instantiate(air,new Vector3(0,0,0),Quaternion.identity).GetComponent<I_AttackEffectGeneratable>();
    }

    public void playerUpdate (){
        //ダメージ判定

    }


    public void addPos ( Vector2 vec){
        vec.x = vec.x + transform.position.x;
        vec.y = vec.y + transform.position.y;
        transform.position = vec;
    }


    public void addRotate (float rotate){

    }


    public void setActionState(E_ActionState state){

    }


    //ダメージを受けた時の処理
    public void damageApplicated(){

    }


    public bool isLanding(){
        Vector2 startPoint = new Vector2 (transform.position.x,transform.position.y);
        Vector2 endPoint = new Vector2 (transform.position.x,transform.position.y - 1.0f);

        RaycastHit2D hitObjct = Physics2D.Linecast(startPoint,endPoint,groundLayer);

        if(hitObjct){

            //座標を修正
            transform.position = new Vector2 (hitObjct.point.x,hitObjct.point.y + 1.0f); 
            return true;

        }else {
            return false;

        }
    }


    //通常攻撃生成
    public void attack(bool isAir){

        if(isAir){
            normalAttackEffect_Air.generateEffect();
        }else{
            normalAttackEffect_Land.generateEffect();
        }
        
    }


    //チャージ攻撃生成
    public void chargeAttack(bool isAir){

    }


    //マネージャーのセット
    public void setManager(PlayerManager manager){
        playerManager = manager;
    }
}
