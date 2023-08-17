using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MyGameManagers;

namespace MyInputSystems {
    public class InputManager : MonoSingleton<InputManager>{

        static Dictionary<E_InputType , InputData> inputDic; 
        public const float maxValidFrameCount = 6.0f * 1.0f/60.0f;

        private Dictionary< GameModes , InputMode > input_Mode_List;
        private List< InputData > inputBuffer;

        public InputData[] getInputBuffer { 
            get {

                //入力順にソート
                inputBuffer.Sort((x,y) => {
                    if(y.frameCount > x.frameCount){
                        return 1;
                    }
                        return -1;
                });

                //配列にコピー
                InputData[] copy = new InputData[inputBuffer.Count];
                inputBuffer.CopyTo(copy);
                return copy;
            }
        }

        public override void OnInitialize() {
            //インプットデータの初期化
            inputDic =  new Dictionary<E_InputType , InputData>();
            inputBuffer = new List<InputData> ();

            //各モードインプットの初期化
            input_Mode_List = new Dictionary<GameModes, InputMode>();
            input_Mode_List[GameModes.ACTION_ACTION] = GameObject.Find("IS_Action").GetComponent<ActionInput>();

            foreach (var key in input_Mode_List.Keys) {
                input_Mode_List[key].init();
            }
        }

        public void inputUpdate(){
            //ゲームモードをもとに入力を更新
            input_Mode_List[GameManager.getCurrentMode].inputUpdate();
            
            //インプットデータの更新
            //入力されてから何フレーム経ったかカウント
            var keyList = new List<E_InputType>(inputDic.Keys);

            foreach(var key in keyList){
                if(inputDic[key].frameCount < maxValidFrameCount){
                    //フレームカウントを加算
                    InputData nextData = inputDic[key];
                    nextData.frameCount += Time.deltaTime;
                    inputDic[key] = nextData;
                }
            }

            //バッファの更新
            for (int i = 0; i < inputBuffer.Count; i++){
                //フレームカウントを加算
                InputData data  = inputBuffer[i]; 
                data.frameCount += Time.deltaTime;
                inputBuffer[i] = data;
            }

            //有効フレームをすぎた入力を削除
            inputBuffer.RemoveAll( x => {
                return x.frameCount >= maxValidFrameCount;
            });
        }


        //入力を登録
        public void setInputData(E_InputType type){
            inputDic[type] = new InputData(type);
        }


        // 指定フレーム以内に入力された入力をすべて取得
        public List < InputData > getInputData( float validFrameCount){ 

            var result = new List<InputData>();
            var keyList = new List<E_InputType>(inputDic.Keys);

            foreach(var key in keyList){
                //一定フレーム前までの入力取得
                if (inputDic[key].frameCount < validFrameCount) {
                    InputData data = inputDic[key];

                    //有効な入力データをリストとバッファに登録
                    result.Add(data);
                    inputBuffer.Add(data);

                    //辞書を無効な入力へ
                    data.frameCount = maxValidFrameCount;
                    inputDic[key] = data;
                }
            }

            //入力が古い順にソート
            result.Sort((x,y) => {
                if(y.frameCount > x.frameCount){
                    return 1;
                }
                return -1;
            });

            return result;
        }
    }

    public enum E_InputType {
        //Action
        WALK_LEFT,
        WALK_LEFT_PERFORMED,
        WALK_LEFT_CANCELED,

        WALK_RIGHT,
        WALK_RIGHT_PERFORMED,
        WALK_RIGHT_CANCELED,

        ATTACK,

        CHARGE_ATTACK,
        CHARGE_ATTACK_PEFORMED,
        CHARGE_ATTACK_CANCELED,

        GUARD,
        GUARD_PEFORMED,
        GUARD_CANCELED,

        JUMP,
        LITTLE_JUMP,        
        DUSH,
        POUSE,
        CAMERA_CHANGE,

        //Title

        //Camera
        CAMERA_MOVE_UP,
        CAMERA_MOVE_DOWN
    }

    public struct InputData{
        public E_InputType type;
        public float frameCount;

        public InputData (E_InputType type){
            this.type = type;
            this.frameCount = 0.0f;
        }
    }

}