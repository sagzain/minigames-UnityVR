using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Magazine Boxes")] 
    [SerializeField] private GameObject prefabMagazineBox;
    [SerializeField] private List<Transform> boxLocations;
    
    [Header("Target Options")] 
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject targetPrefab;
    [Range(0, 5f)]
    [SerializeField] private float spawnFrequency;

    [SerializeField] private AxisRange spawnPosX;
    [SerializeField] private AxisRange spawnPosY;
    [SerializeField] private AxisRange spawnPosZ;

    private List<GameObject> _magazineBoxes;
    
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
        _magazineBoxes = new List<GameObject>();
        xrStartButton.OnReleasedButton += StartChallenge; 
    }
    
    protected override void StartChallenge()
    {
        if(challengeStatus == ChallengeStatusEnum.Started)
            return;
        
        challengeStatus = ChallengeStatusEnum.Started;
        base.StartChallenge();
        StartCoroutine(SpawnRoutine());
        GenerateAmmoBoxes();
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

    protected override void EndChallenge()
    {
        base.EndChallenge();
        DestroyAmmoBoxes();
    }

    private void GenerateAmmoBoxes()
    {
        foreach (var location in boxLocations)
        {
            var go = Instantiate(prefabMagazineBox);
            go.transform.position = location.position;
            _magazineBoxes.Add(go);
        }
    }

    private void DestroyAmmoBoxes()
    {
        foreach (var box in _magazineBoxes)
        {
            Destroy(box);
        }

        _magazineBoxes.Clear();
    }
}
