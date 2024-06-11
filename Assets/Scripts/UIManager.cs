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
        public GameObject GameOverScene;
        public Player _player;

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
            GameOver();
        }
        public void Toggle()
        {
            PauseUI.SetActive(!PauseUI.activeSelf && !GameOverScene.activeSelf);

            if(PauseUI.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
           
        }
        public void ContinueBtn()
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
        public void GameOver()
        {
            if (_player.CurrentLife <= 0 && !GameOverScene.activeSelf)
            {
                GameOverScene.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
