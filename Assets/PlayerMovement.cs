using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            TryMove(movementInput);
        }
    }

    private void TryMove(Vector2 direction)
    {
        int count = rb.Cast(
                direction, // X and Y values brtween -1 and 1 that represent the direction from the body to look for collisions.
                movementFilter, // The settings that determine where a collision can occur on such as layers to collido with.
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished.
                moveSpeed * Time.fixedDeltaTime * collisionOffset // The amount to cast equal to the movement plus an offset.
            );

        //If there is a collision the player will not move.
        if (count == 0)
        {
            rb.MovePosition(rb.position + (direction * moveSpeed * Time.fixedDeltaTime));
        }
        else
        {
            Debug.Log(count);
            Debug.Log(movementInput);
            rb.MovePosition(rb.position + ((direction * -5) * moveSpeed * Time.fixedDeltaTime));
        }

    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
