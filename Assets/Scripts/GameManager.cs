using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text _textScore;
    private Text _bestTextScore;

    private string _name = "BestScore";
    private int _bestScore = 0;
    public int _myScore = 0;

    public int BestScore
    {
        get{ return _bestScore; }
    }

    public int MyScore
    {
        get { return _myScore; }
        set 
        { 
            _myScore = value; 
            if(_myScore > _bestScore)
            {
                _bestScore = _myScore;
                SaveBestScore();
            }
            UpdateScoreUI();
            //_bestTextScore.text = string.Format("My Score : " + _myScore + "Best Score : " + _bestScore);
        }
    
    }
    public void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", _bestScore);
    }
    public void LoadBestScore()
    {
        _bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }
    private void Awake()
    {
        LoadBestScore();
        UpdateScoreUI();
        _bestScore = PlayerPrefs.GetInt(_name, 0);
        //_bestTextScore.text = $"Best Score = {_bestScore.ToString()}";  
    }
    private void UpdateScoreUI()
    {
        if(_textScore != null)
        {
            _textScore.text = $"Score : {_myScore}";
        }
    }

}
