using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private int _speed;
    [SerializeField] private int _bulletSpeed;
    [SerializeField] GameObject _firePoint;
    [SerializeField] private Bullet _bullet;
    [SerializeField] float _fireDelay;
    [SerializeField] private bool isJump;
    private bool isMoving;
    private bool isDetectMove;
    private Animator _anim;
    private int _groundLayer;
    private float _lastFireTime;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _groundLayer = LayerMask.NameToLayer("Ground");
    }
    void Update()
    {
        
        if (Input.GetKey(KeyCode.A))                        // 플레이어 움직임
        {
            Move(Vector2.left, _speed);
            _anim.SetBool("LeftWalk", true);
            _anim.SetBool("RightWalk", false);
            Debug.Log(isMoving);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Move(Vector2.right, _speed);
            _anim.SetBool("LeftWalk", false);
            _anim.SetBool("RightWalk", true);
            isMoving = true;
        }
        if(!isMoving)
        {
            _anim.SetBool("LeftWalk", false);
            _anim.SetBool("RightWalk", false);
            _anim.SetBool("Idle", true);
        }

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= _lastFireTime + _fireDelay)
        {
            Fire(_bullet);
            _lastFireTime = Time.time;
        }
    } 
    private void FixedUpdate()        
    {

        if (Input.GetKeyDown(KeyCode.C) && !isJump)                   //플레이어 공격 설정
        {
            _rigidBody.velocity = Vector3.zero; 
            isJump = true;
            _rigidBody.AddForce(Vector2.up * 6 , ForceMode.Impulse);
        }
    }
    public void Hit(Vector3 position , int damage)                         // Enemy에게 피격시 뒤로 밀려남
    {
        var dir = transform.position - position;
        dir.y = 0;
        dir.z = 0;
        var velocity = dir.normalized * damage;
        _rigidBody.AddForce(velocity, ForceMode.Impulse);
    }
    private void Move(Vector2 direction , int speed)
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
        if(collision.gameObject.layer == _groundLayer)
        {
            isJump = false;
        }
    }
}
