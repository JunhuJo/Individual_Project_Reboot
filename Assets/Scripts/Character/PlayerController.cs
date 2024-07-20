using Cinemachine;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;
    private KannaSkillManager kanna_skill_Manager;
    private NavMeshAgent navMeshAgent;
    private Vector3 previousPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        kanna_skill_Manager = GetComponentInChildren<KannaSkillManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (isLocalPlayer)
        {
            Camera.main.GetComponent<CameraController>().target = this.transform;
        }

        previousPosition = transform.position;
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        HandleMovement();
        HandleActions();
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
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

            // 방향 업데이트
            Vector3 direction = navMeshAgent.steeringTarget - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);
            }

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                animator.SetBool("isWalking", false);
                navMeshAgent.ResetPath(); // 목적지에 도달하면 경로 초기화
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
            // 루트 모션을 통해 이동
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
        kanna_skill_Manager.Attack();
    }

    [ClientRpc]
    private void RpcUseSkill()
    {
        kanna_skill_Manager.UseSkill();
    }

    // 애니메이션 이벤트 함수
    public void MoveStep()
    {
        if (!isLocalPlayer) return;

        // 애니메이션 이벤트에 의해 호출되는 이동 로직
        // 루트 모션을 통해 이동
        Vector3 worldDeltaPosition = navMeshAgent.nextPosition - transform.position;
        transform.position += worldDeltaPosition;
    }
}
