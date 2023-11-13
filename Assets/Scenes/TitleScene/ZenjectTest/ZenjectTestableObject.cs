using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class ZenjectTestableObject {
    [Inject]
    I_ZenjectTestLogable testLoger;

    public void Test(){
        testLoger.TestLog();
    }
}
