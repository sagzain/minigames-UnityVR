using System;
using System.Collections;
using TMPro;
using UnityEngine;

// [Serializable]
// public struct XRChallengeButtons
// {
//     public XRButtonInteractable startButton;
//     public XRButtonInteractable endButton;
//
//     public void StartButtons()
//     {
//         // TODO 
//         // Una vez implementado XRButtonInteractable, añadir los Callbacks a OnButtonPressed
//             
//         // startButton.OnButtonPressed.AddListener(ChallengeManager.Instance.OnPressedButton_StartChallenge());
//         // endButton.OnButtonPressed.AddListener(ChallengeManager.Instance.OnPressedButton_EndChallenge());
//     }
// }
public class ChallengeManager : Singleton<ChallengeManager>
{
    #region Vars
    
    [Header("File path")]
    [Tooltip("Path to the file that stores the information about the Player's scores for a specific challenge.")]
    [SerializeField] protected string file;

    [Header("Scores")]
    [Tooltip("Score of player that stores its name, the points achieved and the time a challenge has taken to be completed.")]
    [SerializeField] protected PlayerScore currentScore;
    [Tooltip("It stores every score that was previously obtained for the current challenge.")]
    [SerializeField] protected ScoreList playerScoreList;

    private void AddScoreToList(PlayerScore score)
    {
        playerScoreList.scoreList.Add(score);
        OnValueChanged_ScoreList();
    }
    
    
    [Header("Challenge")]
    [Tooltip("Displays the current status of the challenge: Waiting, Started or Completed.")]
    [SerializeField] protected ChallengeStatusEnum challengeStatus;
    public ChallengeStatusEnum ChallengeStatus
    {
        get => challengeStatus;
        set
        {
            challengeStatus = value;
            OnValueChanged_ChallengeStatusEnum(value);
        }
    }
    
    [SerializeField] protected float currentTime;
    [SerializeField] private int currentPoints;

    // [SerializeField] private XRChallengeButtons XR_ChallengeButtons;
    [SerializeField] private XRButtonInteractable XR_startButton;
    [SerializeField] private TextMeshProUGUI scoreboard;
    
    #endregion
    
    #region Unity

    private void Start()
    {
        currentScore.player = "DefaultPlayer";
        currentScore.points = 0;
        currentScore.time   = .0f;

        currentPoints = 0;
        currentTime = .0f;
        
        ReadScoresFromFile();
    }
    
    #endregion
    
    #region Methods

    protected virtual void StartChallenge()
    {
        
    }

    protected virtual void EndChallenge()
    {
        currentScore.points = currentPoints;
        currentScore.time = currentTime;
        
        
        
        WriteScoreOnFile();
    }
   
    public void ScorePoints(int points)
    {
        currentPoints += points;
    }

    private IEnumerator TimerRoutine()
    {
        currentTime = 0;
        
        while (challengeStatus == ChallengeStatusEnum.Started)
        {
            yield return new WaitForSeconds(1);
            currentTime++;
        }
    }

    private void ShowScoreboard()
    {
        if (playerScoreList == null)
        {
            scoreboard.text += "There are no previous player scores.";
            return;
        }

        scoreboard.text += "<pos=0%><b>Player</b></pos><pos=25%><b>Points</b></pos><pos=50%><b>Time<b></pos>\n";
        foreach (var score in playerScoreList.scoreList)
        {
            scoreboard.text += $"<pos=0%>{score.player}</pos><pos=25%>{score.points}</pos><pos=50%>{score.time}</pos>\n";
        }
    }
    
    #endregion
    
    #region IO
    
    private 
    
    protected void ReadScoresFromFile()
    {
        try
        {
            var json = System.IO.File.ReadAllText(Application.persistentDataPath + file);
            playerScoreList = JsonUtility.FromJson<ScoreList>(json);
        }
        catch (Exception e)
        {
            Debug.LogError($"[{gameObject.name}] File reading error: {e}", gameObject);
        }
    }
    
    protected void WriteScoreOnFile()
    {
        try
        {
            System.IO.File.WriteAllText(Application.persistentDataPath + file, JsonUtility.ToJson(playerScoreList));
        }
        catch (Exception e)
        {
            Debug.LogError($"[{gameObject.name}] File writing error: {e}", gameObject);
        }

    }
    
    #endregion
    
    
    #region Callbacks

    public void OnPressedButton_StartChallenge()
    {
        StartCoroutine(TimerRoutine());
        StartChallenge();
    }
    
    public void OnPressedButton_EndChallenge()
    {
        EndChallenge();
    }

    private void OnValueChanged_ChallengeStatusEnum(ChallengeStatusEnum value)
    {
        switch (value)
        {
            case ChallengeStatusEnum.Waiting:
                break;
            case ChallengeStatusEnum.Started:
                break;
            case ChallengeStatusEnum.Completed:
                break;
            default:
                Debug.LogError($"[{gameObject.name}] ChallengeStatusEnum '{value}'.");
                break;
        }
    }

    private void OnValueChanged_ScoreList()
    {
        //TODO
        // Pintar de nuevo el cuadro de los resultados
    }
    
    #endregion
    
    /*
     * LA IDEA DE ESTE SCRIPT ES ALMACENAR TODOS LOS ELEMENTOS COMUNES ENTRE LOS CHALLENGES
     * EN PRINCIPIO SERÍA:
     *      - PUNTOS GANADOS POR EL JUGADOR
     *      - TIEMPO QUE HA DURADO EL RETO
     *      - FICHERO DE VOLCADO PARA LAS PUNTUACIONES DE LOS JUGADORES
     *      - LECTURA/ESCRITURA SOBRE EL FICHERO
     *      - MANEJAR EVENTOS DE INICIO Y FINAL DE JUEGO
     *
     *      - EL AUMENTO DE PUNTOS IRÍA EN LA CLASE HIJA ESPECÍFICA DE CADA CHALLENGE
     */
}
