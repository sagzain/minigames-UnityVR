using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [Range(0f,15f)]
    [SerializeField] protected int _speed;
    [Range(1, 20)]
    [SerializeField] protected int _despawnTime;

    void Start()
    {
        StartCoroutine(DespawnAfterTime());
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(_despawnTime);
        Destroy(gameObject);
    }
}
