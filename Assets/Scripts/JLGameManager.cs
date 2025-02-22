using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class JLGameManager : MonoBehaviour
{
    [SerializeField]
    private float _winFadeSpeed = 2.0f,
                    _winTime = 6.0f,
                    _loseFadeSpeed = 5.0f,
                    _loseTime = 4.0f;

    [SerializeField]
    private Image _panel,
                    _winImage,
                    _loseImage;

    [SerializeField]
    private AudioClip _winClip,
                        _loseClip;


    private int _state = 0; // 0 = Jugando, 1 = Perder, 2 = Ganar.

    // El tiempo que será usado para saber cuando se llama Reload() después de haber ganado/perdido.
    private float _time = 0.0f;


    private AudioSource _audioSource;



    void Start()
    {
        // Se esconden las imagenes y panel.
        _winImage.enabled = false;
        _loseImage.enabled = false;
        _panel.color = new Color(0, 0, 0, 0);

        _audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (_state > 0)
        {
            // Se aumenta el temporizador en preparación para llamar a Reload()
            _time += Time.deltaTime;

            // Se puede modificar los componentes del Color de forma independiente si se crea una variable local a partir de el.
            Color c = _panel.color;
            c.a = Mathf.MoveTowards(c.a, 1.0f, Time.deltaTime * 4);
            _panel.color = c;

            if (_state == 1) // Se revela el icono de victoria.
            {
                c = _winImage.color;
                c.a = Mathf.MoveTowards(c.a, 1.0f, Time.deltaTime * 4);
                _winImage.color = c;

                if (_time >= _winTime)
                    Reload();
            }
            else if (_state == 2) // Se revela el icono de derrota.
            {
                c = _loseImage.color;
                c.a = Mathf.MoveTowards(c.a, 1.0f, Time.deltaTime * 4);
                _loseImage.color = c;

                if (_time >= _loseTime)
                    Reload();
            }
        }
    }



    // Si se llama, se activa el estado de derrota.
    public void Lose()
    {
        if (_state == 0)
        {
            _state = 1;
            _time = 0.0f;

            // Se hace que el panel sea negro transparente.
            _panel.color = new Color(0, 0, 0, 0);
            _loseImage.enabled = true; // Y se activa la imagen correspondiente.

            // Finalmente, se reproduce el sonido.
            _audioSource.clip = _loseClip;
            _audioSource.PlayDelayed(1 / _loseFadeSpeed);
        }
    }


    // Si se llama, se activa el estado de victoria.
    public void Win()
    {
        if (_state == 0)
        {
            _state = 2;
            _time = 0.0f;

            // Se hace que el panel sea blanco transparente.
            _panel.color = new Color(1, 1, 1, 0);
            _winImage.enabled = true; // Y se activa la imagen correspondiente.

            // Finalmente, se reproduce el sonido.
            _audioSource.clip = _winClip;
            _audioSource.PlayDelayed(1 / _winFadeSpeed);
        }
    }


    // Si se llama, se reinicia la escena.
    public void Reload()
    {
        _state = 0;

        // Se reinicia el mapa, usando el nombre de la escena actualmente cargada.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
