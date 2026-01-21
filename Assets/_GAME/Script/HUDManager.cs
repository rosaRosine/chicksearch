using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI chickText;
    public GameObject winPanel;

    public EventSystem eventSystem;
    public GameObject firstSelectedButton;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(winPanel) winPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void UpdateChickCount(int current, int max)
    {
        chickText.text = current + "/" + max;
    }

    public void ShowWinPanel()
    {
        if(winPanel) winPanel.SetActive(true);
        if(audioSource) audioSource.Play();

        Time.timeScale = 0f; //Pause the game

        eventSystem.SetSelectedGameObject(firstSelectedButton);
    }
}
