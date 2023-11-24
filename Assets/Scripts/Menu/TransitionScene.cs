using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    [SerializeField] private GameObject TransitionObject;

    public void OnClick(string SceneName)
    {
        StartCoroutine(Transition(SceneName));
    }
    
    IEnumerator Transition(string SceneName)
    {
        TransitionObject.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneName);
    }
}
