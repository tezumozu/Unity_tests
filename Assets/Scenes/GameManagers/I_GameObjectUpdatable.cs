using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;

public interface I_GameObjectUpdatable{
    public abstract void InitObject();

    //マネージャ更新用
    public abstract void UpdateManager(InputData[] inputs);

    //UI更新用
    public abstract void UpdateUI(InputData[] inputs);

    //プレイヤーキャラクター更新用
    public abstract void UpdatePlayer(InputData[] inputs);

    //ステージギミックやNPC更新用
    public abstract void UpdateObject();
}
