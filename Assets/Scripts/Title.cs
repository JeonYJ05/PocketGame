using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
   public void StartBtn()
   {
        Debug.Log("게임시작");
        SceneManager.LoadScene("MainScene");
   }
   public void QuitBtn()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
