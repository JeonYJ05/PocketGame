using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    int MovementFlag = 0;
    public Player Player;
    bool IsCanAttack;
    private void Start()
    {
        StartCoroutine(ChangeMove());
    }
    private void FixedUpdate()
    {
        if(Mathf.Abs(transform.position.x - Player.transform.position.x) <=  2)
        {
            IsCanAttack = true;
        }
        else
        {
            IsCanAttack = false;
        }
        if(IsCanAttack)
        {
            Attack(Player);
        }
        MonsterMove();
    }
    private void MonsterMove()
    {
        Vector3 move = Vector3.zero;
        if(MovementFlag == 1)
        {
            move = Vector3.left;
            transform.localScale = new Vector3(0.02f,0.02f,0.02f);
        }
        else if(MovementFlag == 2)
        {
            move = Vector3.right;
            transform.localScale = new Vector3(-0.02f,0.02f,0.02f);
        }
        transform.position += move * _moveSpeed * Time.deltaTime;
    }
    private IEnumerator ChangeMove()
    {
        MovementFlag = Random.Range(0, 3);

        yield return new WaitForSeconds(3f);
        StartCoroutine(ChangeMove());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("LeftWall"))
        {
            MovementFlag = 2;
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("RightWall"))
        {
            MovementFlag = 1;
        }
    }
    public void Attack(Player player)
    {
        player.Hit(transform.position, 3);
    }
}

