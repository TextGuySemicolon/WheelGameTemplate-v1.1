using UnityEngine;
using UnityEngine.UI;

public class LoseScreenAnimation : MonoBehaviour
{
    [SerializeField] private FaderUI fader;
    [SerializeField] private CanvasGroup overlayGroup;
    [SerializeField] private CanvasGroup contentGroup;

    private bool hide;

    private void OnEnable()
    {
        Show();
    }
    public void Show()
    {
        gameObject.SetActive(true);
        overlayGroup.alpha = 0f;
        LeanTween.value(gameObject, (float _alpha) => {
            overlayGroup.alpha = _alpha;
        }, 0f, 1f, .2f);

        contentGroup.alpha = 0f;
        LeanTween.value(gameObject, (float _alpha) => {
            contentGroup.alpha = _alpha;
        }, 0f, 1f, .2f).setDelay(0.2f);
    }
    public void Hide()
    {
        LeanTween.moveLocalY(contentGroup.gameObject, contentGroup.transform.position.y + 100f, .2f);
        LeanTween.value(gameObject, (float _alpha) =>
        {
            contentGroup.alpha = _alpha;
        }, 1f, 0f, .2f);

        fader.LoadScene("StartScreen");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !hide)
        {
            Hide();
            hide = true;
        }
    }
}
