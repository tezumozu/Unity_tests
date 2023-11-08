using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_GameOverAlertable {
   public abstract void ObserveGameOverAlert (Action method);
}
