using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct AxisRange
{
    public int minValue;
    public int maxValue;
}

public class ShootingRangeChallenge : ChallengeManager
{
    protected static ShootingRangeChallenge _instance;

    public static ShootingRangeChallenge Instance
    {  
        get
        {
            if(_instance == null) 
            {
                _instance = FindObjectOfType<ShootingRangeChallenge>(); 
            }
            return _instance;
        }
    }
    
    [Header("Target Options")] 
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject targetPrefab;
    [Range(0, 5f)]
    [SerializeField] private float spawnFrequency;

    [SerializeField] private AxisRange spawnPosX;
    [SerializeField] private AxisRange spawnPosY;
    [SerializeField] private AxisRange spawnPosZ;
    
    // TODO
    /*
     *  - Instanciar pistolas al pulsar boton Start
     * - Comenzar con el spawn de bichos al pulsar Start
     * - No mostrar el tiempo en Scoreboard, el juego va de matar más en 1/2 min.
     * - A más pequeño/lejos el bicho más puntos
     * - Meter algo de IA sencillita
     * - Al acabar tiempo destruir pistolas y bichos
     */

    private void Start()
    {
        xrStartButton.OnReleasedButton += StartChallenge;
        // StartChallenge();
    }
    
    protected override void StartChallenge()
    {
        if(challengeStatus == ChallengeStatusEnum.Started)
            return;
        
        challengeStatus = ChallengeStatusEnum.Started;
        base.StartChallenge();
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (challengeStatus == ChallengeStatusEnum.Started)
        {
            yield return new WaitForSeconds(spawnFrequency);
            
            var posX = Random.Range(spawnPosX.minValue, spawnPosX.maxValue);
            var posY = Random.Range(spawnPosY.minValue, spawnPosY.maxValue);
            var posZ = Random.Range(spawnPosZ.minValue, spawnPosZ.maxValue);

            var pos = new Vector3(posX, posY, posZ);
;
            var go = Instantiate(targetPrefab, spawnPoint);
            go.transform.localPosition = pos;
        }
    }
}
