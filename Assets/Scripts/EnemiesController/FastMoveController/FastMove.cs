using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMove : GAgent
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("KillPlayer", 1, true);
        goals.Add(s1, 3);
    }

}
