using UnityEngine;

public class IdleMainMenuCharacterStateBehaviour : StateMachineBehaviour
{
    [SerializeField] private float _idleBreakerMin = 20;
    [SerializeField] private float _idleBreakerMax = 25;

    float currentTime = 0;
    float targetTime = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTime = 0;
        targetTime = Random.Range(_idleBreakerMin, _idleBreakerMax);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (currentTime >= targetTime)
        {
            int randomBreaker = Random.Range(0, 2);

            switch (randomBreaker)
            {
                case 0:
                    animator.SetTrigger("Nervous");
                    break;
                case 1:
                    animator.SetTrigger("Terrified");
                    break;
            }

            currentTime = 0;
            targetTime = Random.Range(_idleBreakerMin, _idleBreakerMax);
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}
