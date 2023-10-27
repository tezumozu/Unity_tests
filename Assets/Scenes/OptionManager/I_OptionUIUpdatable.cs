using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;

using UniRx;

public interface I_OptionUIUpdatable{

    public abstract void SetOptionData(Dictionary<E_OptionPage, Dictionary<E_OptionItem,int>> data);

    public abstract void UpdateOptionItem(E_OptionItem kind, int value);

    public abstract void UpdateOptionPage(E_OptionPage index);

    public abstract void SubscribeExit(Action<Unit> metgod);
}
