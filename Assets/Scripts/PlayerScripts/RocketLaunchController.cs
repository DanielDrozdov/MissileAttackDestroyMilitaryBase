using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLaunchController : MonoBehaviour
{
    private RocketStateController rocketStateController;
    private RocketMoveController rocketMoveController;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        rocketStateController = GetComponent<RocketStateController>();
        rocketMoveController = GetComponent<RocketMoveController>();
        animator.SetTrigger("LaunchTrigger");
    }

    public void EndLaunchAnimation() {
        rocketStateController.EnableFireAndSmokeParticles();
        rocketMoveController.OnRocketLaunched();
        animator.enabled = false;
    }
}
