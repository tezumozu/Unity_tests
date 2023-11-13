using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class TestZenjectObject : MonoBehaviour{
    // Start is called before the first frame update

    private ZenjectTestableObject testObject;


    // Update is called once per frame
    void Update(){
        if(testObject == null){
            Debug.Log("test");
            testObject = new ZenjectTestableObject();
        }else{
            testObject.Test();
        }
    }
}
