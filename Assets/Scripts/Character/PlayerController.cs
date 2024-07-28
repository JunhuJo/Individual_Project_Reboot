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
    [SerializeField] private GameObject Effect_prefab; // ����Ʈ ������ ����
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
        // ĳ���Ϳ��� ICharacterSkill ������Ʈ�� ã���ϴ�.
        skill_Manager = character.GetComponent<ISkill>();

        if (skill_Manager == null)
        {
            Debug.Log("��ų �Ŵ��� ���� Ȯ�� ���");
        }
    }


    private void Player_Movement()
    {
        //�̵�
        if (Input.GetMouseButton(0))// ��Ŭ�� ��
        {
            // Ŭ���� ��ġ�� UI ������ Ȯ��
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // Ŭ���� ��ġ�� UI��� ��ȯ
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                NavAgent_Player.SetDestination(hit.point); // Ŭ���� ��ġ�� �̵�
            }

            GameObject pointer_Effect = Instantiate(Effect_prefab, hit.point, Quaternion.identity);
            Destroy(pointer_Effect, 0.7f);
        }
        // �̵� ������ Ȯ���ϰ� �ִϸ��̼� ���¸� ����
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
