using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
public class Player: MonoBehaviour , I_P_DamageApplicable , I_2DPlayerUpdatable {
    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Vector2 playerSize = new Vector2( 1.0f , 1.0f );

    E_PlayerDirection playerDirection;

    public Vector2 getPlayerSize {
        get {return playerSize;}
    }

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

        //親子付け
        normalAttackEffect_Land.setParent(this);
        normalAttackEffect_Air.setParent(this);

        transform.localScale = playerSize;
        
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
        Vector2 endPoint = new Vector2 (transform.position.x,transform.position.y - playerSize.y / 2);

        RaycastHit2D hitObjct = Physics2D.Linecast(startPoint,endPoint,groundLayer);

        if(hitObjct){

            //座標を修正
            transform.position = new Vector2 (hitObjct.point.x,hitObjct.point.y + playerSize.y / 2); 
            return true;

        }else {
            return false;

        }
    }


    //通常攻撃生成
    public void attack(bool isAir){

        if(isAir){
            normalAttackEffect_Air.generateEffect(playerDirection);
        }else{
            normalAttackEffect_Land.generateEffect(playerDirection);
        }
        
    }

    public void setPlayerDirection(E_PlayerDirection direction){
        playerDirection = direction;
    }


    //チャージ攻撃生成
    public void chargeAttack(bool isAir){

    }


    //マネージャーのセット
    public void setManager(PlayerManager manager){
        playerManager = manager;
    }
}
