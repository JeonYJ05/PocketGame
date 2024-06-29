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
        public override void Attack()
        {
            base.Attack();
            AdditionalAttack();
        }

        private void AdditionalAttack()
        {

        }
    }
}
