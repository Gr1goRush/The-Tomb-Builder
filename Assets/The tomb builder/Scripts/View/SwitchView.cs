using UnityEngine;
using UnityEngine.UI;

public class SwitchView : MonoBehaviour
{
    [SerializeField] private Image _currentSprite;
    [SerializeField] private Button _clickSwitch;
    [SerializeField] private Sprite _spriteOn;
    [SerializeField] private Sprite _spriteOff;

    public virtual void Switch() { AudioManager.Instance.PlayClickButton(); }

    public void ChangeSprite(string value)
    {
        switch (value)
        {
            case "1":
                _currentSprite.sprite = _spriteOn;
                break;
            case "0":
                _currentSprite.sprite = _spriteOff;
                break;
        }
    }

    protected virtual void Start()
    {
        _clickSwitch.onClick.AddListener(() => { Switch(); });
    }

    private void OnValidate()
    {
        if (_clickSwitch == null)
            _clickSwitch = GetComponent<Button>();

        if (_currentSprite == null)
            _currentSprite = GetComponent<Image>();
    }
}
