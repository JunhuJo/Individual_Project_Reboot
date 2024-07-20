using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterData> characterDataList;

    public GameObject CreateCharacter(int characterIndex, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (characterIndex < 0 || characterIndex >= characterDataList.Count)
        {
            Debug.LogError("Invalid character index!");
            return null;
        }

        CharacterData data = characterDataList[characterIndex];
        GameObject character = Instantiate(data.characterModel, spawnPosition, spawnRotation);
        PlayerController controller = character.GetComponent<PlayerController>();
        controller.characterData = data;

        return character;
    }
}
