using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YJ.PocketGame
{

    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _chaseDistance = 4.0f;
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] GameObject _gate;

        int MovementFlag = 0;
        bool IsCanAttack;
        public Player Player;
        public float CurrentHealth;
        public bool isDeath;
        private int _waypointIndex = 0;
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
                MonsterPatrol();
            }
        }
        private void MonsterPatrol()
        {
            if (_waypoints.Length == 0) return;

            Transform targetWaypoint = _waypoints[_waypointIndex];
            Vector3 direction = (targetWaypoint.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, _moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                _waypointIndex = (_waypointIndex + 1) % _waypoints.Length;
            }

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
            }
            if (CurrentHealth <= 0) Death();
        }
        public void Death()
        {
            isDeath = true;
            DestroyEnemy();
            if(_gate != null)
            {
                Destroy(_gate);
            }

        }
        private void DestroyEnemy()
        {
            Destroy(gameObject, 3f);
        }
        
    }

}