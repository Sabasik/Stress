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

    private Dictionary<float, float> values = new Dictionary<float, float>();

    private void Awake() {
        values.Add(0, 0.5f);
        values.Add(1, 1f);
        values.Add(2, 1.5f);
        values.Add(3, 2f);
        info = new Info();
        sliderValue();
    }

    private void Start() {
        lastGameText.text = info.getText();
    }

    public void sliderValue() {
        info.setSpeed(values[slider.value]);
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
