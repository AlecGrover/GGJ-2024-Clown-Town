using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Enums;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour
{
    
    [SerializeField]
    private Character characterProfile;

    [SerializeField]
    private NPCInfo infoDisplay;
    [SerializeField]
    private Image happinessImage;
    [SerializeField]
    public Image AffectedHighlight;
    private GameMode gameMode;

    [SerializeField, ReadOnly(true)]
    private float happiness = 1;
    
    [Header("AI")]
    [SerializeField]
    private float wanderRange = 10f;
    [SerializeField]
    private float wanderTime = 10f;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float conversationDistance = 1f;
    
    [Header("AI Decision Weights")]
    [Tooltip("The higher the value relative to other weights, the more likely the NPC will choose to walk around")]
    [SerializeField][Range(0,10)]
    private float wanderWeight = 1f;
    [Tooltip("The higher the value relative to other weights, the more likely the NPC will choose to engage in a conversation")]
    [SerializeField][Range(0,10)]
    private float converseWeight = 1f;
    [Tooltip("The higher the value relative to other weights, the more likely the NPC will choose to interact with an object in the scene")]
    [SerializeField][Range(0,10)]
    private float interactWeight = 1f;
    [Tooltip("The higher the value relative to other weights, the more likely the NPC will choose to do nothing")]
    [SerializeField][Range(0,10)]
    private float idleWeight = 1f;
    
    private float timeSinceLastWander = 0f;
    private ENPCPathState pathState = ENPCPathState.Idle;

    public GameObject coinPrefab;
    
    public bool SeekingConversation { get; private set; }
    public NPC ConversationTarget { get; private set; }

    [SerializeField] private GameObject goodS, badS, neutralS;
    private AudioSource goodSAS, badSAS, neutralSAS;
    
    /*
     *  Cached Components
     */
    private Transform target;
    private NavMeshAgent navMeshAgent;
    
    /*
     *  Unity Lifecycle
     */
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        timeSinceLastWander = wanderTime * Random.Range(0.5f, 1f);
        SeekingConversation = false;
        Init();
        
        gameMode = FindObjectOfType<GameMode>();

        goodSAS = goodS.GetComponent<AudioSource>();
        badSAS = goodS.GetComponent<AudioSource>();
        neutralSAS = goodS.GetComponent<AudioSource>();
        
    }

    private void Init()
    {
        if (infoDisplay != null && characterProfile != null)
        {
            infoDisplay.SetHumorTypes(characterProfile.taste1, characterProfile.taste2);
            infoDisplay.SetSprite(characterProfile.sprite);
            infoDisplay.SetAnimator(characterProfile.animatorController);
        }
    }

    /*
     *  Updates
     */

    void FixedUpdate()
    {
        if (characterProfile != null)
        {
            float m = (7 / 4) * characterProfile.Patience + 6.25f;
            if (m != 0)
            {
                happiness -= Time.fixedDeltaTime / m;
            }
        }
        
        if (happiness <= -5 && gameMode != null) gameMode.LoseGame();
        
        happiness = Mathf.Clamp(happiness, -5, 5);
        
        if (happinessImage != null)
        {

            if (happiness > 0)
            {
                happinessImage.color = new Color(Math.Clamp(1 - (happiness * 0.2f), 0, 1), 1, Math.Clamp(1 - (happiness * 0.2f), 0, 1));
            }
            else if (happiness < 0)
            {
                happinessImage.color = new Color(1, Math.Clamp(1 - (-happiness * 0.2f), 0, 1), Math.Clamp(1 - (-happiness * 0.2f), 0, 1));
            }
            else
            {
                happinessImage.color = Color.white;
            }
        }
    }
    
    void Update()
    {
        Pathfind();
        /*
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material renderMaterial = renderer.material;
            if (renderMaterial != null)
            {
                switch (pathState)
                {
                    case (ENPCPathState.Wander):
                        renderMaterial.SetColor("_Color", Color.blue);
                        break;
                    case (ENPCPathState.Converse):
                        if (SeekingConversation)
                        {
                            renderMaterial.SetColor("_Color", Color.yellow);
                        }
                        else if (!navMeshAgent.isStopped)
                        {
                            renderMaterial.SetColor("_Color", new Color(1f, 0.647f, 0));
                        }
                        else
                        {
                            renderMaterial.SetColor("_Color", Color.red);
                        }
                        break;
                    default:
                        renderMaterial.SetColor("_Color", Color.black);
                        break;
                }
            }
        }
        */
        
    }

    void LateUpdate()
    {
        if (SeekingConversation)
        {
            if (ConversationTarget!= null) EndConversation(false, true);
            List<NPC> ignoredNPCs = new List<NPC>();
            ignoredNPCs.Add(this);
            var NPCs = FindNPCsInRadius(transform.position, wanderRange, -1, ignoredNPCs);
            NPC conversationPartner = NPCs
                .FirstOrDefault(npc => npc.SeekingConversation && Vector3.Distance(transform.position, npc.transform.position) <=  Mathf.Min(wanderRange, npc.wanderRange));
            bool bFoundPartner = false;
            if (conversationPartner != null)
            {
                bFoundPartner = conversationPartner.ProposeConversation(this);
                if (!bFoundPartner) conversationPartner = null;
                else
                {
                    ConversationTarget = conversationPartner;
                    SeekingConversation = false;
                }
            }
        }
    }

    /*
     *  PlayerInteractions
     */
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

            if(LikeRatio > 0){
                if (goodSAS != null) goodSAS.Play();
                for(int i = 0; i < LikeRatio; i++){
                    Instantiate(coinPrefab, transform.position, transform.rotation);
                }
            }
            else if (LikeRatio < 0){
                if (badSAS != null) badSAS.Play();
            }
            else{
                if (neutralSAS != null) neutralSAS.Play();
            }
        }
        
        if (happinessImage != null)
        {

            if (happiness > 0)
            {
                happinessImage.color = new Color(Math.Clamp(1 - (happiness * 0.1f), 0, 1), 1, Math.Clamp(1 - (happiness * 0.1f), 0, 1));
            }
            else if (happiness < 0)
            {
                happinessImage.color = new Color(1, Math.Clamp(1 - (-happiness * 0.1f), 0, 1), Math.Clamp(1 - (-happiness * 0.1f), 0, 1));
            }
            else
            {
                happinessImage.color = Color.white;
            }
        }
        
    }

    /*
     *  AI
     */
    
    // Gets a random NavMesh point in a sphere around the NPC
    public Vector3 RandomNavSphere(Vector3 position, float radius, int layerMask, out bool foundNewDestination)
    {
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(0, radius);
        randomDirection += position;

        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(randomDirection, out navMeshHit, radius, layerMask))
        {
            NavMeshPath navMeshPath = new NavMeshPath();

            if (navMeshAgent.CalculatePath(navMeshHit.position, navMeshPath) &&
                navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                foundNewDestination = true;
                return navMeshHit.position;
            }
        }

        foundNewDestination = false;
        return position;
    }
    
    // Pathfinds the NPC if the wander timer has expired
    private void Pathfind()
    {
        if (!navMeshAgent.isOnNavMesh)
        {
            return;
        }

        timeSinceLastWander += Time.deltaTime;
        if (timeSinceLastWander >= wanderTime)
        {
            GetNewPathState();
            switch (pathState)
            {
                case (ENPCPathState.Wander):
                    Wander();
                    break;
                case (ENPCPathState.Converse):
                    Converse();
                    break;
                case (ENPCPathState.Idle):
                    Idle();
                    break;
                default:
                    Wander();
                    break;
            }
        }

        if (pathState == ENPCPathState.Converse)
        {
            if (ConversationTarget == null) SeekingConversation = true;
            SeekConversationPartner();
        }
        else
        {
            EndConversation(false, true);
        }
    }

    private void Wander()
    {
        bool bSuccess;
        Vector3 newTarget = RandomNavSphere(transform.position, wanderRange, -1, out bSuccess);
        if (bSuccess)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(newTarget);
            ResetWanderTimer();
        }
    }

    private void Idle()
    {
        navMeshAgent.isStopped = true;
    }
    
    private void GetNewPathState()
    {
        float decision = Random.Range(0, wanderWeight + converseWeight + interactWeight + idleWeight);
        if (decision < wanderWeight)
        {
            pathState = ENPCPathState.Wander;
        }
        else if (decision < wanderWeight + converseWeight)
        {
            pathState = ENPCPathState.Converse;
        }
        else if (decision < wanderWeight + converseWeight + interactWeight)
        {
            pathState = ENPCPathState.Interact;
        }
        else
        {
            pathState = ENPCPathState.Idle;
        }
        EndConversation(pathState != ENPCPathState.Converse, true);
    }

    private void Converse()
    {
        SeekingConversation = true;
        ResetWanderTimer();
    }

    public bool ProposeConversation(NPC sender)
    {
        if (SeekingConversation && ConversationTarget == null)
        {
            ConversationTarget = sender;
            SeekingConversation = false;
            return true;
        }

        return false;
    }

    public void EndConversation(bool resetWanderTimer = true, bool doRecursion = false)
    {
        SeekingConversation = false;
        if (doRecursion && ConversationTarget != null) ConversationTarget.EndConversation(false);
        ConversationTarget = null;
        navMeshAgent.isStopped = false;
        if (resetWanderTimer)
        {
            timeSinceLastWander = wanderTime;
        }
    }

    private void SeekConversationPartner()
    {
        if (ConversationTarget == null) return;

        if (ConversationTarget.ConversationTarget != this)
        {
            EndConversation(false);
        }

        if (!SeekingConversation)
        {
            if (Vector3.Distance(transform.position, ConversationTarget.transform.position) <= conversationDistance)
            {
                if (!navMeshAgent.isStopped) ResetWanderTimer();
                navMeshAgent.isStopped = true;
            }
            else
            {
                navMeshAgent.SetDestination(ConversationTarget.transform.position);
                navMeshAgent.isStopped = false;
            }
        }
    }
    
    /*
     *  Utility
     */
    public static List<NPC> FindNPCsInRadius(Vector3 position, float radius, int layerMask, List<NPC> ignoreList)
    {
        List<NPC> hits = Physics.OverlapSphere(position, radius, layerMask)
            .Select(x => x.GetComponent<NPC>())
            .Where(npc => npc != null && !ignoreList.Contains(npc))
            .ToList();
        return hits;
    }

    private void FaceTargetTransform(Transform targetTransform)
    {
        Vector3 targetDirection = targetTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1);
    }

    private void ResetWanderTimer()
    {
        timeSinceLastWander = Random.Range(0, 0.5f * wanderTime);
    }

    public void SetCharacter(Character profile)
    {
        characterProfile = profile;
        Init();
    }
    
}
