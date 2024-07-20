using Cinemachine;
using Mirror;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : NetworkBehaviour
{
    public CharacterData characterData; // ScriptableObject
    [SerializeField] private TextMesh class_Name;
    private Animator animator;
    private KannaSkillManager kannaSkillManager;
    private NavMeshAgent navMeshAgent;
    private Vector3 previousPosition;

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    //private string character_Name;

    [SyncVar] private Vector3 syncPosition;
    [SyncVar] private Quaternion syncRotation;
    [SyncVar(hook = nameof(OnChangeWalkingState))] private bool isWalking;
    [SyncVar(hook = nameof(OnChangeCharacterName))] private string syncCharacterName;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = characterData.animatorController; // Set animator controller from character data
        kannaSkillManager = GetComponentInChildren<KannaSkillManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = characterData.moveSpeed; // Set move speed from character data

        if (isLocalPlayer)
        {
            OnStartLocalPlayer();
            CmdProvideCharacterName(characterData.characterName);
        }

        previousPosition = transform.position;
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            HandleMovement();
            HandleActions(); // Ensure HandleActions is being called

            // Update position and rotation information
            CmdProvidePositionToServer(transform.position, transform.rotation, animator.GetBool("isWalking"));
        }
        else
        {
            // Apply synchronized position and rotation from the server
            SmoothMovement();
        }
    }

    [Command]
    private void CmdProvideCharacterName(string name)
    {
        syncCharacterName = name;
    }

    private void OnChangeCharacterName(string oldName, string newName)
    {
        if (class_Name != null)
        {
            class_Name.text = newName;
        }
    }

    [Command]
    private void CmdProvidePositionToServer(Vector3 position, Quaternion rotation, bool walkingState)
    {
        syncPosition = position;
        syncRotation = rotation;
        isWalking = walkingState;
    }

    private void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, syncPosition, Time.deltaTime * characterData.moveSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, syncRotation, Time.deltaTime * characterData.moveSpeed);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        if (isLocalPlayer)
        {
            class_Name.text = characterData.characterName;

            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

            if (virtualCamera != null)
            {
                virtualCamera.Follow = transform;
            }
        }
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

    private void OnAnimatorMove()
    {
        if (isLocalPlayer && navMeshAgent.enabled)
        {
            // Move using root motion
            transform.position += animator.deltaPosition;
            navMeshAgent.nextPosition = transform.position;
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

    [Command]
    private void CmdAttack()
    {
        RpcAttack();
    }

    [Command]
    private void CmdUseSkill()
    {
        RpcUseSkill();
    }

    [ClientRpc]
    private void RpcAttack()
    {
        kannaSkillManager.Attack();
        // 공격 프리팹 인스턴스화
        //Instantiate(characterData.attackPrefab, transform.position + transform.forward, transform.rotation);
    }

    [ClientRpc]
    private void RpcUseSkill()
    {
        kannaSkillManager.UseSkill();
        // 스킬 프리팹 인스턴스화
        //Instantiate(characterData.skillPrefab, transform.position + transform.forward, transform.rotation);
    }

    // Animation event function
    public void MoveStep()
    {
        if (!isLocalPlayer) return;

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
