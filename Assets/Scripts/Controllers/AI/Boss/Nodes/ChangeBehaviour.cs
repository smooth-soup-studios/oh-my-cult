using System.Collections;
using System.Collections.Generic;
using BossBehaviourTree;
using UnityEngine;

public class ChangeBehaviour : BossNode 
{

    public override BossNodeState Evaluate(BossBaseBehaviourTree tree)
    {
        

        State = BossNodeState.FAILURE; 
        return State; 
    }
}
