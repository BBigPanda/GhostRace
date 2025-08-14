using System;
using Controllers;
using Enums;
using Managers;
using UniRx;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    [SerializeField] private CarController _carPrefab;
    [SerializeField] private Transform _spawnPoint;

    private IDisposable _disposable;

    private void Start()
    {
        InitSubscribes();
    }

    private void InitSubscribes()
    {
        _disposable = GameController.Instance.RaceTemporaryState.Subscribe(OnStateChange);
    }

    private void OnStateChange(RaceState state)
    {
        if (state == RaceState.Counting)
        {
            CarController instance = Instantiate(_carPrefab, _spawnPoint.position, Quaternion.identity);
            instance.Race();
            Camera.main.GetComponent<CameraFollowing>().SetTarget(instance.transform);
            if (RaceManager.Instance.RecordingRace != null)
            {
                instance = Instantiate(_carPrefab, _spawnPoint.position, Quaternion.identity);
                instance.Ghost();
            }
        }
    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
}