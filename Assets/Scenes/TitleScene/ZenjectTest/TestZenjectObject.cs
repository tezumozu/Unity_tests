using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class TestZenjectObject : MonoBehaviour{
    // Start is called before the first frame update
    [Inject]
    ZenjectTestableObject.TestFactory testFactory;
    private ZenjectTestableObject testObject;


    // Update is called once per frame
    void Update(){
        if(testObject == null){
            if(testFactory == null){
                Debug.Log("Factory Null");
            }else{
                testObject = testFactory.Create();
            }
        }else{
            testObject.Test();
        }
    }
}
