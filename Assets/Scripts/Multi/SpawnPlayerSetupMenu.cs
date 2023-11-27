using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class SpawnPlayerSetupMenu : MonoBehaviour
{
    public GameObject PlayerSetupMenuPrefab;
    public PlayerInput input;

    private void Awake()
    {
        var rootMenu = GameObject.Find("MainLayout");
        if (rootMenu != null)
        {
            var menu = Instantiate(PlayerSetupMenuPrefab, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            
            menu.GetComponent<SelectCharacter>().SetPlayerIndex(input.playerIndex);
        }
    }
}
