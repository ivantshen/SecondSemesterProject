using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuitMenu : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private CanvasKeybinds keybinds;
    // Start is called before the first frame update
    void Start()
    {
        yesButton.onClick.AddListener(quitGame);
        noButton.onClick.AddListener(closeMenu);
    }

    void quitGame(){
        Application.Quit();
    }
    void closeMenu(){
        keybinds.setHotkeyMenuState(false);
    }
}
