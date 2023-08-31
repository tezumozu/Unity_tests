using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ダメージを受ける側
public interface I_DamageApplicable{
    void damageApplicable ();
}

public interface I_P_DamageApplicable : I_DamageApplicable{

}

public interface I_E_DamageApplicable : I_DamageApplicable{
    
}


//ダメージ与える側
public interface I_DamageInflict {
    void damageInflict (I_DamageApplicable obj);
}

public interface I_ToPlayerDamageInflict {
    void damageInflict (I_P_DamageApplicable obj);
}

public interface I_ToEnemyDamageInflict {
    void damageInflict (I_E_DamageApplicable obj);
}

