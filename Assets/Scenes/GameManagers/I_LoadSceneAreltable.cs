using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_LoadSceneAreltable{
    public abstract void SubscribeLoadSceneAlert(Action<E_GameScene> method);
}
