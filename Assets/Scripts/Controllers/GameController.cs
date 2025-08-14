using Enums;
using Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class GameController : SingletonMono<GameController>
    {
        public ReactiveProperty<RaceState> RaceTemporaryState = new ReactiveProperty<RaceState>();

        private void Start()
        {
            // Scene Initial Values 
            Application.targetFrameRate = 60;
            RaceTemporaryState.Value = RaceState.None;
            InitSubscribes();
        }

        private void InitSubscribes()
        {
            UIManager.Instance.StartRaceCommand.Subscribe(_ => { RaceTemporaryState.Value = RaceState.Counting; });
            UIManager.Instance.ResetRaceCommand.Subscribe(_ =>
            {
                RaceTemporaryState.Value = RaceState.None;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                UIManager.Instance.RaceNumber.Value++;
            });
        }
    }
}