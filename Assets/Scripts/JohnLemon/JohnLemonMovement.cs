using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnLemonMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f,
                  _turnSpeed = 1.0f;


    private Vector3 _movement;


    private Rigidbody _rb;
    private Animator _animator;
    private AudioSource _sfxSource;



    // Start is called before the first frame update
    private void Start() 
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _sfxSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    private void Update() 
    {
        InputDetection();
    }


    private void FixedUpdate()
    {
        SfxFootsteps();
        UpdateDirection();
    }


    private void OnAnimatorMove() 
    {
        _rb.MovePosition(transform.position + transform.forward * _animator.deltaPosition.magnitude * _speed);
    }



    private void InputDetection() 
    {
        // Detectar y guardar inputs Horizontales y Verticales.
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = 0.0f;
        _movement.z = Input.GetAxis("Vertical");

        _animator.SetBool("IsWalking", _movement.magnitude > 0.1f);

        // Normalizar inputs.
        _movement.Normalize();
    }


    private void UpdateDirection()
    {
        Vector3 d = Vector3.RotateTowards(transform.forward, _movement, _turnSpeed * Time.fixedDeltaTime, 0.0f);
        _rb.MoveRotation(Quaternion.LookRotation(d));
    }


    private void SfxFootsteps()
    {
        if (_movement.magnitude > 0.1f)
        {
            if (!_sfxSource.isPlaying)
                _sfxSource.Play();
        }
        else
            _sfxSource.Stop();
    }
}
