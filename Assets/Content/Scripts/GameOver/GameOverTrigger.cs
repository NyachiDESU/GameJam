using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTrigger : MonoBehaviour
{
    public static Action OnGameEnded;

    private void OnTriggerEnter(Collider other)
    {
        OnGameEnded?.Invoke();
    }
}