using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GravityEffectableMono : MonoBehaviour {
    [SerializeField]
    protected LayerMask groundLayer;

    protected float currentGravityAcce;

    public float getCurrentGravityAcce{
        get {
            return currentGravityAcce;
        }
    }

    [SerializeField]
    protected float maxGravityAcce = 10.0f;

    protected const float gravityAcce = 0.98f;

    protected bool isAir;
    public bool getIsAir {
        get {return isAir;}
    }

    virtual protected void addGravity (){
        if(!isAir) return ;
        currentGravityAcce += gravityAcce * Time.deltaTime;

        //加速度上限
        if (currentGravityAcce > maxGravityAcce){
            currentGravityAcce = maxGravityAcce;
        }
        transform.position += new Vector3(0.0f, -currentGravityAcce, 0.0f);
    }

    virtual public void toAir(){
        if(isAir) return ;
        currentGravityAcce = 0.0f;
        isAir = true;
    }

    virtual public void toLand(){
        if (!isAir) return ;
        currentGravityAcce = 0.0f;
        isAir = false;

    }

    virtual public void checkLanding(){
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
            }

        //地上の場合
        }else {
            //落下した
            if(!hitObjct){
                toAir();
            }
        }

        return;
    }

    public void resetGravity(){
        currentGravityAcce = 0.0f;
    }

}
