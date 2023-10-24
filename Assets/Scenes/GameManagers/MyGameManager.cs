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

    private Dictionary<E_GameScene,SceneObjectManager> sceneObjectManagerList;

    private E_GameScene currentScene;
    
    private UniTask initAsync;

    private E_GameState currentGameState;

    private InputManager inputManager;

    private SceneLoader sceneLoader;

    public override void OnInitialize(){
        currentScene = E_GameScene.TitleScene;
        sceneLoader = new SceneLoader();

        sceneObjectManagerList = new Dictionary<E_GameScene, SceneObjectManager>();
        inputManager = InputManager.instance;

        //各シーンの初期化
    }
    

    //シーン開始時の共通の初期化
    private void Start(){
        //gameStateを初期化
        currentGameState = E_GameState.INIT;

        //シーンの初期化
        initAsync = UniTask.RunOnThreadPool( () =>{
            sceneObjectManagerList[currentScene].ObjectInit();
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
                    sceneObjectManagerList[currentScene].SetLoadingActive(false);
                }
                break;


            case E_GameState.PLAY:
                //入力取得
                var inputs = inputManager.getInputList;
                inputManager.inputUpdate();

                //マネージャアップデート
                sceneObjectManagerList[currentScene].ManagerUpdate(inputs);

                //プレイヤアップデート
                sceneObjectManagerList[currentScene].PlayerUpdate(inputs);

                //オブジェクトアップデート
                sceneObjectManagerList[currentScene].ObjectUpdate();

                //UIアップデート
                sceneObjectManagerList[currentScene].UIUpdate();


                //シーンが終了していればシーンを切り替える
                if(sceneObjectManagerList[currentScene].IsFinished){
                    //ゲームの状態をExitへ変更
                    currentGameState = E_GameState.EXIT;

                    //シーンのオブジェクトを破棄 メモリリークを防ぐ
                    sceneObjectManagerList[currentScene].ObjectRelease();

                    //Sceneを更新
                    currentScene = sceneObjectManagerList[currentScene].GetNextScene;

                    //ローディング画面を有効にする
                    sceneObjectManagerList[currentScene].SetLoadingActive(true);

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
    




