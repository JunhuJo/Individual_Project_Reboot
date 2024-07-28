using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    [Header("Character Data")]
    public CharacterData characterData; // ScriptableObject

    private Transform transform_Player;
    private NavMeshAgent NavAgent_Player;
    private Animator animator;
    private ISkill skill_Manager;

    [Header("View")]
    [SerializeField] private GameObject Effect_prefab; // 이펙트 프리팹 변수
    [SerializeField] private TextMesh class_Name;

    private Vector3 previousPosition;

    

    private void Start()
    {
        SelectCharacter(gameObject);
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = characterData.animatorController; // Set animator controller from character data
        NavAgent_Player = GetComponent<NavMeshAgent>();
        transform_Player = GetComponent<Transform>();
        NavAgent_Player.speed = characterData.moveSpeed; // Set move speed from character data
    }

    private void Update()
    {
        Player_Movement();
        skill_Manager.UseSkill();
    }

    public void SelectCharacter(GameObject character)
    {
        // 캐릭터에서 ICharacterSkill 컴포넌트를 찾습니다.
        skill_Manager = character.GetComponent<ISkill>();

        if (skill_Manager == null)
        {
            Debug.Log("스킬 매니저 없음 확인 요망");
        }
    }


    private void Player_Movement()
    {
        //이동
        if (Input.GetMouseButton(0))// 좌클릭 시
        {
            // 클릭한 위치가 UI 위인지 확인
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // 클릭한 위치가 UI라면 반환
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                NavAgent_Player.SetDestination(hit.point); // 클릭한 위치로 이동
            }

            GameObject pointer_Effect = Instantiate(Effect_prefab, hit.point, Quaternion.identity);
            Destroy(pointer_Effect, 0.7f);
        }
        // 이동 중인지 확인하고 애니메이션 상태를 업뎃
        if (NavAgent_Player.remainingDistance > NavAgent_Player.stoppingDistance && gameObject.transform.position != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    //public void MoveStep()
    //{
    //    Vector3 worldDeltaPosition = NavAgent_Player.nextPosition - transform.position;
    //    transform.position += worldDeltaPosition;
    //}
    //
    //private void OnChangeWalkingState(bool oldValue, bool newValue)
    //{
    //    animator.SetBool("isWalking", newValue);
    //}
}
