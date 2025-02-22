using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    private Transform[] _patrol;

    [SerializeField]
    private float _speed = 0.7f;

    private int _index = 0;



    private void FixedUpdate()
    {
        if (_patrol.Length > 0)
            PatrolPath();
        Scan();
    }



    private void PatrolPath()
    {
        // Reiniciar el indice si se termina el camino.
        if (_index >= _patrol.Length || _index < 0)
            _index = 0;

        Vector3 tPosition = _patrol[_index].position; // Se guarda para referenciado rápido.

        // Se mira cara donde se mueve.
        Vector3 d = Vector3.RotateTowards(transform.forward, (tPosition - transform.position).normalized, _speed * 2.4f * Time.fixedDeltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(d);

        // Se mueve.
        transform.position = Vector3.MoveTowards(transform.position, tPosition, _speed * Time.fixedDeltaTime);

        // Actualizar el indice si se está cerca.
        if (Vector3.Distance(transform.position, tPosition) < 0.2f)
            _index++;
    }


    private void Scan()
    {
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 4f) && hit.collider.CompareTag("Player"))
        {
            // Se busca y se pilla el GameManager y se llama el metodo para perder.
            FindFirstObjectByType<JLGameManager>().Lose();
        }
    }
}
