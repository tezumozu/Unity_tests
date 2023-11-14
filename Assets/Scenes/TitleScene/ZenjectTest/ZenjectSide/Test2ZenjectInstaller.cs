using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class Test2ZenjectInstaller : MonoInstaller{
    public override void InstallBindings(){
        /*
        //コンストラクタに渡したい値は事前にバインド必須
        Container
            .Bind<I_ZenjectTestLogable>()//このメンバを持つクラスに
            .To<TestZenjectInjectableObject>()//このインスタンスを注入
            .AsTransient();
        */

        //ファクトリーメソッドにバインド　ファクトリーメソッド経由で動的にバインド
        Container
            .BindFactory<ZenjectTestableObject , ZenjectTestableObject.TestFactory>();
    }
}
