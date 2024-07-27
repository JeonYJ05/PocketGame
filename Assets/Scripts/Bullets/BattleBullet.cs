using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YJ.PocketGame.Monsters;

namespace YJ.PocketGame.Bullets
{
    public class BattleBullet : Bullet
    {
        [SerializeField] protected Rigidbody _rigidbody;
        private int _damage;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        public void Create(int damage, int speed)
        {
            _damage = damage;
            var dir = transform.forward * speed;                 // 총알방향
            _bulletRigidbody.AddForce(dir, ForceMode.Impulse);
            DestroyBullet(5);
        }
        private void DestroyBullet(float time)
        {
            Destroy(gameObject, time);
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("충돌발생" + other);
            if(other.TryGetComponent<MonsterBase>(out MonsterBase monster))
            {
                monster.TakeDamage(1);
                Debug.Log("몬스터 데미지입다");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("충돌했지만 몬스터아님");
            }
            
        }
    }
}
