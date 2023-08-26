using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAnimatorScript : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject sparkles = GameObject.Find("Sparkles");
        sparkles.SetActive(false);
    }
}
