using Photon.Deterministic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [field: SerializeField] public UIHealthBar HealthBar { get; private set; }
    [SerializeField] private GameObject _deathScreen;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Only one instance of {nameof(UIController)} should be present on the scene.", this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void ShowDeathScreen()
    {
        _deathScreen.SetActive(true);
    }

}
