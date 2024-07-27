using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace YJ.PocketGame.Monsters
{
    public class Monster3 : Monster2
    {
        [SerializeField] private GameObject _meteorPrefab;
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3[] _meteorPosition;

        System.Random rand = new System.Random();
        public override void Attack()
        {
            int RanNum = rand.Next(0, 3);
            if (RanNum == 2)
            {
                MeteorAttack();
            }
            else
            {
                base.Attack();
            }
        }
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }
        private void MeteorAttack()
        {
            int RanNum = rand.Next(0, 2);
            GameObject meteor = Instantiate(_meteorPrefab, _meteorPosition[RanNum], Quaternion.identity);
            meteor.GetComponent<Meteor>().Initialize(_target.position);
        }
    }
}
