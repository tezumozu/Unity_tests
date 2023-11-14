using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class ZenjectTestableObject {
    I_ZenjectTestLogable testLoger;

    private ZenjectTestableObject (I_ZenjectTestLogable obj){
        testLoger = obj;
    }

    public void Test(){
        if(testLoger == null){
            Debug.Log("Object Null");
        }else{
            testLoger.TestLog();
        }
    }


    public class TestFactory : PlaceholderFactory<ZenjectTestableObject>{

    }
}
