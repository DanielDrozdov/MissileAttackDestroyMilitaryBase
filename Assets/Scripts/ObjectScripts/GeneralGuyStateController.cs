using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGuyStateController : MonoBehaviour
{
    private Animator animator;

    private void Awake() {
        DiePanelController.OnPlayerDie += OnPlayerDie;
        animator = GetComponent<Animator>();
    }

    private void OnDestroy() {
        DiePanelController.OnPlayerDie -= OnPlayerDie;
    }

    private void OnPlayerDie() {
        animator.enabled = true;
    }
}
