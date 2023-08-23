using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
public class Player: GravityEffectableMono{

    private Dictionary< E_ActionState , ActionState > ActionStateList;
    // Start is called before the first frame update
    private E_ActionState currentActionState;
    public void init(){
        //状態遷移の初期化
        ActionState.initState();

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
        
        //各状態に応じた処理
        currentActionState = ActionStateList[currentActionState].stateUpdate();

        //重力の適応
        this.addGravity();

        //着地の判定
        this.checkLanding();
    }

    
}
