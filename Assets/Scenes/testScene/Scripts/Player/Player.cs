using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
public class Player: MonoBehaviour{

    private Dictionary< E_ActionState , ActionState > ActionStateList;
    // Start is called before the first frame update
    private E_ActionState currentActionState;
    public void init(){
        currentActionState = E_ActionState.WAIT;

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

    }

    // Update is called once per frame
    public void objectUpdate(){
        //入力確認
        currentActionState = ActionStateList[currentActionState].checkInput();
        
        //各状態に応じた処理
        currentActionState = ActionStateList[currentActionState].stateUpdate();
    }
}
