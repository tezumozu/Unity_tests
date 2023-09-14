using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateManagement_ver2;
public interface I_2DPlayerUpdatable {
    private static PlayerManager playerManager;

    void playerUpdate ();
    void addPos ( Vector2 vec);
    void addRotate (float rotate);
    void setActionState(E_ActionState state);
    bool isLanding();
    void setManager(PlayerManager manager);

    void attack(bool isAir);

    void setPlayerDirection(E_PlayerDirection direction);

    void chargeAttack(bool isAir);
}
