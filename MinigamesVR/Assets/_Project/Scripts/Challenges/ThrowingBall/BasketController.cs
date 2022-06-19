using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource), typeof(Collider))]
public class BasketController : MonoBehaviour
{
    [SerializeField] private int score;

    [SerializeField] private AudioClip sfxScore;
    [SerializeField] private GameObject vfxScore;

    private TMP_Text _displayScore;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _displayScore = GetComponentInChildren<TMP_Text>();
        _audioSource = GetComponent<AudioSource>();
        
        if(_displayScore) 
            _displayScore.text = score.ToString();
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Ball"))
            return;
        
        if (sfxScore && _audioSource)
            _audioSource.PlayOneShot(sfxScore);

        if (vfxScore)
            Instantiate(vfxScore, transform);
            
        ThrowingBallChallenge.Instance.ScorePoints(score);
    }
}
