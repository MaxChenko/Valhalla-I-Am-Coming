using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    public Vector3 direction;
    private float rotater = 0.0f;
    private bool collided = false;
    void Start()
    {
        StartCoroutine(LifetimeRoutine());
    }

    private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.tag.Equals("Player"))
        {
            collided = true;
            transform.parent = other.transform;
        }
    }


    void Update()
    {
        if (collided) return;
        rotater += direction.x > 0 ? -10 : 10;
        transform.rotation = Quaternion.Euler(0,0,rotater);
        transform.position += direction * 0.1f;
    }
}
