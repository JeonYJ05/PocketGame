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
        private Vector3 direction;


        public void Create(int damage, int speed)
        {
            _damage = damage;
            direction = transform.forward;
            _bulletRigidbody.velocity = direction * speed;
            DestroyBullet(5);
        }
        public void SpecialCreate(int damage, int speed)
        {
            _damage = damage;
            _bulletRigidbody.velocity = direction * speed;
            DestroyBullet(5);
        }
        private void DestroyBullet(float time)
        {
            Destroy(gameObject, time);
        }

        public void SetDirection(Vector3 dir)
        {
            direction = dir.normalized;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                player.CurrentLife -= 1;
            }
            DestroyBullet(0);
        }



    }
}
