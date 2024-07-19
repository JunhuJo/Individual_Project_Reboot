using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public enum GameState
{
    Start,
    Lobby,
    Play,
    Reward
}

public class GameManager : MonoBehaviour
{
    public GameState currentState;

    [Header("Cursor_Settings")]
    [SerializeField] private Texture2D customCursorTexture; // Ŀ�� �̹���
    [SerializeField] private Vector2 hotSpot = Vector2.zero; // Ŀ�� �ֽ��� (�̹����� �߽��� �������� Ŀ�� ��ġ�� ����)

    

    private void Start()
    {
        
        ChangeState(GameState.Start);
        ChangeCursor(customCursorTexture, hotSpot);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.Start:
                // Game Start ����
                ChangeState(GameState.Lobby);
                break;
            case GameState.Lobby:
                // �κ� Scene �ε�
                SceneManager.LoadScene("GameLobby");
                break;
            case GameState.Play:
                // ���� �÷��� Scene �ε�
                SceneManager.LoadScene("GamePlay");
                break;
            case GameState.Reward:
                // ���� ó�� ����
                SceneManager.LoadScene("GameLobby");
                // �߰� ���� UI ó��
                // ���� UI ���� �߰� �ʿ�
                break;
        }
    }
    public void OnPlayGame()
    {
        ChangeState(GameState.Play);
    }

    public void OnGetReward()
    {
        ChangeState(GameState.Reward);
    }

    private void ChangeCursor(Texture2D cursorTexture, Vector2 hotSpot)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }
}
