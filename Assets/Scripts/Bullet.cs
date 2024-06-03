using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody _bulletRigidbody;
    private int _damage;
    public void Create(int damage , int speed)
    {
        _damage = damage;
        var dir = transform.right * speed;                 // �Ѿ˹���
        _bulletRigidbody.AddForce(dir, ForceMode.Impulse);
        DestroyBullet(20);
    }
    private void DestroyBullet(float time)
    {
        Destroy(gameObject, time);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Debug.Log("�¾Ҵ�");
        }
        DestroyBullet(0f);
    }
}
