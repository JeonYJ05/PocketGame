using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace YJ.PocketGame.Monsters
{
    public class MonsterBase : MonoBehaviour
    {
        [SerializeField] protected GameObject _bulletPrefab;
        [SerializeField] protected Transform _firePoint;
        
        [SerializeField] float _moveSpeed;
        [SerializeField] float _swayAmount = 0.5f;
        [SerializeField] float _swaySpeed = 2f;

        private GameManager gameManager;

        private float _initialY;
        private bool isDeath;

        public float CurrentHealth;
        public float EnemyCurrentHealth { get { return CurrentHealth; } }
        private CapsuleCollider _monsterCollider;


        protected virtual void Start()
        {
            _initialY = transform.position.y;
            gameManager = FindObjectOfType<GameManager>();
            _monsterCollider = GetComponent<CapsuleCollider>();
            
            StartCoroutine(FireTime());

        }
        private void FixedUpdate()
        {
            Moving();
        }
        private IEnumerator FireTime()
        {
            while(true)
            {
                Attack();
                yield return new WaitForSeconds(3);
            }
        }

        private void Moving()
        {
            transform.position += Vector3.left * _moveSpeed * Time.fixedDeltaTime;

            float newY = _initialY + Mathf.Sin(Time.time * _swaySpeed) * _swayAmount;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z); 
        }
        public virtual void Attack()
        {
            FireBullet();
        }

        public virtual void ThreeAttack()
        {
            FireBulletsInThreeDirections();
        }

        protected void FireBullet()
        {
            if(_bulletPrefab != null && _firePoint != null)
            {
                GameObject Bulletinstance = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
                Bullet bullet = Bulletinstance.GetComponent<Bullet>();
                if(bullet != null)
                {
                    bullet.Create(3, 10);
                }

            }
        }
        public virtual void TakeDamage(float damage)
        {
            if (!isDeath)
            {
                CurrentHealth -= damage;
                

                if (CurrentHealth < 0) Death();
            }
            
        }

        private void Death()
        {
            gameManager.MyScore += 1;
            isDeath = true;
            if (_monsterCollider != null)
            {
                _monsterCollider.enabled = false;
                _moveSpeed = 0;
                
            }
            DestroyEnemy();
            FireBulletsInEightDirections();
        }
        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        public void Initialize(float ranY)
        {
            transform.position = new Vector3(transform.position.x, ranY, transform.position.z);
        }

        private void FireBulletsInEightDirections()
        {
            Vector3[] directions = new Vector3[]
            {
                new Vector3(-1, 1, 0).normalized, // 11시
                new Vector3(0, 1, 0).normalized,  // 12시
                new Vector3(1, 1, 0).normalized,  // 1시
                new Vector3(1, 0, 0).normalized,  // 3시
                new Vector3(1, -1, 0).normalized, // 5시
                new Vector3(0, -1, 0).normalized, // 6시
                new Vector3(-1, -1, 0).normalized,// 7시
                new Vector3(-1, 0, 0).normalized, // 9시
            };
        
            foreach (var direction in directions)
            {
                Vector3 spawnPosition = _firePoint.position + direction * 0.2f;

                GameObject bulletInstance = Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity);
                Bullet bullet = bulletInstance.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.SetDirection(direction); // 각 방향으로 설정
                    bullet.SpecialCreate(3, 15); // 속도와 데미지 설정
                }
            }
        }
        protected void FireBulletsInThreeDirections()
        {
            Vector3[] directions = new Vector3[]
            {
                new Vector3(-1, 1, 0).normalized, // 11시
                new Vector3(-1, -1, 0).normalized,// 7시
                new Vector3(-1, 0, 0).normalized, // 9시
            };

            foreach (var direction in directions)
            {
                Vector3 spawnPosition = _firePoint.position + direction * 0.2f;

                GameObject bulletInstance = Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity);
                Bullet bullet = bulletInstance.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.SetDirection(direction); // 각 방향으로 설정
                    bullet.SpecialCreate(3, 15); // 속도와 데미지 설정
                }
            }
        }
    }
}
