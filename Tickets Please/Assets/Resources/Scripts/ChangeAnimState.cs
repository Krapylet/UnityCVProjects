using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimState : StateMachineBehaviour {
    [SerializeField] private int state;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var cPAs = animator.GetComponentsInChildren<CostumePartAnimation>();
        foreach (var cPA in cPAs) {
            cPA.drawState = state;
        }
    }
}
