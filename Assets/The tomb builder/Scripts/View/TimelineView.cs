using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineView : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void UpdateTimeline(float value)
        => _slider.value = value;

    private void OnValidate()
    {
        if (_slider == null)
            _slider = GetComponent<Slider>();
    }
}
