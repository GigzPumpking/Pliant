using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject gameManager = GameObject.Find("Game Manager");
        GameManager gameManagerScript = gameManager.GetComponent<GameManager>();
        gameManagerScript.Death();
    }
}
