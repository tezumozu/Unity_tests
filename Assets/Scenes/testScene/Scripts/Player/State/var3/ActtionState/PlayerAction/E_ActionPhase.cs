using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateManagement_ver3{
    public enum E_ActionPhase{ //基準：他のアクションと並行できないアクション　キャンセルは可
        RADY,
        ACTION,
        FOLLOW_THROUGH
    }
}
