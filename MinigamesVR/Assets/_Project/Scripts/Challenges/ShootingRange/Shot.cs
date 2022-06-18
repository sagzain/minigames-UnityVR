using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [Range(0f,50f)]
    [SerializeField] protected int _speed;
    [Range(1, 20)]
    [SerializeField] protected int _despawnTime;

    private void Start()
    {
        StartCoroutine(DespawnAfterTime());
    }

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(_despawnTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // TODO 
        // Instantiate vfx & sfx
        StopCoroutine(DespawnAfterTime());
        Destroy(gameObject);
    }
}
