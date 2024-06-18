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

            if (distance <= 1)        // x�� 2���ϸ� ����  Abs Ȯ��
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
            Vector3 move = Vector3.zero;                //���� ������ ���� ����
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
        private IEnumerator ChangeMove()           // ���� ������ ������ȯ
        {
            MovementFlag = Random.Range(0, 3);

            yield return new WaitForSeconds(3f);
            StartCoroutine(ChangeMove());
        }

        private void OnTriggerEnter(Collider other)                            // ���� ���Ϲ����� ���� ���� ���� �����ʺ� ���� ������ �ݴ�� ������
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