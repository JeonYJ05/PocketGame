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
        protected virtual void Start()
        {
            _initialY = transform.position.y;
            gameManager = FindObjectOfType<GameManager>();
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

        protected void FireBullet()
        {
            if(_bulletPrefab != null && _firePoint != null)
            {
                GameObject Bulletinstance = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
                Bullet bullet = Bulletinstance.GetComponent<Bullet>();
                if(bullet != null)
                {
                    bullet.Create(3, 30);
                }

            }
        }
        public virtual void TakeDamage(float damage)
        {
            if(!isDeath)
            {
                CurrentHealth -= damage;
                gameManager.MyScore += 1;
            }
            if (CurrentHealth < 0) Death();
            
        }

        private void Death()
        {
            isDeath = true;
            DestroyEnemy();

        }
        private void DestroyEnemy()
        {
            Destroy(gameObject, 3);
        }

        public void Initialize(float ranY)
        {
            transform.position = new Vector3(transform.position.x, ranY, transform.position.z);
        }


    }
}
