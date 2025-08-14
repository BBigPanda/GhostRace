using System;
using System.Collections.Generic;
using Controllers;
using Enums;
using Interfaces;
using Models;
using UniRx;
using UnityEngine;

namespace Managers
{
    public class RaceManager : SingletonMono<RaceManager>
    {
        public ReactiveProperty<bool> Recording = new ReactiveProperty<bool>(false);
        public List<RecModel> RecordingRace { private set; get; }

        private IDisposable _disposable;

        public void SetRecordingRace(List<RecModel> recordingRace)
        {
            if (recordingRace != null && recordingRace.Count > 0)
                RecordingRace = recordingRace;
        }

        private void Start()
        {
            InitSubscribes();
        }

        //Subscribes
        private void InitSubscribes()
        {
            _disposable = GameController.Instance.RaceTemporaryState.Subscribe(OnStateChange);
        }

        // Called When Race State on Game Controller is changed 
        private void OnStateChange(RaceState state)
        {
            if (state == RaceState.Start)
            {
                Recording.Value = true;
            }
            else if (state == RaceState.Finished)
            {
                Recording.Value = false;
            }
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}