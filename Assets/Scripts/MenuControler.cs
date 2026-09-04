using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    
     public void Jugar()
{
    GameModeManager.multiplayer = false;
    SceneManager.LoadScene("SampleScene");
}
    public void Salir()
    {
        Application.Quit();
    }
    public void Volver()
{
    SceneManager.LoadScene("MenuInicial");
}
    public void AbrirMultiplayer()
{
    GameModeManager.multiplayer = true;
    SceneManager.LoadScene("Multiplayer");
}
public void JugarMultiplayer()
{
    GameModeManager.multiplayer = true;
    UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
}
  public void AbrirOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }
}
