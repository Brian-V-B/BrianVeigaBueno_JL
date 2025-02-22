using UnityEngine;
using UnityEngine.SceneManagement;

public class JLReload : MonoBehaviour
{
    // Si se llama, se reinicia la escena.
    public void Reload()
    {
        // Se reinicia el mapa, usando el nombre de la escena actualmente cargada.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
