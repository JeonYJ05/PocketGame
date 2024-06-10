using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace YJ.PocketGame
{
    public class UIManager : MonoBehaviour
    {
        private bool IsPause;
        public GameObject PauseUI;

        private void Start()
        {
            IsPause = false;
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Toggle();
            }
        }
        public void Toggle()
        {
            PauseUI.SetActive(!PauseUI.activeSelf);

            if(PauseUI.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
