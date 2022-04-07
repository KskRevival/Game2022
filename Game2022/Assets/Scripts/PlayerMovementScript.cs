using UnityEditor;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 5f;
    Vector3 movement;

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(moveHorizontal, moveVertical, 0f);

        movement = movement * speed * Time.deltaTime;

        transform.position += movement;
    }
}