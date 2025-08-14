using System;
using System.Collections.Generic;
using Controllers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;
using Managers;
using Models;
using UniRx;
using UnityEngine;

namespace Components
{
    public class GhostPlayer : MonoBehaviour
    {
        private List<RecModel> _listPositions;

        public void Start()
        {
            InitSubscribes();
            _listPositions = RaceManager.Instance.RecordingRace;
            transform.DOMove(_listPositions[0].Position, Time.fixedDeltaTime);
        }

        private void InitSubscribes()
        {
            GameController.Instance.RaceTemporaryState.Subscribe(OnStateChange);
        }

        private void OnStateChange(RaceState state)
        {
            if (state == RaceState.Start)
            {
                PlayRecording();
            }
        }


        public async void PlayRecording()
        {
            Sequence seq = DOTween.Sequence();

            for (int i = 0; i < _listPositions.Count; i++)
            {
                seq.Append(transform.DOMove(_listPositions[i].Position, Time.fixedDeltaTime).SetEase(Ease.Linear));
            }

            seq.Play();
        }
    }
}