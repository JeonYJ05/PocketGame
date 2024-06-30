using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace YJ.PocketGame.Monsters
{
    public class Monster2 : Monster1
    {
        System.Random rand = new System.Random();
        [SerializeField] Transform _player;

        public override void Attack()
        {
            
            int RanNum = rand.Next(0, 1);
            if (RanNum == 2)
            {
                Debug.Log("기본공격");
                base.Attack();
            }
            else
            {
                Debug.Log("반사공격");
                AdditionalAttack();
            }
        }
        private void AdditionalAttack()
        {

            if(IsFacing())
            {
                Debug.Log("데미지입다");
            }
        }

        private bool IsFacing()
        {
            Vector3 dir = (transform.position - _player.position).normalized;  // 몬스터 방햑 벡터
            Vector3 playerDir = _player.forward; // 플레이어 방향벡터
            float angle = Vector3.Angle(playerDir, dir);
            return angle < 45.0f;
        }
    }
}
