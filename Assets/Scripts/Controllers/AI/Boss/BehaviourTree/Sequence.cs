using System.Collections.Generic;
namespace BossBehaviourTree{
public class Sequence : BossNode {
public Sequence() : base(){}
public Sequence (List<BossNode> children) : base(children){}
    public override BossNodeState Evaluate(BossBaseBehaviourTree tree)
    {
        bool anyChildIsRunning = false;
        foreach(BossNode bossNode in Children){
            switch(bossNode.Evaluate(tree)){
                case BossNodeState.FAILURE:
                    State = BossNodeState.FAILURE;
                    return State; 
                case BossNodeState.SUCCESS:
                    continue; 
                case BossNodeState.RUNNING:
                    anyChildIsRunning = true;
                    continue; 
                default: 
                    State = BossNodeState.SUCCESS; 
                    return State; 
            }        
        }
        State = anyChildIsRunning ? BossNodeState.RUNNING : BossNodeState.SUCCESS; 
        return State; 
    }
}
}