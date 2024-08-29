using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace YJ.PocketGame
{

    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private int _speed;
        [SerializeField] private int _bulletSpeed;
        [SerializeField] GameObject _firePoint;
        [SerializeField] private Bullet _bullet;
        [SerializeField] float _fireDelay;
        public int CurrentLife = 30;
        public bool isDead;
        private bool _invincibility;
        public Animator Anim;
        private int _groundLayer;
        private float _lastFireTime;
        public int Life { get { return CurrentLife;}}

        private void Start()
        {
            Anim = GetComponent<Animator>();
            _groundLayer = LayerMask.NameToLayer("Ground");
        }
        void FixedUpdate()
        {
            Move();
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= _lastFireTime + _fireDelay)
            {
                Fire(_bullet);
                _lastFireTime = Time.time;
            }
            
        }

        public void Move()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector2 moveDir = new Vector2(moveX, moveY).normalized;
            transform.Translate(moveDir * _speed * Time.deltaTime);
        }
        public void Hit(Vector3 position, int damage)                         // Enemy에게 피격시 뒤로 밀려남
        {
            var dir = transform.position - position;
            dir.y = 0;
            dir.z = 0;
            var velocity = dir.normalized * damage;
            _rigidBody.AddForce(velocity, ForceMode.Impulse);
            if(!_invincibility)
            {
                StartCoroutine(LifeManager());
            }

        }
        private void Move(Vector2 direction, int speed)
        {
            var dir = speed * Time.deltaTime * new Vector3(direction.x, direction.y , 0);
            _rigidBody.MovePosition(_rigidBody.position + dir);

        }

        private void Fire(Bullet prefab)
        {
            var bullet = Instantiate(prefab, _firePoint.transform.position, _firePoint.transform.rotation);
            bullet.Create(3, _bulletSpeed);
        }

      
        private void Dead()
        {
           if(Life<= 0)
            {
                isDead = true;  
            }
        }
       IEnumerator LifeManager()
        {
            CurrentLife -= 1;
            _invincibility = true;
            yield return new WaitForSeconds(2);
            _invincibility = false;
        }
    }
}