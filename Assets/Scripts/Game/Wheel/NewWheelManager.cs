using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class NewWheelManager : MonoBehaviour
{
    [SerializeField] private WheelBoard board;

    [SerializeField] private FadePage displayFadePageUI;
    [SerializeField] private FadePage resultPage;
    [SerializeField] private PopPage menuPage;
    [SerializeField] private TextMeshProUGUI displayText;

    [SerializeField] private RectTransform spinningContentRect;

    [Header("Audio")]
    [SerializeField] private AudioEvent playingAudio;
    [SerializeField] private AudioEvent stopAudio;
    [SerializeField] private AudioEvent rewardAudio;
    [SerializeField] private AudioEvent pointerAudio;

    public virtual float GetDuration() => 12f;

    //private void Start() => Test();

    void Test(){
        board.ReadData();
        Dictionary<string, int> counter = new Dictionary<string, int>();

        for(int i = 0; i < 1000000; i++)
        {
            WheelItemSO _target = GetTarget();
            if (counter.ContainsKey(_target.displayName))
            {
                counter[_target.displayName]++;
            }
            else
            {
                counter[_target.displayName] = 1;
            }
        }

        foreach(string key in counter.Keys)
        {
            Debug.Log($"{key} : {counter[key]}");
        }
    }

    public void Spin()
    {
        if (!WheelBoard.isReady) return;

        board.ReadData();

        WheelItemSO _target = GetTarget();

        resultPage.HideGroup();
        menuPage.Hide();

        if (_target == null)
        {
            LeanTween.delayedCall(gameObject, 1f, () =>
            {
                displayFadePageUI.ShowGroup();
                displayText.text = $"<color=red>SORRY!\n there's no item left.</color>";
            });
        }
        else
        {
            playingAudio.Play();

            //fade music volume
            LeanTween.value(gameObject, (float _value) => {
                AudioSetting.MusicVolume = _value;
            }, AudioSetting.MusicVolume, 0.4f, 0.2f);

            int _extra = Random.Range(6, 9);
            LeanTween.rotateZ(spinningContentRect.gameObject, board.GetRotationZ(_target) + (360f * _extra), GetDuration()).setEaseOutQuad();
            LeanTween.delayedCall(gameObject, GetDuration(), () => board.AnimateSector(_target));
            LeanTween.delayedCall(gameObject, GetDuration(), () => {
                playingAudio.Stop();
                stopAudio.Play();
                LeanTween.delayedCall(gameObject, 2f, () => {

                    //fade music volume
                    LeanTween.value(gameObject, (float _value) => {
                        AudioSetting.MusicVolume = _value;
                    }, AudioSetting.MusicVolume, 0.8f, 0.6f);

                    displayFadePageUI.ShowGroup();
                    displayText.text = $"Congratulations! you've won\n<b>{_target.GetDisplayName()}</b>!";

                    OnItemDrawn(_target);

                    rewardAudio.Play();
                });
            });
        }

    }

    protected virtual void OnItemDrawn(WheelItemSO _target)
    {
        if (board.GetDatabase().spinType == SpinType.Quantity)
        {
            _target.quantity--;
            board.UpdateQuantity(_target.id, _target.quantity);
        }else if(board.GetDatabase().spinType == SpinType.Percentage)
        {
            //No reason to update percentage right now
        }
    }

    public WheelItemSO GetTarget()
    {
        if(board.GetDatabase().spinType == SpinType.Quantity)
        {
            return GetQuantityTarget();
        }else if(board.GetDatabase().spinType == SpinType.Percentage)
        {
            return GetPercentageTarget();
        }
        return null;
    }
    public WheelItemSO GetQuantityTarget()
    {
        int _total = 0;
        foreach (WheelItemSO _data in board.GetUniqueData())
        {
            _total += _data.quantity;
        }

        int _rand = Random.Range(0, _total);

        foreach (WheelItemSO _data in board.GetUniqueData())
        {
            _rand -= _data.quantity;
            if (_rand < 0) return _data;
        }

        return null;
    }
    public WheelItemSO GetPercentageTarget()
    {
        float _total = 0;
        var uniques = board.GetUniqueData();
        foreach (WheelItemSO _data in uniques)
        {
            _total += _data.percentage;
        }

        float _rand = Random.Range(0, _total);

        uniques.OrderBy(item => item.percentage);
        uniques.Reverse();

        float counter = 0;
        foreach (WheelItemSO _data in uniques)
        {
            counter += _data.percentage;
            if (_rand <= counter)
            {
                return _data;
            }
        }

        return null;
    }
}
