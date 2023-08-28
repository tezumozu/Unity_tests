using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
public class Player: GravityEffectableMono , I_DamageApplicable {
    
    private Dictionary< E_ActionState , ActionState > ActionStateList;
   
    private E_ActionState currentActionState;
    public void init(){
        //状態遷移の初期化
        ActionState.initActionState();

        currentActionState = E_ActionState.FALL;
        toAir();

        //リスト作成
        ActionStateList = new Dictionary< E_ActionState , ActionState >();
        ActionStateList[E_ActionState.WAIT] = new Action_Wait();
        ActionStateList[E_ActionState.WALK] = new Action_Walk();
        ActionStateList[E_ActionState.JUMP] = new Action_Jump();
        ActionStateList[E_ActionState.ATTACK] = new Action_Attack();
        ActionStateList[E_ActionState.CHARGE_ATTACK] = new Action_ChageAttack();
        ActionStateList[E_ActionState.GUARD] = new Action_Guard();
        ActionStateList[E_ActionState.DUSH] = new Action_Dush();
        ActionStateList[E_ActionState.FALL] = new Action_Fall();
        ActionStateList[E_ActionState.LANDING] = new Action_Landing();

    }

    // Update is called once per frame
    public void objectUpdate(){

        //入力確認
        currentActionState = ActionStateList[currentActionState].checkInput();
        
        //重力の適応
        this.addGravity();

        //各状態に応じた処理
        currentActionState = ActionStateList[currentActionState].stateUpdate();

        //着地の判定
        this.checkLanding();
    }

    override public void checkLanding(){
        Vector2 startPoint = new Vector2 (transform.position.x,transform.position.y);
        Vector2 endPoint = new Vector2 (transform.position.x,transform.position.y - 1.0f);

        RaycastHit2D hitObjct = Physics2D.Linecast(startPoint,endPoint,groundLayer);

        //空中の場合
        if (isAir){
            //検出していたら
            if(hitObjct){
                toLand();
                //座標を修正
                transform.position = new Vector2 (hitObjct.point.x,hitObjct.point.y + 1.0f); 
                currentActionState = E_ActionState.LANDING;
                ActionStateList[currentActionState].resetState();
            }

        //地上の場合
        }else {
            //落下した
            if(!hitObjct){
                toAir();
                currentActionState = E_ActionState.FALL;
                resetGravity();
            }
        }

        return;
    }

    //ダメージを受けた時の処理
    public void damageApplicable(){

    }
    
}
