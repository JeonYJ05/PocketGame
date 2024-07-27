using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace YJ.PocketGame
{

    public class BattleScenePlayer : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private int _speed;
        [SerializeField] private int _bulletSpeed;
        [SerializeField] GameObject _firePoint;
        [SerializeField] private Bullet _bullet;
        [SerializeField] float _fireDelay;
        [SerializeField] int MaxJumpCount;
        public bool isJumping = true;
        private float _jumpTime;
        public int _jumpCount;
        public int CurrentLife = 30;
        public bool isDead;
        private bool _invincibility;
        private bool isMoving;
        private bool isDetectMove;
        public Animator Anim;
        private int _groundLayer;
        private float _lastFireTime;
        public int Life { get { return CurrentLife; } }

        private void Start()
        {
            Anim = GetComponent<Animator>();
            _groundLayer = LayerMask.NameToLayer("Ground");
            _jumpCount = MaxJumpCount;
        }
        void FixedUpdate()
        {
            Jump();
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

            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= _lastFireTime + _fireDelay)
            {
                Fire(_bullet);
                _lastFireTime = Time.time;
            }

        }
        public void Hit(Vector3 position, int damage)                         // Enemy에게 피격시 뒤로 밀려남
        {
            var dir = transform.position - position;
            dir.y = 0;
            dir.z = 0;
            var velocity = dir.normalized * damage;
            _rigidBody.AddForce(velocity, ForceMode.Impulse);
            if (!_invincibility)
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

        private void Jump()
        {
            if(_jumpCount == 0)
            {
                isJumping = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) && isJumping && _jumpCount > 0)
            {
                //isJumping = true;
                _rigidBody.AddForce(new Vector2(0,10) , ForceMode.Impulse);
                _jumpCount--;
            }
            if(isGrounded())
            {
                _jumpCount = MaxJumpCount;
                isJumping = true;
            }
        }
        private bool isGrounded()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Dead()
        {
            if (Life <= 0)
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
