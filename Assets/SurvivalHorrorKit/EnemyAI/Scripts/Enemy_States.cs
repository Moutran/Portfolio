using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyState
{
    void EnterState(Enemy_AI enemy);
    void UpdateState();
    void ExitState();
}

