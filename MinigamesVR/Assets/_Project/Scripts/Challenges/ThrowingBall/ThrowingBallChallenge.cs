using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ThrowingBallChallenge : ChallengeManager
{
    protected static ThrowingBallChallenge _instance;

    public static ThrowingBallChallenge Instance
    {  
        get
        {
            if(_instance == null) 
            {
                _instance = FindObjectOfType<ThrowingBallChallenge>(); 
            }
            return _instance;
        }
    }

    [Header("Ball Options")] 
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject ballPrefab;
    [Range(0, 10)]
    [SerializeField] private int ballQuantity;
    
    //TODO
    /*
     * - Instanciar de forma aleatoria prefabs de monedas
     * - Instanciar de forma aleatoria obstaculos (?) (quizás se va de madre)
     * - Instanciar coche en punto de Spawn al pulsar boton Start
     * - Destruir coche y finalizar challenge al coleccionar todas las monedas
     * - mas puntos a menos tiempo
     */

    private void Start()
    {
        xrStartButton.OnReleasedButton += StartChallenge;
    }

    protected override void StartChallenge()
    {
        if(challengeStatus == ChallengeStatusEnum.Started)
            return;
        
        challengeStatus = ChallengeStatusEnum.Started;
        base.StartChallenge();
        SpawnBalls();
    }

    private void SpawnBalls()
    {
        for (int i = 0; i < ballQuantity; i++)
        {
            var go = Instantiate(ballPrefab);
            go.transform.position = spawnPoint.position;
        }
    }
}
