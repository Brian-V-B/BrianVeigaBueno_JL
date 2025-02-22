using UnityEngine;

public class JohnLemonCamera : MonoBehaviour
{
    public Transform Target;

    [SerializeField] private float _speed = 2.0f;

    private Vector3 _offset;


    // Start is called before the first frame update
    void Start()
    {
        if (Target != null)
            _offset = transform.position - Target.position;
    }


    private void LateUpdate() 
    {
        if (Target != null)
        {
            Vector3 tPosition = Target.position + _offset;

            transform.position = Vector3.Lerp(transform.position, tPosition, _speed * Time.deltaTime);
        }
    }
}
