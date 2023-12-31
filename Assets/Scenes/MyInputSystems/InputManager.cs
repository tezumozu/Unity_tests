using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyInputSystems {
    public class InputManager : GenericSingletonObject<InputManager>{

        public const float maxValidFrameCount = 6.0f * GameValue.g_FrameTime;

        private List< InputData > inputList;

        private List< InputData > inputBuffer;


        // 指定フレーム以内に入力された入力をすべて取得
        public InputData[] getInputList { 
            get {
                //入力が古い順にソート
                inputList.Sort((x,y) => {
                    if(y.frameCount > x.frameCount){
                        return 1;
                    }
                    return -1;
                });

                //配列にコピー
                InputData[] copy = new InputData[inputList.Count];
                inputList.CopyTo(copy);

                //配列内をクリア
                inputList.Clear();
                return copy;
            }
        }


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

                //inputListをクリア
                inputList.Clear();
                return copy;
            }
        }



        public InputManager (){
            //インプットデータの初期化
            inputList = new List<InputData> ();
            inputBuffer = new List<InputData> ();

            //各InputModeを初期化
        }


        public override void OnInitialize() {
            inputBuffer.Clear();
            inputList.Clear();
        }


        public void inputUpdate(){
            
            //インプットデータの更新
            //入力されてから何フレーム経ったかカウント
            for (int i = 0; i < inputList.Count; i++){
                //フレームカウントを加算
                InputData data  = inputList[i]; 
                data.frameCount += Time.deltaTime;
                inputList[i] = data;
            }

            //バッファの更新
            for (int i = 0; i < inputBuffer.Count; i++){
                //フレームカウントを加算
                InputData data  = inputBuffer[i]; 
                data.frameCount += Time.deltaTime;
                inputBuffer[i] = data;
            }

            //有効フレームをすぎた入力を削除
            inputList.RemoveAll( x => {
                return x.frameCount > maxValidFrameCount;
            });

            inputBuffer.RemoveAll( x => {
                return x.frameCount > maxValidFrameCount;
            });
        }


        //入力を登録
        public void setInputData(E_InputType type){
            InputData data = new InputData(type);
            inputList.Add(data);
            inputBuffer.Add(data);
        }

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