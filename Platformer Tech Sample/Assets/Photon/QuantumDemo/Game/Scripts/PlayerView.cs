using Photon.Deterministic;
using Quantum;
using UnityEngine;

[RequireComponent(typeof(EntityView))]
public class PlayerView : MonoBehaviour
{

    private EntityView _view;

    private float _maxHealth;
    private float _currentHealth;

    private void Start()
    {
        _view = GetComponent<EntityView>();

        Frame f = QuantumRunner.Default.Game.Frames.Verified;

        PlayerLink playerLink = f.Get<PlayerLink>(_view.EntityRef);

        if (QuantumRunner.Default.Session.IsLocalPlayer(playerLink.Player))
        {
            Camera.main!.GetComponent<CameraFollow>().SetTarget(transform);

            // Subscribe to damaged event
            QuantumEvent.Subscribe<EventDamaged>(this, OnDamaged);
            // Save max and current health
            _maxHealth = f.Get<Health>(_view.EntityRef).Value.AsFloat;
            _currentHealth = _maxHealth;
            UpdateUI();
        }
    }

    private void OnDamaged(EventDamaged e)
    {
        _currentHealth -= e.Amount.AsFloat;
        UpdateUI();
    }

    private void UpdateUI()
    {
        UIController.Instance.HealthBar.Display(_currentHealth, _maxHealth);
        if (_currentHealth <= 0f) UIController.Instance.ShowDeathScreen();
    }

}