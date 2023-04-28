using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public float fadeOutTime = 1f;
    public float fadeInTime = 1f;
    public string nextScene;

    public Image fadeImage;

    public GameObject fadeOutPrefab;  // assign the prefab in the Inspector

    public void CreateFadeOutObject()
    {
        Debug.Log("Create FadeOut Object");

        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas != null)
        {
            GameObject fadeOut = Instantiate(fadeOutPrefab, canvas.transform);

            fadeOut.transform.localPosition = Vector3.zero;
            fadeOut.transform.localScale = Vector3.one;

            RectTransform rectTransform = fadeOut.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;

            //fadeOut.GetComponent<Canvas>().sortingOrder = 999;

            Debug.Log("Create FadeOut Object Done");
        }
        else
        {
            Debug.LogError("Could not find Canvas object in the scene!");
        }
    }

    public void sceneLoad(string targetScene)
    {
        CreateFadeOutObject();

        fadeImage = GameObject.Find("fadeOut(Clone)").GetComponent<Image>();
        Debug.Log("fadeImage.name : " + fadeImage.name);
        StartCoroutine(FadeOut(targetScene));
    }

    private IEnumerator FadeOut(string targetScene)
    {
        Debug.Log("FadeOut Init");
        float t = 0f;
        Color color = fadeImage.color;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeOutTime);
            Debug.Log("alpha : " + alpha);
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        GameManager.Instance.sceneInit(targetScene);
        //SceneManager.LoadScene(targetScene);
        CreateFadeOutObject();
        fadeImage = GameObject.Find("fadeOut(Clone)").GetComponent<Image>();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Debug.Log("FadeIn Init");
        float t = 0f;
        Color color = fadeImage.color;
        while (t < fadeInTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeInTime);
            Debug.Log("alpha : " + alpha);
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }
        Destroy(fadeImage.gameObject);
    }
}