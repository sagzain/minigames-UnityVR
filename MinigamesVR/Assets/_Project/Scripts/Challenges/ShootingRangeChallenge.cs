using UnityEngine;

public class ShootingRangeChallenge : ChallengeManager
{
    [Header("Options")]
    [Range(0, 5)]
    [SerializeField] private int maxLives;

    [SerializeField] private GameObject playerWeapon;
    
    [SerializeField] private GameObject enemyPrefab;
    [Range(0, 5f)]
    [SerializeField] private float enemySpawnFrequency;

    [SerializeField] private GameObject friendPrefab;
    [Range(0f, 5f)]
    [SerializeField] private float friendSpawnFrequency;
        
    [SerializeField] private int currentLives;
    
    // TODO
    /*
     *  - Instanciar pistolas al pulsar boton Start
     * - Comenzar con el spawn de bichos al pulsar Start
     * - No mostrar el tiempo en Scoreboard, el juego va de matar más en 1/2 min.
     * - A más pequeño/lejos el bicho más puntos
     * - Meter algo de IA sencillita
     * - Al acabar tiempo destruir pistolas y bichos
     */


}
