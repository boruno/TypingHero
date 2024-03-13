using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Vector2 input;

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        var targetPos = transform.position;
        targetPos.x += input.x * speed * Time.deltaTime;
        targetPos.y += input.y * speed * Time.deltaTime;

        transform.position = targetPos;
    }
}