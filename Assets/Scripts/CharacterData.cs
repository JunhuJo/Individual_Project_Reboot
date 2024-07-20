using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public float moveSpeed;
    public RuntimeAnimatorController animatorController;
    public GameObject characterModel;
    public GameObject attackPrefab; // ���� ������
    public GameObject skillPrefab; // ��ų ������
    // �ʿ��� �ٸ� �Ӽ��� �߰�
}