using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
   public void StartBtn()
   {
        Debug.Log("���ӽ���");
        SceneManager.LoadScene("MainScene");
   }
   public void QuitBtn()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }
}
