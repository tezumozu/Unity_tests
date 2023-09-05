using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
public class Player: MonoBehaviour , I_P_DamageApplicable , I_2DPlayerUpdatable {
    [SerializeField]
    LayerMask groundLayer;

    static PlayerManager playerManager;

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
    public void damageApplicable(){

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


    //マネージャーのセット
    public void setManager(PlayerManager manager){
        playerManager = manager;
    }
}
