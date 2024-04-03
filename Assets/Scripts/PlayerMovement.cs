using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4.5f;
    public Rigidbody2D rb;
    Vector2 input;
    Vector2 movement;

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        movement = input * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement);
        movement = Vector2.zero; // Reset the movement after applying it
    }
}