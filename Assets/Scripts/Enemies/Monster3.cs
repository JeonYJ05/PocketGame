using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

namespace YJ.PocketGame.Monsters
{
    public class Monster3 : MonsterBase
    {
        [SerializeField] float _anglepre;

        public override void Attack()
        {
            StartCoroutine(FireBulletFanShape(_anglepre, 30 , 0.1f));
        }

        protected IEnumerator FireBulletFanShape(float angle, int bulletCount , float delay)
        {
            float halfAngle = angle / 2;
            for (int i = 0; i <= bulletCount; i++)
            {
                float Angle = -halfAngle + (angle / (bulletCount - 1)) * i;
                Vector3 direction = Quaternion.Euler(0, 0, Angle) * Vector3.up;

                Vector3 spawnPosition = _firePoint.position + direction * 0.2f;

                GameObject bulletInstance = Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity);
                Bullet bullet = bulletInstance.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.SetDirection(direction); // 각 방향으로 설정
                    bullet.SpecialCreate(3, 15); // 속도와 데미지 설정
                }
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
