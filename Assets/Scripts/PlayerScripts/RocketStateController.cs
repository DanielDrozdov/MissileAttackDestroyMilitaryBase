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
    private float explosionRadius = 1.5f;
    private float explodeForce = 500f;

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
        DisableFireAndSmokeParticles();
        animator.enabled = true;
        animator.applyRootMotion = true;
        animator.SetTrigger("ExplosionTrigger");
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Explode();
    }

    private void Explode() {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        for(int i = 0;i < coliders.Length;i++) {
            if(coliders[i].CompareTag("ExplosionSprite") || coliders[i].CompareTag("GeneralGuy")) {
                if(coliders[i].CompareTag("GeneralGuy")) {
                    PanelsStateController.GetInstance().ActivateWinPanel();
                    RocketLifesStateController.GetInstance().OnPlayerWin();
                }
                coliders[i].attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
                Vector2 direction = (coliders[i].transform.position - transform.position).normalized;
                coliders[i].attachedRigidbody.AddForceAtPosition(direction * explodeForce, transform.position);
            }
        }
    }

    private void StopExplodeAnim() {
        animator.enabled = false;
        Destroy(gameObject);
        OnPlayerDie();
        RocketLifesStateController.GetInstance().StartCameraAnim();
    }
}
