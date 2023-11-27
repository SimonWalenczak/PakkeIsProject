using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class BackToPreviousMenu : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                BackToMenu();
            }
        }

        public void BackToMenu()
        {
            if (FindObjectOfType<PlayerConfigurationManager>())
                Destroy(PlayerConfigurationManager.Instance.gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }
}