using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YJ.PocketGame
{
   public class FinishSpot : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Player>(out Player player))
            {
                SceneManager.LoadScene("BattleScene");
            }
        }
       
    }
}
