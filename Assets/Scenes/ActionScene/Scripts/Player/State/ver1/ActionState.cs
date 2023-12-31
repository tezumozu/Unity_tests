using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

namespace StateManagement_ver1{
    abstract public class ActionState {
        
        protected AnimState currentAnimState;
        protected bool isInputStandBy;

        protected static InputManager inputManager;
        protected static PlayerDirection playerDirection;

        protected static bool isWalkLeft;
        protected static bool isWalkRight;

        protected static float moveDistance = 10.0f;

        protected static GravityEffectableMono playerObject;
        protected static GravityEffectableMono getPlayer{
            get {
                if (playerObject == null){
                    playerObject = GameObject.Find("Player").GetComponent<GravityEffectableMono>();
                }

                return playerObject;
            }
        }

        public static void initActionState(){
            inputManager = InputManager.instance;
            isWalkLeft = false;
            isWalkRight = false;
            playerDirection = PlayerDirection.LEFT;
            playerObject = GameObject.Find("Player").GetComponent<GravityEffectableMono>();
        }

        public ActionState (){
            currentAnimState = AnimState.RADY;
            isInputStandBy = true;
        }

        // Start is called before the first frame update
        virtual public void resetState(){
            
        }

        // Update is called once per frame
        virtual public E_ActionState  stateUpdate( ){
            return E_ActionState.WALK;
        }

        //入力確認
        virtual public E_ActionState checkInput(){
            return E_ActionState.WALK;
        }

    }

    public enum PlayerDirection{
        LEFT,
        RIGHT
    }

    public enum AnimState{
        RADY,

    }

    public enum E_ActionState {//アニメーション単位
        WAIT,
        WALK,
        ATTACK,
        CHARGE_ATTACK,
        JUMP,
        DUDGE,
        GUARD,
        DUSH,
        FALL,
        LANDING
    }


}




