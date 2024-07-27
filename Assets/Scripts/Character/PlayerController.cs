using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    [Header("Character Data")]
    public CharacterData characterData; // ScriptableObject

    [Header("View")]
    [SerializeField] private GameObject Effect_prefab; // ÀÌÆåÆ® ÇÁ¸®ÆÕ º¯¼ö
    [SerializeField] private TextMesh class_Name;


    private Animator animator;
    private KannaSkillManager kannaSkillManager;
    private NavMeshAgent navMeshAgent;
    private Vector3 previousPosition;

    

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = characterData.animatorController; // Set animator controller from character data
        kannaSkillManager = GetComponentInChildren<KannaSkillManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = characterData.moveSpeed; // Set move speed from character data
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButton(0)) // Left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                navMeshAgent.SetDestination(hit.point);
            }


            GameObject pointer_Effect = Instantiate(Effect_prefab, hit.point, Quaternion.identity);
            Destroy(pointer_Effect, 0.7f);
        }

        if (navMeshAgent.isOnNavMesh && navMeshAgent.hasPath)
        {
            animator.SetBool("isWalking", true);

            // Update direction
            Vector3 direction = navMeshAgent.steeringTarget - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);
            }

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                animator.SetBool("isWalking", false);
                navMeshAgent.ResetPath(); // Clear path when destination is reached
            }
           
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }



    private void HandleActions()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CmdAttack();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            CmdUseSkill();
        }
    }

    
    private void CmdAttack()
    {
        RpcAttack();
    }
    
    
    private void CmdUseSkill()
    {
        RpcUseSkill();
    }
    
    
    private void RpcAttack()
    {
        kannaSkillManager.Attack();
    }
    
    
    private void RpcUseSkill()
    {
        kannaSkillManager.UseSkill();
    }

    // Animation event function
    public void MoveStep()
    {
        

        // Movement logic called by animation events
        // Move using root motion
        Vector3 worldDeltaPosition = navMeshAgent.nextPosition - transform.position;
        transform.position += worldDeltaPosition;
    }

    private void OnChangeWalkingState(bool oldValue, bool newValue)
    {
        animator.SetBool("isWalking", newValue);
    }
}
