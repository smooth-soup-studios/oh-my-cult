using System.Collections.Generic;
namespace BossBehaviourTree{
public class Selector : BossNode
{
    public Selector(): base (){}
    public Selector(List<BossNode> children) : base(children){}

    public override BossNodeState Evaluate(BossBaseBehaviourTree tree)
    {
        foreach (BossNode bossNode in Children){
            switch (bossNode.Evaluate(tree)){
                case BossNodeState.FAILURE:
                    continue; 
                case BossNodeState.SUCCESS:
                    State = BossNodeState.SUCCESS;
                    return State; 
                case BossNodeState.RUNNING: 
                    State = BossNodeState.RUNNING; 
                    return State;
                default:
                    continue; 
            }
        }
        State = BossNodeState.FAILURE; 
        return State;
    }
}
}