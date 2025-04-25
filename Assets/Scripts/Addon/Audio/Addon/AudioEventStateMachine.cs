using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventStateMachine : StateMachineBehaviour
{
    [SerializeField] private AudioEvent[] audioEvents;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audioEvents.Play();
    }
}
