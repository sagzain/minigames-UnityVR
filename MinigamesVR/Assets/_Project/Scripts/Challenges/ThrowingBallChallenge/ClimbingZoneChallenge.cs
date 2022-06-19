using System.Collections;
using UnityEngine;
using TMPro;

public class ClimbingZoneChallenge : ChallengeManager
{
    protected static ClimbingZoneChallenge _instance;

    public static ClimbingZoneChallenge Instance
    {  
        get
        {
            if(_instance == null) 
            {
                _instance = FindObjectOfType<ClimbingZoneChallenge>(); 
            }
            return _instance;
        }
        
    }
    [SerializeField] private GameObject climbingRocks;
    [SerializeField] private TMP_Text watchTime;
    
    //TODO
    /*
     * - Almacenar los prefabs de las rocas
     * - Mostrar los prefabs cuando se pulse el boton Start
     * - Ocultar los prefabs cuando se pulse el boton End
     * - Hacer un calculo de puntos x tiempo
     */

    private void Start()
    {
        xrStartButton.OnReleasedButton += StartChallenge;
        DespawnClimbingRocks();
    }

    protected override void StartChallenge()
    {
        if(challengeStatus == ChallengeStatusEnum.Started)
            return;
        
        challengeStatus = ChallengeStatusEnum.Started;
        base.StartChallenge();
        SpawnClimbingRocks();
        StartCoroutine(UpdateWatchTimer());
    }

    private IEnumerator UpdateWatchTimer()
    {
        while (challengeStatus == ChallengeStatusEnum.Started)
        {
            watchTime.text = displayTimer.text;
            yield return new WaitForSeconds(1);
        }

    }
    
    private void SpawnClimbingRocks()
    {
        climbingRocks.SetActive(true);
    }

    private void DespawnClimbingRocks()
    {
        climbingRocks.SetActive(false);
    }
}
