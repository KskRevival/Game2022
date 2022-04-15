using UnityEditor;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float Speed;
    public float RunSpeed = 5f;
    public float NormalSpeed = 3.5f;
    private float stamina = 1f;
    private float staminaDepleteTime = 10f;
    private float staminaRegenTime = 15f;
    public bool canRun = true;
    public bool isRunning = false;
    Vector2 movement;
    public Rigidbody2D rb;
    public Animator animator;

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical);

        animator.SetFloat("Horizontal", moveHorizontal);
        animator.SetFloat("Vertical", moveVertical);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && movement != Vector2.zero && stamina > 0f && canRun)
        {

            isRunning = true;
            Speed = RunSpeed;
            //Debug.Log(stamina);
            stamina -= Time.deltaTime / staminaDepleteTime;
            if (stamina < 0f)
            {
                //Debug.Log("STOP");
                canRun = false;
                stamina = 0f;
            }
        }
        else
        {
            isRunning = false;
            Speed = NormalSpeed;
            //Debug.Log(stamina);
            if (stamina < 1f)
                stamina += Time.deltaTime / staminaRegenTime;
            else
            {
                canRun = true;
                stamina = 1f;
            }
        }
        var speedMultiplier = movement.x != 0 && movement.y != 0 ? 0.75f : 1f;
        rb.MovePosition(rb.position + Speed * Time.fixedDeltaTime * movement * speedMultiplier);
    }

    public void MoveTo(Vector2 pos)
    {
        rb.MovePosition(pos);
    }
}