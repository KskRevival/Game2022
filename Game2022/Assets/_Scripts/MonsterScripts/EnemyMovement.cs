using UnityEngine;

namespace MonsterScripts
{
    public class EnemyMovement : MonoBehaviour
    {
        public Vector2 monsterTargetLocation;
        public Vector2 enemyDirection;
        public Rigidbody2D monsterRigidbody;

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
        void Update()
        {
            SetMovingSpeed();

            angle = Mathf.Atan2(enemyDirection.y, enemyDirection.x);
            enemyDirection.Normalize();
        }

        public void MoveEnemy()
        {
            monsterRigidbody.MovePosition(transform.position + (Vector3)enemyDirection * (moveSpeed * Time.fixedDeltaTime));
        }

        public Vector2 GetMovePosition(Vector3 target) => target - transform.position;

        private void SetMovingSpeed() => moveSpeed = GetComponent<ChasePlayer>().isChasingPlayer ? chaseSpeed : standardSpeed;
    }
}
