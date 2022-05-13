using System;
using UnityEngine;

namespace PlayerScripts
{
    public class MovementData : MonoBehaviour
    {
        public float speed;
        public Vector2 movement;
        public Rigidbody2D rb;
        public Animator animator;
        public const float RunSpeed = 5f;
        public const float NormalSpeed = 3.5f;
        public static readonly int Horizontal = Animator.StringToHash("Horizontal");
        public static readonly int Vertical = Animator.StringToHash("Vertical");
        public static readonly int Speed = Animator.StringToHash("Speed");

        public MovementData(MovementData md)
        {
            speed = md.speed;
            movement = md.movement;
            rb = md.rb;
            animator = md.animator;
        }
    }
}