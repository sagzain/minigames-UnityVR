using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [Range(0f, 50f)] [SerializeField] protected int speed;
    [Range(1, 20)] [SerializeField] protected int destroyTime;

    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.forward;
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var go = collision.collider.gameObject;

        if (go.layer != 9)
            return;

        Destroy(gameObject);

        // TODO 
        // Instantiate vfx & sfx
    }
}