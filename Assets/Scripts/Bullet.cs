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
        var dir = transform.right * speed;                 // ÃÑ¾Ë¹æÇâ
        _bulletRigidbody.AddForce(dir, ForceMode.Impulse);
        Destroy(gameObject, 20f);

    }
}
