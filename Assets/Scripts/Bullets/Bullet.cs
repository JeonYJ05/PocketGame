using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YJ.PocketGame.Monsters;

namespace YJ.PocketGame
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _bulletRigidbody;
        private int _damage;
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
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakeDamage(3);
                Debug.Log("맞았다");
            }
            DestroyBullet(0);
        }
        
    }
}