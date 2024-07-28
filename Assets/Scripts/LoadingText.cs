using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    [SerializeField] private Text loading_Text;
    private int count = 0;

    private void Update()
    {
        StartCoroutine(Active_Loading_Text());
    }

    IEnumerator Active_Loading_Text()
    {
        while (count == 0)
        {
            if (gameObject.activeSelf)
            {
                loading_Text.text = $"Now Loading";
                count++;
                yield return new WaitForSeconds(0.5f);
                loading_Text.text = $"Now Loading.";
                count++;
                yield return new WaitForSeconds(0.5f);
                loading_Text.text = $"Now Loading. .";
                count++;
                yield return new WaitForSeconds(0.5f);
                loading_Text.text = $"Now Loading. . .";
                count++;
                yield return new WaitForSeconds(0.5f);
                count = 0;
            }
        }
    }
 }
