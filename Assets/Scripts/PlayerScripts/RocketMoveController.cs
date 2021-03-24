using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMoveController : MonoBehaviour
{
    private RocketStateController rocketStateController;
    private RocketFuelStateController rocketFuelStateController;
    private TouchPanelController touchPanelController;
    private Transform mapCenter;
    private Rigidbody2D rb;
    private bool IsRocketLaunched;
    private bool IsRocketExitMapBorder;
    private float rocketMoveSpeed = 7f;
    private float rocketRotationSpeed = 2f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rocketStateController = GetComponent<RocketStateController>();
        rocketFuelStateController = RocketFuelStateController.GetInstance();
        touchPanelController = TouchPanelController.GetInstance();
        mapCenter = MapBorderState.GetInstance().transform;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("MapBorder")) {
            IsRocketExitMapBorder = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("MapBorder")) {
            IsRocketExitMapBorder = false;
        }
    }

    private void FixedUpdate() {
        if(IsRocketLaunched) {
            if(rocketFuelStateController.HasRocketFuel() && rocketStateController.IsRocketAlive()) {
                if(!IsRocketExitMapBorder) {
                    RotateRocket(touchPanelController.moveDirection);
                } else {
                    RotateRocket((mapCenter.position - transform.position));
                }
                Move();
                rocketFuelStateController.BurnFuel();
            } else {
                rocketStateController.EnableGravityScale();
                rocketStateController.DisableFireAndSmokeParticles();
                rb.AddTorque(0.2f);
            }
        } 
    }

    public void OnRocketLaunched() {
        IsRocketLaunched = true;
        touchPanelController.moveDirection = Vector2.zero;
    }

    private void Move() {
        rb.AddForce(transform.up * rocketMoveSpeed);
        float velocityX = Mathf.Clamp(rb.velocity.x, -rocketMoveSpeed, rocketMoveSpeed);
        float velocityY = Mathf.Clamp(rb.velocity.y, -rocketMoveSpeed, rocketMoveSpeed);
        rb.velocity = new Vector2(velocityX, velocityY);
    }

    private void RotateRocket(Vector2 moveDirection) {
        float angle = Vector2.SignedAngle(transform.up,moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angle),
            rocketRotationSpeed * Time.deltaTime);
    }
}
