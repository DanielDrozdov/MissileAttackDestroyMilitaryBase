using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStateController : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private ParticleSystem smokeParticles;
    private Rigidbody2D rb;
    private Animator animator;
    private bool IsRocketDestroy;

    public delegate void PlayerActions();
    public static event PlayerActions OnPlayerDie;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public bool IsRocketAlive() {
        return !IsRocketDestroy;
    }

    public void EnableFireAndSmokeParticles() {
        fireParticles.Play();
        smokeParticles.Play();
    }

    public void DisableFireAndSmokeParticles() {
        fireParticles.gameObject.SetActive(false);
        smokeParticles.Stop();
    }

    public void EnableGravityScale() {
        rb.gravityScale = 0.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(!IsRocketDestroy) {
            DestroyRocket();           
        }
    }

    private void DestroyRocket() {
        IsRocketDestroy = true;
        Explode();
    }

    private void Explode() {
        DisableFireAndSmokeParticles();
        animator.enabled = true;
        animator.applyRootMotion = true;
        animator.SetTrigger("ExplosionTrigger");
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void StopExplodeAnim() {
        animator.enabled = false;
        Destroy(gameObject);
        OnPlayerDie();
        NewRocketLifeCreaterController.GetInstance().SpawnRocket();
    }
}
