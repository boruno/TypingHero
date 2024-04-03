using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4.5f;
    public Rigidbody2D rb;
    public Camera mainCamera;
    public Camera typingCamera;
    Vector2 input;
    Vector2 movement;

    private void FixedUpdate()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        movement = Vector2.Lerp(movement, input * speed, Time.deltaTime * 10f);
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        movement = Vector2.zero; // Reset the movement after applying it
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Teleporter")
        {
            // Disable the current camera
            mainCamera.enabled = false;

            // Enable the "Typing Camera"
            typingCamera.enabled = true;

            // Disable player movement
            speed = 0;
        }
    }
}