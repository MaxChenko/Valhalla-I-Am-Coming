using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public CharacterController characterController;
    public Transform Axe;
    private State state;

    public GameObject projectilePrefab;
    
    private void Awake()
    {
        characterController.OnCharacterAction += CopyState;
    }

    private void CopyState(State state)
    {
        this.state = state;
    }


    public void ShootProjectile()
    {
        var g = Instantiate(projectilePrefab);
        g.transform.position = Axe.position;
        g.GetComponent<ProjectileMotion>().direction = state.lookDirection;
    }
    
}
