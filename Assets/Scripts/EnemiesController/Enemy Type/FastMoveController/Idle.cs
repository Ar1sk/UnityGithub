using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : GAction
{
    public override bool PrePerform()
    {
        Debug.Log("Hey, An enemy!");
        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }
}
