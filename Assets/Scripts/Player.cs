using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private bool isJump;
        public bool isDead;
        private bool _invincibility;
        private bool isMoving;
        private bool isDetectMove;
        public Animator Anim;
        private int _groundLayer;
        private float _lastFireTime;
        public int CurrentLife = 3;
        public int Life { get { return CurrentLife;}}

        private void Start()
        {
            Anim = GetComponent<Animator>();
            _groundLayer = LayerMask.NameToLayer("Ground");
        }
        void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.A))                        // 플레이어 움직임
            {
                Move(Vector2.left, _speed);
                Anim.SetBool("LeftWalk", true);
                Anim.SetBool("Idle", false);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Move(Vector2.right, _speed);
                Anim.SetBool("RightWalk", true);
                Anim.SetBool("Idle", false);
            }
            else
            {
                Anim.SetBool("Idle", true);
                Anim.SetBool("LeftWalk", false);
                Anim.SetBool("RightWalk", false);
            }

            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= _lastFireTime + _fireDelay)
            {
                Fire(_bullet);
                _lastFireTime = Time.time;
            }



            if (Input.GetKeyDown(KeyCode.C) && !isJump)                   //플레이어 공격 설정
            {
                _rigidBody.velocity = Vector3.zero;
                isJump = true;
                _rigidBody.AddForce(Vector2.up * 6, ForceMode.Impulse);

            }
            
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
            var dir = speed * Time.deltaTime * new Vector3(direction.x, direction.y, 0);
            _rigidBody.MovePosition(_rigidBody.position + dir);

        }

        private void Fire(Bullet prefab)
        {
            var bullet = Instantiate(prefab, _firePoint.transform.position, _firePoint.transform.rotation);
            bullet.Create(3, _bulletSpeed);
        }

        private void OnCollisionStay(Collision collision)                    // 공중에서 점프 못하게 구현
        {
            if (collision.gameObject.layer == _groundLayer)
            {
                isJump = false;
            }
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
            yield return new WaitForSeconds(1);
            _invincibility = false;
        }
    }
}