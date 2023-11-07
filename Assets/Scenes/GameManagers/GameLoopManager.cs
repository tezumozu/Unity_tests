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

public class GameLoopManager : MonoSingleton<GameLoopManager> {

    private Dictionary<E_GameScene,GameModeManager> gameModeManagerList;

    private E_GameScene currentScene;
    private E_GameScene nextScene;
    
    private UniTask initAsync;

    private E_GameState currentGameState;

    private InputManager inputManager;

    private SceneLoader sceneLoader;

    private bool isHaveNeedLoading;

    //ゲーム開始時の初期化
    public override void OnInitialize(){
        currentScene = E_GameScene.TitleScene;
        sceneLoader = SceneLoader.instance;

        gameModeManagerList = new Dictionary<E_GameScene, GameModeManager>();
        inputManager = InputManager.instance;

        //各シーンのGameMmodeManagerを初期化
        gameModeManagerList[E_GameScene.TitleScene] = new TitleGameModeManager();
        gameModeManagerList[E_GameScene.ActionScene] = new ActionGameModeManager();
    }
    

    //シーン開始時の共通の初期化
    private void Start(){
        isHaveNeedLoading = false;

        //gameStateを初期化
        currentGameState = E_GameState.INIT;

        //シーンの初期化
        initAsync = UniTask.RunOnThreadPool( () =>{
            gameModeManagerList[currentScene].InitScene();
            //ゲームマネージャをサブスク
            var gameManagers = gameModeManagerList[currentScene].GetLoadSceneAreltableObjects();
            foreach (var gm in gameManagers){
                gm.SubscribeLoadSceneAlert(LoadScene);
            }
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
                    gameModeManagerList[currentScene].SetLoadingActive(false);
                }
                break;


            case E_GameState.PLAY:
                //入力取得
                var inputs = inputManager.getInputList;
                inputManager.inputUpdate();

                var gameMode = gameModeManagerList[currentScene].GetCurrentGameMode;

                //UIアップデート(UIへのユーザ入力を処理)
                gameMode.UpdateUI(inputs);

                //マネージャアップデート
                gameMode.UpdateManager(inputs);

                //プレイヤアップデート
                gameMode.UpdatePlayer(inputs);

                //オブジェクトアップデート
                gameMode.UpdateObject();

                //GameModeManagerの更新
                gameModeManagerList[currentScene].UpdateOwn();


                //ロードの必要があればシーンロード
                if(isHaveNeedLoading){
                    //ゲームの状態をExitへ変更
                    currentGameState = E_GameState.EXIT;

                    //シーンのオブジェクトを破棄 メモリリークを防ぐ
                    gameModeManagerList[currentScene].ReleaseObject();

                    //ローディング画面を有効にする
                    gameModeManagerList[currentScene].SetLoadingActive(true);

                    //Sceneを更新
                    currentScene = nextScene;

                    //シーン読み込み開始
                    //StartCoroutine(sceneLoader.loadScene(currentScene));

                    isHaveNeedLoading = false;
                }

                break;


            case E_GameState.EXIT:
                //読み込み完了まで待機
                break;
        }
    }


    //呼び出されたフレームの最後にロードを行う
    private void  LoadScene(E_GameScene next){
        isHaveNeedLoading = true;
        nextScene = next;
    }
}
    




