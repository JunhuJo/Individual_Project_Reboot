using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public float moveSpeed;
    public RuntimeAnimatorController animatorController;
    public GameObject characterModel;
    public GameObject attackPrefab; // 공격 프리팹
    public GameObject skillPrefab; // 스킬 프리팹
    // 필요한 다른 속성들 추가
}