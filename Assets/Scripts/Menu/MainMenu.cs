using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject TransitionObject;

    public void ValidPreset(int index)
    {
        GameData.NumberOfPlayer = index;
        Debug.Log("Number of players : " + index);
        StartCoroutine(Transition());
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Quit()
    {
        //If we are running in a standalone build of the game
#if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
#endif

        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    IEnumerator Transition()
    {
        TransitionObject.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("PlayerSetup");
    }
}