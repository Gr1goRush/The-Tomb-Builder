using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationView : MonoBehaviour
{
    [SerializeField] private Button _close;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _close.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            SetActive(false);
        });
    }
}
