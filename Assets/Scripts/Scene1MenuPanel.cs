using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene1MenuPanel : MonoBehaviour
{
    public Transform player;

    List<Button> menuButtons;

    void Start()
    {
        PlayBgMusic();
        menuButtons = ToolUtils.FetchItemList<Button>(transform, "", "btn", 4);
        foreach (var button in menuButtons)
        {
            button.onClick.AddListener(() => {
                OnButtonClick(button);
            });
        }
    }

    void OnEnable()
    {
        OnPageChanged(true);
    }

    void OnPageChanged(bool forMenuPage)
    {   
        Vector3 position = Camera.main.transform.localPosition;
        position.z += forMenuPage ? 2.0f : -2.0f; ;
        Camera.main.transform.localPosition = position;
        player.gameObject.SetActive(!forMenuPage);
    }

    void PlayBgMusic()
    {
        string name = SceneManager.GetActiveScene().name;
        string suffix = name.Substring(name.Length - 1, 1);
    }

    void OnButtonClick(Button sender)
    {
        int index = menuButtons.IndexOf(sender);
        if (index == -1)
        {
            return;
        }
        switch (index)
        {
            case 0:
                OnStartButtonClick();
                break;
            default:
                break;
        }
    }

    void OnStartButtonClick()
    {
        ToolUtils.FetchPanel(transform.parent, "controlPanel").gameObject.SetActive(true);
        gameObject.SetActive(false);
        OnPageChanged(false);
    }
}
