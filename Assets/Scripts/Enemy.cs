using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YJ.PocketGame
{

    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _chaseDistance = 4.0f;

        int MovementFlag = 0;
        public Player Player;
        bool IsCanAttack;
        public float CurrentHealth;
        public bool isDeath;
        public float EnemyCurrentHealth { get { return CurrentHealth; } }
        private void Start()
        {
            StartCoroutine(ChangeMove());
        }
        private void FixedUpdate()
        {
            float distance = Vector3.Distance(transform.position, Player.transform.position);

            if (distance <= 1)        // x값 2이하면 감지  Abs 확인
            {
                IsCanAttack = true;
            }
            else
            {
                IsCanAttack = false;
            }
            if (IsCanAttack)
            {
                Attack(Player);
            }
            else if(distance <= _chaseDistance)
            {
                ChasePlayer();
            }
            else
            {
                MonsterMove();
            }
        }
        private void MonsterMove()
        {
            Vector3 move = Vector3.zero;                //몬스터 움직임 랜덤 설정
            if (MovementFlag == 1)
            {
                move = Vector3.left;
                transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            }
            else if (MovementFlag == 2)
            {
                move = Vector3.right;
                transform.localScale = new Vector3(-0.02f, 0.02f, 0.02f);
            }
            transform.position += move * _moveSpeed * Time.deltaTime;
        }
        private void ChasePlayer()
        {
            Vector3 dir = (Player.transform.position - transform.position).normalized;
            transform.position += dir * _moveSpeed * Time.deltaTime;

            if(dir.x > 0)
            {
                transform.localScale = new Vector3(-0.02f, 0.02f, 0.02f);
            }
            else
            {
                transform.localScale = new Vector3(0.02f, 0.02f, 0.02f); 
            }
        }
        private IEnumerator ChangeMove()           // 몬스터 움직임 방향전환
        {
            MovementFlag = Random.Range(0, 3);

            yield return new WaitForSeconds(3f);
            StartCoroutine(ChangeMove());
        }

        private void OnTriggerEnter(Collider other)                            // 몬스터 낙하방지를 위해 투명 왼쪽 오른쪽벽 생성 닿을시 반대로 움직임
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("LeftWall"))
            {
                MovementFlag = 2;
            }
            if (other.gameObject.layer == LayerMask.NameToLayer("RightWall"))
            {
                MovementFlag = 1;
            }
        }
        public void Attack(Player player)
        {
            player.Hit(transform.position, 3);
        }
        public void TakeDamage(float damage)
        {
            if (!isDeath)
            {
                CurrentHealth -= damage;
                Player.CurrentLife -= 1;
            }
            if (CurrentHealth <= 0) Death();
        }
        public void Death()
        {
            isDeath = true;
            DestroyEnemy();

        }
        private void DestroyEnemy()
        {
            Destroy(gameObject, 3f);
        }
    }

}