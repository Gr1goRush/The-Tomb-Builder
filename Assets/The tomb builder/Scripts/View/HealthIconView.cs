using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIconView : MonoBehaviour
{
    [SerializeField] private Image _viewIcon;

    public void SetIcon(Sprite icon)
        => _viewIcon.sprite = icon;

    public void DestroyHealth()
        => Destroy(gameObject);

    private void OnValidate()
    {
        if (_viewIcon == null)
            _viewIcon = GetComponent<Image>();
    }
}
