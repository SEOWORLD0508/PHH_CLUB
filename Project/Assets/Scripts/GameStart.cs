using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Btntype : MonoBehaviour
{
    public enum ButtonType
    {
        StartGame,
        Options,
        Quit,
        SettingQuit
    }

    public ButtonType buttonType;

    public GameObject SettingMenu;

    public AudioMixer audioMixer;  // Audio
    public Slider volumeSlider;    // UI
    void Start()
    {
        SettingMenu.SetActive(false);
    }

    public void OnButtonClick()
    {
        switch (buttonType)
        {
            case ButtonType.StartGame:
                SceneManager.LoadScene("SampleScene");
                break;
            case ButtonType.Options:
                SettingMenu.SetActive(true);
                volumeSlider.onValueChanged.AddListener(SetVolume);
                break;
            case ButtonType.Quit:
                Application.Quit();
                break;
            case ButtonType.SettingQuit:
                SettingMenu.SetActive(false);
                break;
        }
    }

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
        Debug.Log(sliderValue);
    }
}
