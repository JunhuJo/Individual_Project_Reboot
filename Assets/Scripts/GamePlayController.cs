using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public GameManager gameManager;

    public void EndGame()
    {
        // ���� ���� ����
        gameManager.OnGetReward();
    }
}
