using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateManagement_ver3{
    public enum E_PlayerAction { //基準：他のアクションと並行できないアクション　キャンセルは可
        WAIT,
        ATTACK,
        LANDING
    }
}
