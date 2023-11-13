using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class Test2ZenjectInstaller : MonoInstaller{
    public override void InstallBindings(){
        Debug.Log("test");

        Container
            .Bind<I_ZenjectTestLogable>()//このメンバを持つクラスに
            .To<TestZenjectInjectableObject>()//このインスタンスを注入
            .AsTransient();
    }
}
