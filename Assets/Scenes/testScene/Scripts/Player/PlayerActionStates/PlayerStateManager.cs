using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager {
    private I_2DPlayerUpdatable player;
    private E_ActionState currentState;

    private GravityManager gravityManager;

    private Dictionary< E_ActionState , PlayerActionState > stateMap;

    public PlayerStateManager (I_2DPlayerUpdatable player){
        
        this.player = player;
        gravityManager = new GravityManager();

        //StateMapの作成
        stateMap = new Dictionary < E_ActionState , PlayerActionState >();
        stateMap [E_ActionState.WAIT] = new PlayerAction_Wait(gravityManager);

        /*
        stateMap [E_ActionState.WALK] = new PlayerAction_Walk();
        stateMap [E_ActionState.JUMP] = new PlayerAction_Jump();
        stateMap [E_ActionState.ATTACK] = new PlayerAction_Attack();
        stateMap [E_ActionState.CHARGE_ATTACK] = new PlayerAction_ChageAttack();
        stateMap [E_ActionState.GUARD] = new PlayerAction_Guard();
        stateMap [E_ActionState.DUSH] = new PlayerAction_Dush();
        stateMap [E_ActionState.FALL] = new PlayerAction_Fall();
        stateMap [E_ActionState.LANDING] = new PlayerAction_Landing();
        */
    }

    public void managerUpdate (){
        
        E_ActionState nextState;

        //入力を確認
        nextState = stateMap[currentState].checkInput();

        //着地判定の確認
        if(player.isLanding()){
            gravityManager.landing();

            //入力による状態変化がなければ
            if (nextState == currentState){
                nextState = E_ActionState.LANDING;
            }
        }

        //状態が変わっていたら
        if(nextState != currentState){
            //前の状態の後処理
            stateMap[currentState].stateTermination();

            //バッファを確認して同時押しなど検出し状態遷移を確定させる
            nextState = stateMap[nextState].checkBuffer();
            
            //次の状態の初期化処理
            stateMap[nextState].stateInit();
        }

        //状態ごとの処理
        stateMap[nextState].stateUpdate();
    }
}
