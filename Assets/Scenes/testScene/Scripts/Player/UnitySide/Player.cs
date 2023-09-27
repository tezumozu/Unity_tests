using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
using StateManagement_ver3;
using UniRx;

public class Player: MonoBehaviour , I_P_DamageApplicable , I_2DPlayerUpdatable {
    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Vector2 playerSize;

    [SerializeField]
    S_ActionConfig config;


    S_StateData currentState;

    PlayerAttackManager attackManager;

    Dictionary <E_ActionState, PlayerAction> actionMap;
    Dictionary <E_MoveState, PlayerMove> moveMap;


    //Subject
    Subject<Unit> landingSubject;
    Subject<Unit> fallSubject;
    Subject<Unit> damagedSubject;

    
    public Vector2 getPlayerSize {
        get {return playerSize;}
    }

    public S_StateData getStateData {
        get {return currentState;}
    }


    //プレイヤーの初期化
    public void playerInit(){
        attackManager = new PlayerAttackManager(this);
        transform.localScale = playerSize;

        landingSubject = new Subject<Unit>();
        fallSubject = new Subject<Unit>();
        damagedSubject = new Subject<Unit>();


        //マップの作成
        actionMap = new Dictionary<E_ActionState, PlayerAction>();

        actionMap[E_ActionState.WAIT] = new Wait_Action(this);
        actionMap[E_ActionState.LANDING] = new Landing_Action(this);
        actionMap[E_ActionState.JUMP] = new Jump_Action(this);


        //move
        moveMap = new Dictionary<E_MoveState, PlayerMove>();

        moveMap[E_MoveState.LAND] = new Land_Move(this);
        moveMap[E_MoveState.FALL] = new Fall_Move(this);
        moveMap[E_MoveState.JUMP] = new Jump_Move(this);
    }



    //プレイヤーの状態を更新
    public void stateUpdate(S_StateData nextState){

        if(nextState.actionState != currentState.actionState){
            actionMap[nextState.actionState].actionInit();
        }


        if(nextState.moveState != currentState.moveState){
            moveMap[nextState.moveState].moveInit();
        }


        currentState = nextState;
    }



    //プイレイヤーの更新
    public void playerUpdate (){
        //ダメージ判定
        checkDamaged();

        //stateに応じて移動や処理
        actionMap[currentState.actionState].actionUpdate();
        moveMap[currentState.moveState].moveUpdate();
    }



    private void checkDamaged(){

    }



    //ダメージを受けた時の処理
    public void damageApplicated(){

    }



    public void checkLanding(){
        Vector2 startPoint = new Vector2 (transform.position.x,transform.position.y);
        Vector2 endPoint = new Vector2 (transform.position.x,transform.position.y - playerSize.y / 2);

        RaycastHit2D hitObjct = Physics2D.Linecast(startPoint,endPoint,groundLayer);

        if(hitObjct){
            //着地したら
            if(currentState.isAir){
                //座標を修正
                transform.position = new Vector2 (hitObjct.point.x,hitObjct.point.y + playerSize.y / 2); 
                landingSubject.OnNext(Unit.Default);

            }

        }else {
            //落下したら
            if(!currentState.isAir){
                fallSubject.OnNext(Unit.Default);

            }

        }
    }



    //落下時の購読設定
    public void subscribeFall(SubscrivableMethod method){
        fallSubject.Subscribe(x => method());
    }

    //着地時の購読設定
    public void subscribeLanding(SubscrivableMethod method){
        landingSubject.Subscribe(x => method());
    }

    //ダメージ時の購読設定
    public void subscribeDamaged(SubscrivableMethod method){
        damagedSubject.Subscribe(x => method());
    }

    public S_ActionConfig getActionConfig(){
        return config;
    }
}
