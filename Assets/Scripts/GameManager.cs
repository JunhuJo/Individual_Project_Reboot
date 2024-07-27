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
    [SerializeField] private Texture2D customCursorTexture; // Ŀ�� �̹���
    private Vector2 hotSpot = Vector2.zero; // Ŀ�� �ֽ��� (�̹����� �߽��� �������� Ŀ�� ��ġ�� ����)

    [Header("UI")]
    [SerializeField] private GameObject escMenu;
    [SerializeField] private RectTransform mission_window;
    private bool mission_move = false;

    [Header("Sound_Manager")]
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private bool gamePlayeScene = false;




    private void Start()
    {
        //Ŀ�� �̹��� ����
        ChangeCursor(customCursorTexture, hotSpot);

        //�÷��̾� ����(ī�޶� ����)
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
        // ��ũ��Ʈ�� ��Ȱ��ȭ�� �� �⺻ Ŀ���� ����
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
