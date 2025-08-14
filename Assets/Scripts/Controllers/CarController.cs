using System;
using Ashsvp;
using Components;
using Controllers;
using Enums;
using UniRx;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private SimcadeVehicleController _simcadeVehicleController;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private AudioSystem _audioSystem;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _material;

    private IDisposable _disposable;

    public void Race()
    {
        gameObject.AddComponent<RaceRecording>();
        _simcadeVehicleController.enabled = false;
        _disposable = GameController.Instance.RaceTemporaryState.Subscribe(state =>
        {
            if (state == RaceState.Start)
            {
                _simcadeVehicleController.enabled = true;
            }
        });
    }

    public void Ghost()
    {
        Material[] mats = _meshRenderer.materials;
        mats[0] = _material;
        _meshRenderer.materials = mats;
        Destroy(_simcadeVehicleController);
        Destroy(_rigidbody);
        Destroy(_audioSystem);
        Destroy(_boxCollider);
        gameObject.AddComponent<GhostPlayer>();
    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
}