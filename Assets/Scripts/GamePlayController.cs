using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public GameManager gameManager;

    public void EndGame()
    {
        // 게임 종료 로직
        gameManager.OnGetReward();
    }
}
