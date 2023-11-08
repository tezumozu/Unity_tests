using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_PlayerDataUpdatable {
   public abstract void UpdateTresure(E_Tresure tresureName);
   public abstract void ClearStage(E_Stage stageName);
}
