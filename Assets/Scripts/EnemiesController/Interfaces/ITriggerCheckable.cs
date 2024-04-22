using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool IsAggroed { get; set; }
    bool IsWithinAttackArea { get; set; }
    bool IsInRange { get; set; }

    void SetAggroStatus(bool isAggroed);
    void SetAttackingArea(bool isWithinAttackArea);
    void SetRunAway(bool isInRange);
}
