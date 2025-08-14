using System;
using Controllers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;
using TMPro;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CountDownText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (!_text)
            _text = gameObject.GetComponent<TextMeshProUGUI>();
#endif
    }

    private void Start()
    {
        InitSubscribes();
    }

    private void InitSubscribes()
    {
        GameController.Instance.RaceTemporaryState.Subscribe(OnStateChange);
    }

    private void OnStateChange(RaceState state)
    {
        if (state == RaceState.Counting)
        {
            CountDownTimer();
        }
    }


    private async void CountDownTimer()
    {
        _text.DOFade(1, 0.5f);
        int time = 3;
        while (time > 0)
        {
            _text.text = time.ToString();
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime);
            time--;
        }

        _text.text = "GO";
        GameController.Instance.RaceTemporaryState.Value = RaceState.Start;
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), DelayType.Realtime);
        _text.DOFade(0, 0.5f);
    }
}