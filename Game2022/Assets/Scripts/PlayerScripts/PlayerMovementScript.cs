using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovementScript : MonoBehaviour
{
    private static GameObject player = GameObject.Find("Player");
    public float speed;
    public float runSpeed = 5f;
    public float normalSpeed = 3.5f;
    private Stamina stamina = player.AddComponent<Stamina>();
    private Vector2 movement;
    public Rigidbody2D rb;
    public Animator animator;
    
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Update()
    {
        var moveHorizontal = Input.GetAxisRaw("Horizontal");
        var moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical);

        animator.SetFloat(Horizontal, moveHorizontal);
        animator.SetFloat(Vertical, moveVertical);
        animator.SetFloat(Speed, movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina.IsStaminaAvailable(movement))
        {
            animator.speed = 2f;
            speed = runSpeed;
            stamina.DrainStamina();
        }
        else
        {
            animator.speed = 1f;
            speed = normalSpeed;
            stamina.RechargeStamina();
        }
        var speedMultiplier = movement.x != 0 && movement.y != 0 ? 0.75f : 1f;
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement * speedMultiplier);
    }
}