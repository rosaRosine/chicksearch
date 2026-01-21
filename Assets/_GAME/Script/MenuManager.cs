using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class MenuManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1f; //Resume the game
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMasterVolume(float volume)
    {
        masterMixer.SetFloat("VolumeMaster", volume);
    }
}
