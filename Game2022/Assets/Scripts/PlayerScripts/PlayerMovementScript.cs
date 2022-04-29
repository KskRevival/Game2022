using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovementScript : MonoBehaviour
    {
        private static GameObject player;
        public float speed;
        private Stamina stamina;
        private Vector2 movement;
        public Rigidbody2D rb;
        public Animator animator;

        private const float RunSpeed = 6f;
        private const float NormalSpeed = 3.5f;
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Start()
        {
            player = Player.player;
            stamina = player.AddComponent<Stamina>();
        }

        void Update()
        {
            if (DialogueManager.GetInstance().dialogueIsPlaying)
            {
                return;
            }

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
                speed = RunSpeed;
                stamina.DrainStamina();
            }
            else
            {
                animator.speed = 1f;
                speed = NormalSpeed;
                stamina.RechargeStamina();
            }
            var speedMultiplier = movement.x != 0 && movement.y != 0 ? 0.75f : 1f;
            rb.MovePosition(rb.position + movement * (speed * Time.fixedDeltaTime * speedMultiplier));
        }
    }
}
