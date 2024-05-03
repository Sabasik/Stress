using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public static Info info;
    public TextMeshProUGUI lastGameText;
    public Slider slider;

    private void Awake() {
        info = new Info();
        sliderValue();
    }

    private void Start() {
        lastGameText.text = info.getText();
    }

    public void sliderValue() {
        info.setSpeed(slider.value);
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
