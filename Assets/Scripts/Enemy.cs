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
        if(Mathf.Abs(transform.position.x - Player.transform.position.x) <=  2)        // x값 2이하면 감지  Abs 확인
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
        Vector3 move = Vector3.zero;                //몬스터 움직임 랜덤 설정
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
    private IEnumerator ChangeMove()           // 몬스터 움직임 방향전환
    {
        MovementFlag = Random.Range(0, 3);

        yield return new WaitForSeconds(3f);
        StartCoroutine(ChangeMove());
    }

    private void OnTriggerEnter(Collider other)                            // 몬스터 낙하방지를 위해 투명 왼쪽 오른쪽벽 생성 닿을시 반대로 움직임
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

