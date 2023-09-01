using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

public class PlayerStateManager {
    private I_2DPlayerUpdatable player;
    private E_ActionState currentState;
    private GravityManager gravityManager;
    private InputManager inputManager;
    private Dictionary< E_ActionState , PlayerActionState > stateMap;


    public PlayerStateManager (I_2DPlayerUpdatable player){
        
        this.player = player;
        gravityManager = new GravityManager();
        inputManager = InputManager.instance;

        //StateMapの作成
        stateMap = new Dictionary < E_ActionState , PlayerActionState >();
        stateMap [E_ActionState.WAIT] = new Wait_PlayerAction(gravityManager,player);
        stateMap [E_ActionState.WALK] = new Walk_PlayerAction(gravityManager,player);
        stateMap [E_ActionState.FALL] = new Fall_PlayerAction(gravityManager,player);

        /*
        stateMap [E_ActionState.JUMP] = new PlayerAction_Jump();
        stateMap [E_ActionState.ATTACK] = new PlayerAction_Attack();
        stateMap [E_ActionState.CHARGE_ATTACK] = new PlayerAction_ChageAttack();
        stateMap [E_ActionState.GUARD] = new PlayerAction_Guard();
        stateMap [E_ActionState.DUSH] = new PlayerAction_Dush();
        
        stateMap [E_ActionState.LANDING] = new PlayerAction_Landing();
        */

        //初期課
        currentState = E_ActionState.FALL;
        PlayerActionState.toAir();
    }


    public void managerUpdate (){
        E_ActionState nextState = currentState;

        //着地判定の確認
        if(PlayerActionState.getIsAir){
            if(player.isLanding()){
            nextState = E_ActionState.LANDING;
            PlayerActionState.landing();
            gravityManager.resetGravity();

            }

        //落下判定
        }else {
            if(!player.isLanding()){
                nextState = E_ActionState.FALL;
                PlayerActionState.toAir();
            }
        }


        //入力をもとに状態遷移を変更
        //入力を確認
        if(stateMap[currentState].isInputStandBy){

            //入力を取得
            var inputData = inputManager.getInputData(6.0f * 1.0f / 60.0f);

            //入力があれば
            if(inputData.Count > 0){
                //入力をもとに遷移を確認
                foreach(var data in inputData) {
                    nextState = stateMap[currentState].checkInput(data.type);
                }

                //先行入力を確認
                if(stateMap[currentState].isBufferedInpuitAvailable){
                    var bufferList = inputManager.getInputBuffer;

                    for (int i = 0; i < bufferList.Length; i++){
                        nextState = stateMap[currentState].checkInput(bufferList[i].type);
                    }
                }
            }
        }

        //状態が変化していたら
        if(currentState != nextState){  
            //前の状態を終了させ、次の状態を準備
            stateMap[currentState].stateTermination();
            stateMap[nextState].stateInit();

            //状態を更新
            currentState = nextState;
        }

        //状態ごとの処理
        nextState = stateMap[nextState].stateUpdate();

        //状態を更新
        currentState = nextState;
    }
}
