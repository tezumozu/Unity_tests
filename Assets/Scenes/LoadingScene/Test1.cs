using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class Test1 : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start(){
        //重い処理を待機
        await UniTask.Delay(TimeSpan.FromSeconds(10));
        Debug.Log("TEST 1 : START");
    }

    // Update is called once per frame
    void Update(){
        //Debug.Log("TEST 1 : UPDATE");
    }
}
