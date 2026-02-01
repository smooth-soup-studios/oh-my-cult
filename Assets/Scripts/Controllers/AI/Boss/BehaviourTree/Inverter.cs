
namespace BossBehaviourTree{
public class Inverter : BossNode
{
    private BossNode _kiddo; 
    public Inverter(BossNode child)
    {
        _kiddo = child;
    }
    public override BossNodeState Evaluate (BossBaseBehaviourTree tree)
    {
        return _kiddo.Evaluate (tree) switch{
            BossNodeState.SUCCESS => BossNodeState.FAILURE, 
            BossNodeState.FAILURE => BossNodeState.SUCCESS,
            BossNodeState.RUNNING => BossNodeState.RUNNING,
            _ => State, 
        };
    }
}
}