using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FaderUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private bool fadeInOnStart;

    private void Start()
    {
        if (fadeInOnStart) FadeIn();
    }

    public void FadeIn()
    {
        group.alpha = 1f;
        LeanTween.value(gameObject, (float _alpha) =>
        {
            AudioSetting.MusicVolume = 1f - _alpha;
            group.alpha = _alpha;
        }, 1f, 0f, 0.2f).setDelay(0.1f);
    }

    public void FadeOut()
    {
        group.alpha = 1f;
        LeanTween.value(gameObject, (float _alpha) =>
        {
            AudioSetting.MusicVolume = 1f - _alpha;
            group.alpha = _alpha;
        }, 0f, 1f, 0.2f);
    }

    public void LoadScene(string _sceneName)
    {
        FadeOut();
        LeanTween.delayedCall(gameObject, 1f, () => {
            SceneManager.LoadScene(_sceneName);
        });
    }
}
