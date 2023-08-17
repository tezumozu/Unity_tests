using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInputSystems;

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
        private Player player;


        // Start is called before the first frame update
        void Start()
        {
            //フレームレートの設定
            Application.targetFrameRate = 60; 

            currentMode = GameModes.ACTION_ACTION;

            inputManager = InputManager.instance;

            player = GameObject.Find("Player").GetComponent<Player>();
            player.init();
        }

        // Update is called once per frame
        void Update()
        {
            //Inputを更新
            inputManager.inputUpdate();

            //各ゲームオブジェクト更新
            player.objectUpdate();

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


