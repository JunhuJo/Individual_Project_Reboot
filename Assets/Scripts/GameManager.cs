using UnityEngine;
using Cinemachine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Character_Create")]
    [SerializeField] private CinemachineVirtualCamera virtual_Camera;
    [SerializeField] private GameObject Player_Prefap;
    [SerializeField] private Transform player_Start_Position;


    [Header("Cursor_Settings")]
    [SerializeField] private Texture2D customCursorTexture; // 커서 이미지
    private Vector2 hotSpot = Vector2.zero; // 커서 핫스팟 (이미지의 중심을 기준으로 커서 위치를 설정)

    [Header("UI")]
    [SerializeField] private GameObject escMenu;
    [SerializeField] private RectTransform mission_window;
    private bool mission_move = false;

    [Header("Sound_Manager")]
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private bool gamePlayeScene = false;




    private void Start()
    {
        //커서 이미지 변경
        ChangeCursor(customCursorTexture, hotSpot);

        //플레이어 생성(카메라 붙임)
        GameObject Player = Instantiate(Player_Prefap, player_Start_Position);
        virtual_Camera.Follow = Player.transform;


        //miniMap.player = Player.transform;
        //player_Info = Player.GetComponent<PlayerInfo>();
        //player_Skill_Manager = Player.GetComponent<SkillManager>();
    }

    private void Update()
    {
        EscMeunOpen();
        Mission_Window_Move();
    }


    private void EscMeunOpen()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escMenu.activeSelf)
            {
                escMenu.gameObject.SetActive(true);
            }

            if (escMenu.activeSelf)
            {
                escMenu.gameObject.SetActive(false);
            }
        }
    }

    private void ChangeCursor(Texture2D cursorTexture, Vector2 hotSpot)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    private void OnDisable()
    {
        // 스크립트가 비활성화될 때 기본 커서로 복원
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnClick_Mission_Move()
    {
        if (!mission_move)
        {
            mission_move = true;
        }
        else if (mission_move)
        {
            mission_move = false;
        }
    }

    private void Mission_Window_Move()
    {
        Vector3 target_Pos = new Vector3(595, -9, 0);
        float move_Speed = 50.0f;
        if (mission_move)
        {
            target_Pos = new Vector3(1166, -9, 0);
        }
        else if (!mission_move)
        {
            target_Pos = new Vector3(595, -9, 0);
        }
        mission_window.anchoredPosition = Vector3.Lerp(mission_window.anchoredPosition, target_Pos, move_Speed * Time.deltaTime);
    }
}
