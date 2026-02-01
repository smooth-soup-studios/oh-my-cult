using System.Collections;
using System.Collections.Generic;
using BossBehaviourTree; 
using UnityEngine;

public class BehaviourTree : BossBaseBehaviourTree
{
private new void Awake()
    {
        base.Awake();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }

    protected new void Update()
    {
        base.Update();
    }

    protected override BossNode SetupTree()
    {
        BossNode root = new Selector(new List<BossNode>
        {
            new Sequence(new List<BossNode>
            {
                new ChangeBehaviour()
            })
            
        });

        return root; 
        

    }
}
