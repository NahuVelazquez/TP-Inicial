using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text collectiblesNumbersText;

    private int collectiblesNumber;

    void Start()
    {       
    }

    void Update()
    {
    }

    public void addCollectible()
    {
        collectiblesNumber++;

        collectiblesNumbersText.text = collectiblesNumber.ToString();
    }
}
