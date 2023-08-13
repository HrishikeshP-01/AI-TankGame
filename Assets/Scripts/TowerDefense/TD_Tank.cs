using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TD_Tank : MonoBehaviour
{
    [SerializeField]
    private Transform agentGoal;
    private NavMeshAgent navMeshAgent;

    [SerializeField]
    private float boostSpeedDuration = 3f;
    [SerializeField]
    private ParticleSystem boostParticleSystem;
    [SerializeField]
    private float shieldDuration = 3f;
    [SerializeField]
    private GameObject shield;

    private float regularSpeed = 3.5f;
    private float boostedSpeed = 7f;
    private bool canBoost = true;
    private bool canShield = true;

    // Start is called before the first frame update
    private void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(agentGoal.position);
    }

    private IEnumerator Boost()
    {
        canBoost = false;
        navMeshAgent.speed = boostedSpeed;
        boostParticleSystem.Play();
        float boostCounter = 0f;
        while(boostCounter<boostSpeedDuration)
        {
            boostCounter += Time.deltaTime;
            yield return null; // Pauses execution for current frame. In order to execute the while loop over several frames.
        }

        boostParticleSystem.Pause();
        canBoost = true;
        navMeshAgent.speed = regularSpeed;
    }

    private IEnumerator Shield()
    {
        canShield = false;
        float shieldCounter = 0f;
        shield.SetActive(true);
        while(shieldCounter<shieldDuration)
        {
            shieldCounter += Time.deltaTime;
            yield return null;
        }
        shield.SetActive(false);
        canShield = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (canBoost) { StartCoroutine(Boost()); }
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            if (canShield) { StartCoroutine(Shield()); }
        }
    }
}
