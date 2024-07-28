using System.Collections;
using UnityEngine;

public class LobyManager : MonoBehaviour
{
    [Header("UI")]
    //스타트
    [SerializeField] private GameObject start;
    //로딩
    [SerializeField] private GameObject loading;
    private bool active_Loading = false;
    //로비
    [SerializeField] private GameObject loby;
    //[SerializeField] private Descriptions descriptions;
    //private BtnNum btnNum;
    //public int textNum;
    //설정
    [SerializeField] private GameObject option_menu;
    private bool active_Option = false;
    //게임종료
    [SerializeField] private GameObject exit_Game;
    private bool active_Exit_game = false;

    private void Update()
    {
        Active_Option();
        
        StartCoroutine(Active_Loading());
        
        Active_Exit_Game();
    }

    //public void Set_Description_Box()
    //{
    //    Debug.Log("메서드 호출");
    //    btnNum = GetComponent<BtnNum>();
    //    textNum = btnNum.btn_Num;
    //    descriptions.des_Num = textNum;
    //}

    public void OnClick_Set_Loby()
    {
        if (loading != null)
        {
            if (!active_Loading)
            {
                active_Loading = true;
            }
            else if (active_Loading)
            {
                active_Loading = false;
            }
        }
        else 
        {
            Debug.Log("Loading Page Null");
        }
    }

    IEnumerator Active_Loading()
    {
        if (active_Loading)
        {
            start.gameObject.SetActive(false);
            loading.gameObject.SetActive(true);
            yield return new WaitForSeconds(5.5f);
            loading.gameObject.SetActive(false);
            active_Loading = false;
            loby.gameObject.SetActive(true);
        }
    }

    public void OnClick_Set_Option()
    {
        if (option_menu != null)
        {
            if (!active_Option)
            {
                active_Option = true;
            }
            else
            {
                active_Option = false;
            }
        }
        else 
        {
            Debug.Log("Option_menu Null");
        }
    }

    private void Active_Option()
    {
        if (active_Option)
        {
            option_menu.gameObject.SetActive(true);
        }
        else if (!active_Option)
        {
            option_menu.gameObject.SetActive(false);
        }
    }

    public void OnClick_Exit_Game()
    {
        if (exit_Game != null)
        {
            if (!active_Exit_game)
            {
                active_Exit_game = true;
            }
            else if (active_Exit_game)
            {
                active_Exit_game = false;
            }
        }
        else 
        {
            Debug.Log("Exit Null");
        }
    }

    private void Active_Exit_Game()
    {
        if (active_Exit_game)
        {
            exit_Game.gameObject.SetActive(true);
        }
        else if (!active_Exit_game)
        {
            exit_Game.gameObject.SetActive(false);
        }
    }

    public void Game_Quit()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

}
