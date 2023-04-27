using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public float fadeOutTime = 1f;
    public float fadeInTime = 1f;
    public string nextScene;

    private Image fadeImage;

    public void sceneLoad(string targetScene)
    {
        fadeImage = GameObject.Find("fadeOut").GetComponent<Image>();
        StartCoroutine(FadeOut(targetScene));

    }

    private IEnumerator FadeOut(string targetScene)
    {
        float t = 0f;
        Color color = fadeImage.color;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeOutTime);
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }
        SceneManager.LoadScene(targetScene);
        fadeImage = GameObject.Find("fadeOut").GetComponent<Image>();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        Color color = fadeImage.color;
        while (t < fadeInTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeInTime);
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }
    }
}