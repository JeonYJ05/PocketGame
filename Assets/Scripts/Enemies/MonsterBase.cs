using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace YJ.PocketGame.Monsters
{
    public class MonsterBase : MonoBehaviour
    {
        [SerializeField] protected GameObject _bulletPrefab;
        [SerializeField] protected Transform _firePoint;

        private GameManager gameManager;

        private bool isDeath;

        public float CurrentHealth;
        public float EnemyCurrentHealth { get { return CurrentHealth; } }
        protected virtual void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            StartCoroutine(FireTime());
        }

        private IEnumerator FireTime()
        {
            while(true)
            {
                Attack();
                yield return new WaitForSeconds(3);
            }
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
    }
}
