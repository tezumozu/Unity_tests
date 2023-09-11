using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_AttackEffectGeneratable{
   void generateEffect(E_PlayerDirection delection);
   void setParent(Player player);
}
