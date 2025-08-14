using System;
using TMPro;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RaceNumberField : MonoBehaviour
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
        UIManager.Instance.RaceNumber.Subscribe(OnChangeRaceNumber);
    }


    private void OnChangeRaceNumber(int raceNumber)
    {
        _text.text = $"Race Number: {raceNumber}";
    }
}