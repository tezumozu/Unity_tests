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

public abstract class MyGameManager : MonoBehaviour {

    [SerializeField]
    private List<InputMode> InputModeList;

    protected InputManager inputManager;
    protected E_GameState currentGameState;

    private static MyGameManager currentActiveManager;
    public static MyGameManager GetActiveManager{
        get{return currentActiveManager;}
    }

    [SerializeField]
    protected GameObject LoadingUI; 

    private AsyncOperation asyncLoad;

    async void Start(){
        currentGameState = E_GameState.INIT;

        currentActiveManager = this;
        inputManager = InputManager.instance;

        var tasks = new List<UniTask>();

        foreach(var inputMode in InputModeList){
            tasks.Add( UniTask.RunOnThreadPool(() => {
                inputMode.init();
            }) );
        }

        await tasks;

        //その他初期化
        UniTask.RunOnThreadPool(() => {
            //InputManagerの初期化
            inputManager.OnInitialize();

            //各シーンごとに必要な初期化
            managerInit();
        }).Forget();
    }


    void Update(){

        switch(currentGameState){
            case E_GameState.INIT:
                Debug.Log("INIT");
            break;

            case E_GameState.LOADING:
                Debug.Log("LOADING");
            break;

            case E_GameState.PLAY:
                Debug.Log("PLAY");
                inputManager.inputUpdate();

                var inputs = inputManager.getInputList;
                managerUpdate(inputs);
            break;
        }
    }


    protected abstract void managerUpdate(InputData[] inputs);


    protected abstract void managerInit();


    protected abstract void managerExit();


    protected void translateScene(E_GameScene sceneName){
        currentGameState = E_GameState.LOADING;
        LoadingUI.SetActive(true);

        UniTask.RunOnThreadPool(() => {
            managerExit();
            currentGameState = E_GameState.EXIT;
        }).Forget();

        StartCoroutine("loadScene",sceneName);
    }

    private IEnumerator loadScene (E_GameScene sceneName){
        asyncLoad = SceneManager.LoadSceneAsync(Enum.GetName(typeof(E_GameScene),sceneName));

        asyncLoad.allowSceneActivation = false;

        while(currentGameState != E_GameState.EXIT && !asyncLoad.isDone){
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }
}
    




