using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMoveController : MonoBehaviour
{
    private bool IsRocketLaunched;
    private RocketStateController rocketStateController;
    private RocketFuelStateController rocketFuelStateController;
    private TouchPanelController touchPanelController;
    private Rigidbody2D rb;
    private float rocketMoveSpeed = 7f;
    private float rocketRotationSpeed = 2f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rocketStateController = GetComponent<RocketStateController>();
        rocketFuelStateController = RocketFuelStateController.GetInstance();
        touchPanelController = TouchPanelController.GetInstance();
    }

    public void OnRocketLaunched() {
        IsRocketLaunched = true;
    }

    private void FixedUpdate() {
        if(IsRocketLaunched) {
            if(rocketFuelStateController.HasRocketFuel() && rocketStateController.IsRocketAlive()) {
                RotateRocket();
                Move();
                rocketFuelStateController.BurnFuel();
            } else {
                rocketStateController.EnableGravityScale();
                rocketStateController.DisableFireAndSmokeParticles();
                rb.AddTorque(0.2f);
            }
        } 
    }

    private void Move() {
        rb.AddForce(transform.up * rocketMoveSpeed);
        float velocityX = Mathf.Clamp(rb.velocity.x, -rocketMoveSpeed, rocketMoveSpeed);
        float velocityY = Mathf.Clamp(rb.velocity.y, -rocketMoveSpeed, rocketMoveSpeed);
        rb.velocity = new Vector2(velocityX, velocityY);
    }

    private void RotateRocket() {
        float angle = Vector2.SignedAngle(transform.up,touchPanelController.moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angle),
            rocketRotationSpeed * Time.deltaTime);
    }
}
