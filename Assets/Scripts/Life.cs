using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace YJ.PocketGame
{
    public class Life : MonoBehaviour
    {
        [SerializeField] Text _lifeText;
        private Player _playerLife;

        private void Awake()
        {
            _playerLife = FindObjectOfType<Player>(); 
        }

        private void Update()
        {
            _lifeText.text = " = " + _playerLife.CurrentLife;
        }
    }
}
