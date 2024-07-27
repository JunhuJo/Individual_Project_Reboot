using UnityEngine;

public class KannaSkillManager : MonoBehaviour, ISkill
{
    [Header("Kanna Weapon")]
    [SerializeField] private GameObject katana;  // 발도 카타나
    [SerializeField] private GameObject hide_katana;    // 검집 카타나
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void UseSkill()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Kanna_Base_Attack();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Kanna_Skill_A();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Kanna_Skill_B();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Kanna_Skill_C();
        }
    }

    private void Kanna_Base_Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void Kanna_Skill_A()
    {
        animator.SetTrigger("UseSkill");
    }

    private void Kanna_Skill_B()
    {
        animator.SetTrigger("UseSkill");
    }

    private void Kanna_Skill_C()
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
