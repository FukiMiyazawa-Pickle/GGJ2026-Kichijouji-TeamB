using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [SerializeField] Image _fadeImage;
    [SerializeField] float _fadeDuration = 1f;

    public bool IsFading { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneWithFade(string sceneName)
    {
        if (IsFading) return;
        StartCoroutine(FadeAndLoad(sceneName));
    }

    IEnumerator FadeAndLoad(string sceneName)
    {
        IsFading = true;

        // フェードアウト
        yield return Fade(0f, 1f);

        SceneManager.LoadScene(sceneName);

        // フェードイン
        yield return Fade(1f, 0f);

        IsFading = false;
    }

        IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float time = 0f;
        Color color = _fadeImage.color;

        while (time < _fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / _fadeDuration);
            _fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        _fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
