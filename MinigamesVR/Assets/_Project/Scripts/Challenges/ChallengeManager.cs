using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    #region Vars
    
    [Header("File path")]
    [Tooltip("Path to the file that stores the information about the Player's scores for a specific challenge.")]
    [SerializeField] protected string file;

    private PlayerScore _currentScore = new PlayerScore();
    private ScoreList _playerScoreList = new ScoreList();

    [Header("Challenge")]
    [Tooltip("Displays the current status of the challenge: Waiting, Started or Completed.")]
    [SerializeField] protected ChallengeStatusEnum challengeStatus;

    [Range(0, 120)]
    [SerializeField] private int maxTime;
    
    [SerializeField] private TMP_Text displayInfo;
    [SerializeField] protected TMP_Text displayTimer;
    [SerializeField] protected TMP_Text displayPoints;
    [SerializeField] private TMP_Text scoreboard;
    
    [SerializeField] protected XRButtonInteractable xrStartButton;
    
    private float _currentTime;
    private int _currentPoints;
    
    #endregion
    
    #region Unity

    private void Awake()
    {
        _currentScore.player = Player.Instance.GetPlayerName();
        _currentScore.points = 0;
        _currentScore.time   = .0f;

        _currentPoints = 0;
        _currentTime = maxTime;
        
        displayPoints.text = "0 pts";
        
        DisplayTimeAndPoints(false);
        ReloadScoreboard();
    }
    
    #endregion
    
    #region Methods

    protected virtual void StartChallenge()
    {
        StartCoroutine(TimerRoutine());
        DisplayTimeAndPoints(true);
    }
   
    public void ScorePoints(int points)
    {
        if (challengeStatus != ChallengeStatusEnum.Started)
            return;
        
        _currentPoints += points;
        displayPoints.text = $"{_currentPoints} pts";
    }

    protected virtual void DisplayTimeAndPoints(bool value)
    {
        displayInfo.gameObject.SetActive(!value);
        displayTimer.gameObject.SetActive(value);
        displayPoints.gameObject.SetActive(value);
    }
    
    private IEnumerator TimerRoutine()
    {
        _currentTime = maxTime;
        
        while (challengeStatus == ChallengeStatusEnum.Started)
        {
            string time = TimeSpan.FromSeconds(_currentTime).ToString();
            displayTimer.text = time.Substring(time.Length-5);
            yield return new WaitForSeconds(1);
            _currentTime--;
            
            if (_currentTime < 0)
                EndChallenge();
        }
    }
    
    protected virtual void EndChallenge()
    {
        DisplayTimeAndPoints(false);
        
        challengeStatus = ChallengeStatusEnum.Completed;
        
        _currentTime = 0;
        _currentScore.points = _currentPoints;
        _currentScore.time = _currentTime;
        
        AddScoreToList(_currentScore);
        WriteScoreOnFile();
        ReloadScoreboard();
    }

    private void ReloadScoreboard()
    {
        ReadScoresFromFile();
        ShowScoreboard();
    }
    
    private void ShowScoreboard()
    {
        scoreboard.text = string.Empty;
        
        if (_playerScoreList.scoreList.Count == 0)
        {
            scoreboard.text += "There are no previous player scores.";
            return;
        }

        foreach (var score in _playerScoreList.scoreList)
        {
            scoreboard.text += $"<pos=0%>{score.player}</pos><pos=45%>{score.points}</pos><pos=75%>{score.time}</pos>\n";
        }
    }
    
    private void AddScoreToList(PlayerScore score)
    {
        _playerScoreList.scoreList.Add(score);
    }
    
    #endregion
    
    #region IO
    
    private void ReadScoresFromFile()
    {
        try
        {
            var json = System.IO.File.ReadAllText($"{Application.persistentDataPath}\\{file}");
            _playerScoreList = JsonUtility.FromJson<ScoreList>(json) ?? new ScoreList();
        }
        catch (Exception e)
        {
            Debug.LogError($"[{gameObject.name}] File reading error: {e}", gameObject);
        }
    }
    
    private void WriteScoreOnFile()
    {
        try
        {
            System.IO.File.WriteAllText($"{Application.persistentDataPath}\\{file}", JsonUtility.ToJson(_playerScoreList));
        }
        catch (Exception e)
        {
            Debug.LogError($"[{gameObject.name}] File writing error: {e}", gameObject);
        }

    }
    
    #endregion
}
