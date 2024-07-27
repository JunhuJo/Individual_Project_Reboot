using UnityEngine;

public class KannaSkillManager : MonoBehaviour
{
    [Header("Kanna Weapon")]
    [SerializeField] private GameObject katana;  // �ߵ� īŸ��
    [SerializeField] private GameObject hide_katana;    // ���� īŸ��
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void UseSkill()
    {
        animator.SetTrigger("UseSkill");
    }

    public void SetAttackKatana()
    {
        katana.SetActive(true);
        hide_katana.SetActive(false);
    }

    public void SetNonAttackKatana()
    {
        hide_katana.SetActive(true);
        katana.SetActive(false);
    }
}
