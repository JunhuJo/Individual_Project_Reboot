using UnityEngine;
using UnityEngine.UI;

public class Descriptions : MonoBehaviour
{
    [Header("Btn")]
    [SerializeField] private BtnNum story;
    [SerializeField] private BtnNum tower;
    [SerializeField] private BtnNum characterInfo;
    [SerializeField] private BtnNum shop;

    [Header("Description")]
    public Text description_Title;
    public Text description_Text;
    public int des_Num;

    private BtnNum btn_Num;
    //private int text_Num;

    [SerializeField] private TextData[] textDatas;

    private void Update()
    {
        View_Description(des_Num);
    }

    private void View_Description(int num)
    {
        description_Title.text = textDatas[num].description_Title;
        description_Text.text = textDatas[num].description_Text;
    }

    public void Trigger_Btn()
    {
        if (gameObject.name == "Story_Mode_Btn")
        {
            des_Num = 0;
        }
        else if (gameObject.name == "Tower_Mode_Btn")
        {
            des_Num = 1;
        }
        else if (gameObject.name == "Character_Info_Btn")
        {
            des_Num = 2;
        }
        else if (gameObject.name == "Shop_Btn")
        {
            des_Num = 3;
        }
    }
   
}
