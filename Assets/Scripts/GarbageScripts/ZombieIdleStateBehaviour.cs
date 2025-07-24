using UnityEngine;

public class ZombieIdleStateBehaviour : StateMachineBehaviour
{
    private float current = 0;
    private float delta = 20;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(current >= delta)
        {
            var scriptPositionReset = animator.GetComponent<PositionResetterInLateUpdate>().Dirty = true;
            current = 0;
        }
        else
        {
            current += Time.deltaTime;
        }
    }
}
