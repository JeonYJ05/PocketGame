using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace YJ.PocketGame.Monsters
{
    public class Monster2 : MonsterBase
    {
        public override void Attack()
        {
            FireBulletsInThreeDirections();
        }

        protected void FireBulletsInThreeDirections()
        {
            Vector3[] directions = new Vector3[]
            {
                new Vector3(-1, 1, 0).normalized, // 11시
                new Vector3(-1, -1, 0).normalized,// 7시
                new Vector3(-1, 0, 0).normalized, // 9시
            };

            foreach (var direction in directions)
            {
                Vector3 spawnPosition = _firePoint.position + direction * 0.2f;

                GameObject bulletInstance = Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity);
                Bullet bullet = bulletInstance.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.SetDirection(direction); // 각 방향으로 설정
                    bullet.SpecialCreate(3, 15); // 속도와 데미지 설정
                }
            }
        }
    }
}
