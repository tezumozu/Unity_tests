using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateManagement_ver2;

public class PlayerManager {
    private I_2DPlayerUpdatable player;
    private E_PlayerAction currentState;
    private Dictionary< E_PlayerAction , PlayerActionState > stateMap;

    public PlayerManager (I_2DPlayerUpdatable player){
        this.player = player;
        player.setManager(this);

        //StateMapの作成
        stateMap = new Dictionary < E_PlayerAction , PlayerActionState >();
        stateMap [E_PlayerAction.WAIT] = new Wait_PlayerAction(player);
        stateMap [E_PlayerAction.WALK] = new Walk_PlayerAction(player);
        stateMap [E_PlayerAction.FALL] = new Fall_PlayerAction(player);
        stateMap [E_PlayerAction.LANDING] = new Landing_PlayerAction(player);
        stateMap [E_PlayerAction.JUMP] = new Jump_PlayerAction(player);
        //stateMap [E_PlayerAction.ATTACK] = new Attack_PlayerAction(player);

        /*
        stateMap [E_PlayerAction.CHARGE_ATTACK] = new PlayerAction_ChageAttack();
        stateMap [E_PlayerAction.GUARD] = new PlayerAction_Guard();
        stateMap [E_PlayerAction.DUSH] = new PlayerAction_Dush();
        */

        //初期課
        currentState = E_PlayerAction.FALL;
        stateMap[currentState].stateEntrance();
    }


    public void managerUpdate(){
        //状態の更新
        E_PlayerAction nextState = currentState;

        //着地、落下判定
        if(PlayerActionState.getIsAir){
            if(player.isLanding()){
                nextState = E_PlayerAction.LANDING;  //着地時処理
                stateMap[nextState].stateEntrance();
            }
        
        }else {    //落下判定
            if(!player.isLanding()){
                nextState = E_PlayerAction.FALL;  //落下時処理
                stateMap[nextState].stateEntrance();
            }
        }

        //Stateが終了しているか
        if(stateMap[currentState].getIsFinished){
            nextState = stateMap[currentState].stateExit();
            stateMap[nextState].stateEntrance();
        }

        //入力によるなどによる状態遷移を確認
        nextState = stateMap[nextState].checkInput();

        //状態が変化していたら
        if(currentState != nextState){  
            //前の状態を終了させ、次の状態を準備
            stateMap[nextState].stateEntrance();
        }

        //状態ごとの処理
        stateMap[nextState].stateUpdate();

        //状態を更新
        currentState = nextState;
        
        //プレイヤーの更新
        player.playerUpdate();
    }

}
