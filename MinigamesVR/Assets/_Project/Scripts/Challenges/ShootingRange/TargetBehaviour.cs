using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    private Vector3 _originalScale;
    
    [Range(0f,5f)]
    [SerializeField] private float lifeSpanTime;
    
    [Range(0,100)]
    [SerializeField] private int scorePoints;
    
    private void Awake()
    {
        _originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    
    private void OnEnable()
    {
        transform.DOScale(_originalScale, 1f).OnComplete(() => transform.localScale = _originalScale);
        StartCoroutine(DespawnRoutine());
    }

    private IEnumerator DespawnRoutine()
    {
        yield return new WaitForSeconds(lifeSpanTime);
        yield return transform.DOScale(Vector3.zero, 1f).WaitForCompletion();
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ShootingRangeChallenge.Instance.ScorePoints(scorePoints);
            StopCoroutine(DespawnRoutine());
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
