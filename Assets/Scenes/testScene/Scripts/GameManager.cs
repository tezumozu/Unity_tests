using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;
using StateManagement_ver3;

namespace MyGameManagers{

    public class GameManager : MonoSingleton<GameManager>{

        static private GameModes currentMode;
        static public GameModes getCurrentMode {
            get { return currentMode; }
        }


        static private ControllerDevice currentDevise;        
        static public ControllerDevice gatCurrentDevise {
            get { return currentDevise;}
        }


        private InputManager inputManager;
        private PlayerManager playerManager;


        private S_ActionConfig actionConfig; 

        public S_ActionConfig getActionConfig{
            get {return actionConfig;}
        }

        // Start is called before the first frame update
        void Start(){
            //フレームレートの設定
            Application.targetFrameRate = 60; 

            currentMode = GameModes.ACTION_ACTION;

            //入力関係の初期化
            inputManager = InputManager.instance;
            GameObject.Find("IS_Action").GetComponent<ActionInput>().init();

            I_PlayerUpdatable playerObject = GameObject.Find("Player").GetComponent<I_PlayerUpdatable>();
            playerManager = new PlayerManager(playerObject);
            actionConfig = playerObject.getActionConfig();
        }


        // Update is called once per frame
        void Update(){
            //Inputを更新
            inputManager.inputUpdate();

            //インプットの取得
            var input = inputManager.getInputList;

            //各ゲームオブジェクト更新
            playerManager.playerUpdate(input);

            //UI更新
        }
    }


    public enum GameModes{ //入力仕様単位ごとにモードを決める
        TITLE,
        ACTION_ACTION,
        ACTION_CAMERA,
        ACTION_POSE,
        MAPSELECT
    }

    public enum ControllerDevice {
        GAMEPAD,
        KAYMOUSE
    }

}




