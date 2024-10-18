using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
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
<<<<<<< HEAD
    void Start()
    {
       //ettingMenu.SetActive(false);
        SettingMenu.SetActive(true);
    }
=======
   
>>>>>>> 35729521658b6be864c06e76e9080c2eff22ff56

    public void OnButtonClick()
    {

        print(buttonType);
        switch (buttonType)
        {


            case ButtonType.StartGame:
                SceneManager.LoadScene("SampleScene");
                break;
            case ButtonType.Options:
                print("asfa");
                SettingMenu.SetActive(true);
                // volumeSlider.onValueChanged.AddListener(SetVolume);
                break;
            case ButtonType.Quit:
                Application.Quit();
                break;
            case ButtonType.SettingQuit:
                print("asdf");
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
