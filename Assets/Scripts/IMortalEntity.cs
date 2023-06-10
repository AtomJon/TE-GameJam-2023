using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMortalEntity
{
    bool IsAlive { get; }
    int remainingHealthPoints { get; }

    void dealDamage(int damage);
}
