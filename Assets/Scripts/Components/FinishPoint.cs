using Controllers;
using Enums;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag("Player"))
        {
            GameController.Instance.RaceTemporaryState.Value = RaceState.Finished;
        }
    }
}