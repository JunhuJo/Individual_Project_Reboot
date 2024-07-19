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
    [SerializeField] private Texture2D customCursorTexture; // 커서 이미지
    [SerializeField] private Vector2 hotSpot = Vector2.zero; // 커서 핫스팟 (이미지의 중심을 기준으로 커서 위치를 설정)

    

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
                // Game Start 로직
                ChangeState(GameState.Lobby);
                break;
            case GameState.Lobby:
                // 로비 Scene 로드
                SceneManager.LoadScene("GameLobby");
                break;
            case GameState.Play:
                // 게임 플레이 Scene 로드
                SceneManager.LoadScene("GamePlay");
                break;
            case GameState.Reward:
                // 보상 처리 로직
                SceneManager.LoadScene("GameLobby");
                // 추가 보상 UI 처리
                // 보상 UI 로직 추가 필요
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
