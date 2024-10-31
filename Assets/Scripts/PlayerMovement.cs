using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = (2 * maxSpeed) / timeToFullSpeed;
        moveFriction = (-2 * maxSpeed) / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = (-2 * maxSpeed) / (timeToStop * timeToStop);
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     Move();
    // }

    public void Move()
    {
        // Calculate move direction based on input
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Normalize move direction
        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        // Calculate move velocity
        moveVelocity = moveDirection * maxSpeed;

        // Apply friction
        Vector2 friction = GetFriction();
        moveVelocity -= friction * Time.deltaTime;

        // Clamp velocity
        moveVelocity.x = Mathf.Clamp(moveVelocity.x, -maxSpeed.x, maxSpeed.x);
        moveVelocity.y = Mathf.Clamp(moveVelocity.y, -maxSpeed.y, maxSpeed.y);

        // Apply velocity to Rigidbody2D
        rb.velocity = moveVelocity;

        // Stop player if velocity is below stopClamp
        if (Mathf.Abs(rb.velocity.x) < stopClamp.x && Mathf.Abs(rb.velocity.y) < stopClamp.y)
        {
            rb.velocity = Vector2.zero;
        }
    }

    Vector2 GetFriction()
    {
        if (moveDirection.magnitude > 0)
        {
            return new Vector2(
                moveDirection.x != 0 ? moveFriction.x : 0,
                moveDirection.y != 0 ? moveFriction.y : 0
            );
        }
        else
        {
            return new Vector2(
                moveVelocity.x != 0 ? stopFriction.x : 0,
                moveVelocity.y != 0 ? stopFriction.y : 0
            );
        }
    }

    void MoveBound()
    {
        // Empty for now
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f;
    }
}