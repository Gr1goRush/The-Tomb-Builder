using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class BlockView : MonoBehaviour
{
    private bool _isBuilded;
    private float _speedFall = -6;

    [SerializeField] private Vector3 _stepIncreaseScale;
    [SerializeField] private float _speedIncreaseScale;
    [SerializeField] private Color _colorDrop;
    [SerializeField] private Vector2 _minScaleBlock;
    [Header("Components")]
    [SerializeField] private Transform _center;
    [SerializeField] private Image _image;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private RectTransform _rectTransform;

    public Rigidbody2D BaseRigidbody => _rigidbody;
    public Transform Center => _center;
    public float Width => _rectTransform.localScale.x;
    public Vector3 StepIncreaseScale => _stepIncreaseScale;
    public float SpeedIncreaseScale => _speedIncreaseScale;

    public ReactiveCommand OnBlockDownCommand = new();
    public ReactiveCommand OnSizeLimitExceededCommand = new();

    public void SetSpriteBlock(Sprite sprite)
        => _image.sprite = sprite;

    public void UpdatePosition(Vector2 newPosition)
        => transform.localPosition = newPosition;

    public void UpdateScale(Vector3 scale)
        => transform.localScale = scale;

    public void StartCoroutineDropBlock()
        => StartCoroutine(DropBlock());

    public IEnumerator DropBlock()
    {
        _image.color = _colorDrop;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    public bool TryTombBreak()
        => _rigidbody.velocity.y < _speedFall;

    public void NotCollisionWithWall()
    {
        if (_boxCollider != null)
        {
            var layerMask = LayerMask.GetMask("Wall");
            _boxCollider.excludeLayers = layerMask;
        }
    }

    public void SetStaticBodyTypeRigidbody()
    {
        if(_rigidbody != null)
            _rigidbody.bodyType = RigidbodyType2D.Static;
    }

    private void Start()
    {
        ManagerUniRx.AddObjectDisposable(OnBlockDownCommand);
        ManagerUniRx.AddObjectDisposable(OnSizeLimitExceededCommand);
    }

    private void FixedUpdate()
    {
        if (_isBuilded && TryTombBreak())
            OnBlockDownCommand.Execute();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var wallView = collision.gameObject.GetComponent<WallView>();
        if (_boxCollider.isTrigger == false && wallView)
        {
            _boxCollider.isTrigger = true;
            OnSizeLimitExceededCommand.Execute();
        }

        var blockVIew = collision.gameObject.GetComponent<BlockView>();
        if (blockVIew)
            _isBuilded = true;
    }

    private void OnValidate()
    {
        if (_image == null)
            _image = GetComponent<Image>();

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        if (_boxCollider == null)
            _boxCollider = GetComponent<BoxCollider2D>();

        if (_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnBlockDownCommand);
        ManagerUniRx.Dispose(OnSizeLimitExceededCommand);
    }
}
