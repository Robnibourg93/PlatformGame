using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Button startButton,exitButton;

	// Use this for initialization
	void Start () {
        startButton = startButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
    }

    // Update is called once per frame
    public void ExitPress() {
        Application.Quit();
    }

    public void StartPress() {
        SceneManager.LoadScene("Level 1");
    }

}
