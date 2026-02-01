using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using BossBehaviourTree;
using UnityEngine;

public class ChangeBehaviour : Node
{

    public override NodeState Evaluate(BaseBehaviourTree tree)
    {
        State = NodeState.FAILURE;
        return State;
    }
}
