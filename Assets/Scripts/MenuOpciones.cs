using UnityEngine;
using UnityEngine.UI;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private Slider sliderSensibilidad;

    private void Start()
    {
        if (sliderSensibilidad != null)
        {
            //asgina el valor de gamesettings
            sliderSensibilidad.value = GameSettings.MouseSensitivity;

            // actualiza sensibilidad de acuerdo al slider
            sliderSensibilidad.onValueChanged.AddListener((nuevoValor) =>
            {
                GameSettings.MouseSensitivity = nuevoValor;
            });
        }
    }
}