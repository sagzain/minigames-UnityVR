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
        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(lifeSpanTime);
        yield return transform.DOScale(Vector3.zero, 1f).WaitForCompletion();
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        var go = collision.collider.gameObject;

        if (go.layer != 7)
            return;
        
        ShootingRangeChallenge.Instance.ScorePoints(scorePoints);
        StopCoroutine(DestroyRoutine());
        Destroy(gameObject);
        
    }
}
