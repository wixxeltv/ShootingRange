using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [System.Serializable]
    struct GroupUi
    {
        [SerializeField] public RawImage image;
        [SerializeField] public TMP_Text text;
    }

    [SerializeField] private GroupUi[] tutoGroups;
    [SerializeField] private RawImage controller;

    void Start()
    {
        foreach (var group in tutoGroups)
        {
            group.image.canvasRenderer.SetAlpha(0f);
            group.text.canvasRenderer.SetAlpha(0f);
        }
        StartCoroutine(FadeController());
        StartCoroutine(FadeInputs());
    }

    IEnumerator FadeController()
    {
        controller.canvasRenderer.SetAlpha(0f);
        controller.CrossFadeAlpha(1f, 2f, false);
        yield return new WaitForSeconds(20f);
        controller.CrossFadeAlpha(0f, 2f, false); 
    }
    IEnumerator FadeInputs()
    {
        foreach (var group in tutoGroups)
        {
            group.image.CrossFadeAlpha(1f, 2f, false);
            group.text.CrossFadeAlpha(1f, 2f, false);
            
            yield return new WaitForSeconds(5f);
            
            group.image.CrossFadeAlpha(0f, 2f, false);
            group.text.CrossFadeAlpha(0f, 2f, false);
        }
    }
}