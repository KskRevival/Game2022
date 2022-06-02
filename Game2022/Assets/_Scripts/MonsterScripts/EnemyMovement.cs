using InventoryScripts;
using LabyrinthScripts;
using UIScripts;
using UnityEngine;

namespace MonsterScripts
{
    public class EnemyMovement : MonoBehaviour
    {
        public Vector2 monsterTargetLocation;
        public Vector2 enemyDirection;
        public Rigidbody2D monsterRigidbody;
        public Animator animator;

        public float moveSpeed;
        public float standardSpeed;
        public float chaseSpeed;

        public float angle;

        // Start is called before the first frame update
        void Start()
        {
            monsterTargetLocation = transform.position;
            monsterRigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (GameManager.Instance.state == GameState.Fight
                || PauseScript.IsPaused
                || InventoryHandler.IsInventoryActive
                || TutorialHandler.IsTutorialOpened)
                return;

            SetMovingSpeed();

            angle = Mathf.Atan2(enemyDirection.y, enemyDirection.x);
            enemyDirection.Normalize();
        }

        public void MoveEnemy()
        {
            animator.SetFloat(Animator.StringToHash("Horizontal"), GetHorizontal());
            animator.SetFloat(Animator.StringToHash("Vertical"), GetVertical());
            animator.SetFloat(Animator.StringToHash("Speed"), moveSpeed);
            animator.speed = GetComponent<ChasePlayer>().isChasingPlayer ? 0.8f : 0.5f;
            monsterRigidbody.MovePosition(monsterRigidbody.position + enemyDirection * (moveSpeed * Time.fixedDeltaTime));
        }

        private float GetHorizontal() => !(Mathf.Abs(angle) < Mathf.PI / 4 || Mathf.Abs(angle) > 3 * Mathf.PI / 4)
            ? 0
            : Mathf.Abs(angle) < Mathf.PI / 4 ? 1 : -1;

        private float GetVertical() => Mathf.Abs(angle) < Mathf.PI / 4 || Mathf.Abs(angle) > 3 * Mathf.PI / 4
            ? 0
            : angle > 0 ? 1 : -1;

        public Vector2 GetMovePosition(Vector3 target) => target - transform.position;

        private void SetMovingSpeed() => moveSpeed = GetComponent<ChasePlayer>().isChasingPlayer ? chaseSpeed : standardSpeed;
    }
}
