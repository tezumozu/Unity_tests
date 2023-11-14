using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class TestZenjectInstaller : MonoInstaller{
    public override void InstallBindings(){
        /*
        Container
            .Bind<I_ZenjectTestLogable>()//このメンバを持つクラスに
            .To<TestZenjectInjectableObject>()//このインスタンスを注入
            .AsTransient();
        */
    }
}