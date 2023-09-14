using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateManagement_ver2;

public interface I_AttackEffectGeneratable{
   void generateEffect(E_PlayerDirection delection);
   void setParent(Player player);
}
