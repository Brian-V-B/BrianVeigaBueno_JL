using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Se busca y se pilla el GameManager y se llama el metodo para perder.
            FindFirstObjectByType<JLGameManager>().Lose();
        }
    }
}
