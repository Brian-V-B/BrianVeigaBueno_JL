using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JLWinTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Se busca y se pilla el GameManager y se llama el metodo para ganar.
            FindFirstObjectByType<JLGameManager>().Win();
        }
    }
}
