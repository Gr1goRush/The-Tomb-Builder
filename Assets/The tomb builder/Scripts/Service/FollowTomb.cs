using UnityEngine;

public class FollowTomb : MonoBehaviour
{
    private Vector3 _target;

    [SerializeField] private float _speed;

    public void SetTarget(Vector3 position)
    {
        if(position.y > 0)
            _target = new Vector3(transform.position.x, position.y);
    }

    private void Move()
    {
        var newPosition = Vector3.Lerp(transform.position, _target, _speed);
        transform.position = new Vector3(transform.position.x, newPosition.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, _target) > 0.1)
            Move();
    }
}
