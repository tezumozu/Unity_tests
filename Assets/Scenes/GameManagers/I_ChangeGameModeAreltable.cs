using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_ChangeGameModeAreltable{
    public abstract void SubscribeChangeGameModeAlert(Action<E_GameMode> nextMode);
}
