using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{

    [SerializeField] private Image _bar;
    [SerializeField] private Text _label;

    public void Display(float currentHealth, float maxHealth)
    {
        maxHealth = Mathf.Max(0, maxHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        _bar.fillAmount = currentHealth > 0 ? currentHealth / maxHealth : 0;
        _label.text = Mathf.FloorToInt(currentHealth).ToString();
    }

}
