using System.Collections;
using UnityEngine;
using TMPro;
using Unity.XR.CoreUtils;

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
    
    [SerializeField] private XRButtonInteractable xrEndButton;
    [SerializeField] private Transform spawnPosition; 
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
        xrEndButton.OnReleasedButton += EndChallenge;
        
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

    protected override void EndChallenge()
    {
        base.EndChallenge();
        Player.Instance.MovePlayerTo(spawnPosition.position);
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

    protected override void DisplayTimeAndPoints(bool value)
    {
        base.DisplayTimeAndPoints(value);
        displayPoints.gameObject.SetActive(false);
    }
}
