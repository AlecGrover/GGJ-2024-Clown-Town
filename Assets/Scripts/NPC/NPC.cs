using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour
{
    
    [SerializeField]
    private Character characterProfile;

    [SerializeField, ReadOnly(true)]
    private int happiness = 0;
    
    [Header("AI")]
    [SerializeField]
    private float wanderRange = 10f;
    [SerializeField]
    private float wanderTime = 10f;
    [SerializeField]
    private float moveSpeed = 5f;
    
    private float timeSinceLastWander = 0f;
    
    // Cached Components
    private Transform target;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        timeSinceLastWander = wanderTime * Random.Range(0.5f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastWander += Time.deltaTime;
        if (timeSinceLastWander >= wanderTime)
        {
            timeSinceLastWander = 0f;
            Vector3 newTarget = RandomNavSphere(transform.position, wanderRange, -1);
            navMeshAgent.SetDestination(newTarget);
        }
    }

    public void HitWithCard(Card card)
    {
        if (characterProfile != null && card != null)
        {
            
            int LikeRatio = 0;
            int modifier = 0;
            if (card.humorType1 != null && card.humorType1 == characterProfile.taste1.humorType)
            {
                modifier = card.humorStyle1 == characterProfile.taste1.taste ? 1 : -1;
                LikeRatio += modifier * card.power;
            }
            if (card.humorType1 != null && card.humorType1 == characterProfile.taste2.humorType)
            {
                modifier = card.humorStyle1 == characterProfile.taste2.taste ? 1 : -1;
                LikeRatio += modifier * card.power;
            }
            if (card.humorType2 != null && card.humorType2 == characterProfile.taste1.humorType)
            {
                modifier = card.humorStyle2 == characterProfile.taste1.taste ? 1 : -1;
                LikeRatio += modifier * card.power;
            }
            if (card.humorType2 != null && card.humorType2 == characterProfile.taste2.humorType)
            {
                modifier = card.humorStyle2 == characterProfile.taste2.taste ? 1 : -1;
                LikeRatio += modifier * card.power;
            }

            happiness += LikeRatio;
        }
        
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material renderMaterial = renderer.material;
            if (renderMaterial != null)
            {
                if (happiness > 0)
                {
                    renderMaterial.SetColor("_Color", new Color(Math.Clamp(1 - (happiness * 0.3f), 0, 1), 1, Math.Clamp(1 - (happiness * 0.3f), 0, 1)));
                }
                else if (happiness < 0)
                {
                    renderMaterial.SetColor("_Color", new Color(1, Math.Clamp(1 - (-happiness * 0.3f), 0, 1), Math.Clamp(1 - (-happiness * 0.3f), 0, 1)));
                }
                else
                {
                    renderMaterial.SetColor("_Color", Color.white);
                }
            }
        }
        
    }

    public Vector3 RandomNavSphere(Vector3 position, float radius, int layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(0, radius);
        randomDirection += position;

        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(randomDirection, out navMeshHit, radius, layerMask))
        {
            return navMeshHit.position;
        }
        else
        {
            timeSinceLastWander = wanderTime * Random.Range(0.85f, 1);
            return position;
        }
    }
    
}
