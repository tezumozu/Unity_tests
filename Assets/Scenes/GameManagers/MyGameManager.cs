using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using MyInputSystems;
using StateManagement_ver3;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class MyGameManager : MonoSingleton<MyGameManager> {

    private Dictionary<E_GameScene,GameModeManager> gameObjectManagerList;

    private E_GameScene currentScene;
    
    private UniTask initAsync;

    private E_GameState currentGameState;

    private InputManager inputManager;

    private SceneLoader sceneLoader;

    public override void OnInitialize(){
        currentScene = E_GameScene.TitleScene;
        sceneLoader = new SceneLoader();

        gameObjectManagerList = new Dictionary<E_GameScene, GameModeManager>();
        inputManager = InputManager.instance;

        //各シーンの初期化
        gameObjectManagerList[E_GameScene.TitleScene] = new TitleGameModeManager();
    }
    

    //シーン開始時の共通の初期化
    private void Start(){
        //gameStateを初期化
        currentGameState = E_GameState.INIT;

        //シーンの初期化
        initAsync = UniTask.RunOnThreadPool( () =>{
            gameObjectManagerList[currentScene].InitScene();
        }); 
    }


    private void Update() {
        switch (currentGameState){

            case E_GameState.INIT:
                //初期化が完了していたら
                if(initAsync.GetAwaiter().IsCompleted){
                    //GameStateをPlayへ
                    currentGameState = E_GameState.PLAY;
                    //ローディング画面を無効にする
                    gameObjectManagerList[currentScene].SetLoadingActive(false);
                }
                break;


            case E_GameState.PLAY:
                //入力取得
                var inputs = inputManager.getInputList;
                inputManager.inputUpdate();

                var gameMode = gameObjectManagerList[currentScene].GetCurrentGameMode;

                //UIアップデート(UIへのユーザ入力を処理)
                gameMode.UIUpdate(inputs);

                //マネージャアップデート
                gameMode.ManagerUpdate(inputs);

                //プレイヤアップデート
                gameMode.PlayerUpdate(inputs);

                //オブジェクトアップデート
                gameMode.ObjectUpdate();


                //シーンが終了していればシーンを切り替える
                if(gameMode.IsSceneFinished){
                    //ゲームの状態をExitへ変更
                    currentGameState = E_GameState.EXIT;

                    //シーンのオブジェクトを破棄 メモリリークを防ぐ
                    gameObjectManagerList[currentScene].ReleaseObject();

                    //Sceneを更新
                    currentScene = gameMode.GetNextScene;

                    //ローディング画面を有効にする
                    gameObjectManagerList[currentScene].SetLoadingActive(true);

                    //シーン読み込み開始
                    StartCoroutine(sceneLoader.loadScene(currentScene));
                }

                break;


            case E_GameState.EXIT:
                //読み込み完了まで待機
                break;
        }
        
    }
}
    




