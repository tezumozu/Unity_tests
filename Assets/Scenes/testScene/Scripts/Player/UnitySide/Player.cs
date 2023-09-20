using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
using StateManagement_ver3;
public class Player: MonoBehaviour , I_P_DamageApplicable , I_2DPlayerUpdatable {
    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Vector2 playerSize;

    [SerializeField]
    S_ActionConfig config;

    bool isAir;

    GravityManager gravityManager;


    E_PlayerDirection playerDirection;

    PlayerAttackManager attackManager;

    public Vector2 getPlayerSize {
        get {return playerSize;}
    }

    
    

    static PlayerManager playerManager;

    void Start ( ){
        attackManager = new PlayerAttackManager(this);
        transform.localScale = playerSize;
        isAir = true;
        gravityManager = new GravityManager();
    }



    public void playerUpdate (){
        //現在の状態を出力
    }


    //ダメージを受けた時の処理
    public void damageApplicated(){

    }

    public void stateEnter( StateManagement_ver3.E_ActionState state){

    }

    public void moveEnter(E_MoveState state,E_PlayerDirection direction){

    }


    public bool checkLanding(){
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
    public void attack(){   
        //攻撃を生成
        attackManager.normalAttack(isAir,playerDirection);

        //アニメーションを再生
    }


    //チャージ攻撃生成
    public void chargeAttack(){
        //攻撃を生成
        attackManager.chageAttack(isAir,playerDirection);

        //アニメーションを再生
    }
}
