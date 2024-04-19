using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Detected", 1);
        return true;
    }
}
