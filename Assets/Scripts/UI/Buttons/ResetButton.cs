using System;
using Controllers;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResetButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private IDisposable _disposable;
    private void OnValidate()
    {
#if UNITY_EDITOR
        if (!_button)
            _button = GetComponent<Button>();
#endif
    }

    private void Start()
    {
        _button.gameObject.SetActive(false);
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClick);
        
        _disposable =   GameController.Instance.RaceTemporaryState.Subscribe(state =>
        {
            if (state == RaceState.Finished)
            {
                _button.gameObject.SetActive(true);
            }
        });
    }

    private void OnClick()
    {
        UIManager.Instance.ResetRaceCommand?.Execute();
    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
}