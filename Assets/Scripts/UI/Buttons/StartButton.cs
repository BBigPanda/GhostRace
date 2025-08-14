using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (!_button)
                _button = GetComponent<Button>();
#endif
        }

        private void Start()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnClickStartRace);
        }

        private void OnClickStartRace()
        {
            UIManager.Instance.StartRaceCommand?.Execute();
            _button.gameObject.SetActive(false);
        }
    }
}