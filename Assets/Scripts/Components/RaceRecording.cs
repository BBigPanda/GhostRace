using System;
using System.Collections.Generic;
using Ashsvp;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;
using Managers;
using Models;
using UniRx;
using UnityEngine;

public class RaceRecording : MonoBehaviour
{
    private List<RecModel> _listPositions;
    private bool _recording;

    private IDisposable _disposable;

    void Start()
    {
        _listPositions = new List<RecModel>();
        InitSubscribes();
    }

    private void InitSubscribes()
    {
        _disposable = RaceManager.Instance.Recording.Subscribe(RaceRecordingState);
    }

    private void RaceRecordingState(bool recording)
    {
        _recording = recording;
        if (recording)
        {
            Rec();
        }
        else
        {
            Pause();
        }
    }

    private async void Rec()
    {
        while (_recording)
        {
            // rec transform position and rotation every fixed frame (0.002ms)
            _listPositions.Add(new RecModel() { Position = transform.position, Rotation = transform.rotation });
            await UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate);
        }
    }

    private async void Pause()
    {
        if (!_recording)
            RaceManager.Instance.SetRecordingRace(_listPositions);
    }


    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
}