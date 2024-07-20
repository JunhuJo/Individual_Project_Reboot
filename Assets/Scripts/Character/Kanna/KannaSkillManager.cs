using Mirror;
using UnityEngine;

public class KannaSkillManager : NetworkBehaviour
{
    [Header("Kanna Weapon")]
    [SerializeField] private GameObject katana;  // 발도 카타나
    [SerializeField] private GameObject hide_katana;    // 검집 카타나
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
