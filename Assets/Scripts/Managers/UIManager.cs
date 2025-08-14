using Interfaces;
using UniRx;
using UnityEngine;

public class UIManager : SingletonMono<UIManager>
{
    public ReactiveCommand StartRaceCommand = new ReactiveCommand();
    public ReactiveCommand ResetRaceCommand = new ReactiveCommand();
    public ReactiveProperty<int> RaceNumber = new ReactiveProperty<int>(1);
    public ReactiveProperty<int> CountDown = new ReactiveProperty<int>(1);
}