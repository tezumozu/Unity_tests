using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;

public interface I_GameObjectUpdatable{
    public abstract void ObjectInit();

    //マネージャ更新用
    public abstract void ManagerUpdate(InputData[] inputs);

    //UI更新用
    public abstract void UIUpdate(InputData[] inputs);

    //プレイヤーキャラクター更新用
    public abstract void PlayerUpdate(InputData[] inputs);

    //ステージギミックやNPC更新用
    public abstract void ObjectUpdate();
}
