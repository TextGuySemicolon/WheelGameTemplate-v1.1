using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenAnimation : MonoBehaviour
{
    [SerializeField] private FaderUI fader;
    [SerializeField] private CanvasGroup overlayGroup;
    [SerializeField] private CanvasGroup contentGroup;
    [SerializeField] private RectTransform trophyRect;

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

        trophyRect.localScale = Vector3.zero;
        LeanTween.scale(trophyRect.gameObject, Vector3.one, 0.5f).setEaseOutBack().setDelay(0.2f);

        InvokeRepeating("TrophyAnimation", 1f, 4f);
    }

    private void TrophyAnimation()
    {
        trophyRect.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        LeanTween.rotateZ(trophyRect.gameObject, -5f, 0.1f).setLoopPingPong(2);
    }
    public void Hide()
    {
        LeanTween.rotateZ(trophyRect.gameObject, 0f, 0.1f);
        CancelInvoke("TrophyAnimation");

        LeanTween.moveLocalY(contentGroup.gameObject, contentGroup.transform.position.y + 100f, .2f);
        LeanTween.value(gameObject, (float _alpha) =>
        {
            contentGroup.alpha = _alpha;
        }, 1f, 0f, .2f);

        fader.LoadScene("StartScreen");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && !hide)
        {
            Hide();
            hide = true;
        }
    }
}
