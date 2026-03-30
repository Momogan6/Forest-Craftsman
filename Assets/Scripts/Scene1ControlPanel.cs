using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class Scene1ControlPanel : MonoBehaviour
{
    public Transform player;

    float scale = 1;
    float angleY = 0;
    float positionX = 0;
    float positionZ = 0;
    float markedPositionX;
    float markedPositionZ;
    float markedAngleY;
    List<Button> buttonList;

    void Start()
    {
        scale = 1.0f;
        angleY = 0;
        positionX = 0;
        positionZ = 0;
        markedPositionZ = player.localPosition.z;
        markedPositionX = player.localPosition.x;
        markedAngleY = player.localEulerAngles.y;

        buttonList = ToolUtils.FetchItemList<Button>(transform, "row0/", "btn", 3);
        buttonList.AddRange(ToolUtils.FetchItemList<Button>(transform, "row1/", "btn", 4));

        foreach (var item in buttonList)
        {
            item.onClick.AddListener(() =>
            {
                OnControlButtonClick(item);
            });
        }
        ToolUtils.AddButtonAction(transform, "btnReset", OnResetButtonClick);
        ToolUtils.AddButtonAction(transform, "btnMenu", OnMenuButtonClick);
    }

    void OnDisable()
    {
        ResetPlayerAttributes();
    }

    void OnControlButtonClick(Button sender)
    {
        int index = buttonList.IndexOf(sender);
        if (index == -1)
        {
            return;
        }

        switch (index)
        {
            case 0: // +
                OnScaleUpdated(true);
                break;
            case 1: // Rotate
                OnRotateButtonClick();
                break;
            case 2: // -
                OnScaleUpdated(false);
                break;

            case 3: // <
                OnPositionXUpdated(true);
                break;
            case 6: // >
                OnPositionXUpdated(false);
                break;

            case 4: // Forward
                OnPositionZUpdated(true);
                break;
            case 5: // Backward
                OnPositionZUpdated(false);
                break;
            default:
                break;
        }
    }

    void OnMenuButtonClick()
    {
        ToolUtils.FetchPanel(transform.parent, "menuPanel").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void OnRotateButtonClick()
    {
        angleY += 15;
        var temp = player.localEulerAngles;
        temp.y = markedAngleY + angleY;
        player.localEulerAngles = temp;
    }

    void OnResetButtonClick()
    {
        ResetPlayerAttributes();
    }

    void ResetPlayerAttributes()
    {
        if (player == null)
        {
            return;
        }
        scale = 1;
        angleY = 0;
        positionX = 0;
        positionZ = 0;
        var position = player.localPosition;
        var angles = player.localEulerAngles;
        position.z = markedPositionZ;
        position.x = markedPositionX;
        angles.y = markedAngleY;

        player.localPosition = position;
        player.localEulerAngles = angles;
        player.localScale = Vector3.one;
    }

    void OnPositionXUpdated(bool increaseFlag)
    {
        if ((increaseFlag && positionX >= 1.2)
            || (!increaseFlag && positionX <= -1.2))
        {
            return;
        }

        positionX += (increaseFlag ? 0.3f : -0.3f);
        var temp = player.localPosition;
        temp.x = markedPositionX + positionX;
        player.localPosition = temp;
    }

    void OnPositionZUpdated(bool increaseFlag)
    {
        if ((increaseFlag && positionZ >= 1.0)
            || (!increaseFlag && positionZ <= -1.0))
        {
            return;
        }

        positionZ += (increaseFlag ? 0.1f : -0.1f);
        var temp = player.localPosition;
        temp.z = markedPositionZ + positionZ;
        player.localPosition = temp;
    }

    void OnScaleUpdated(bool increaseFlag)
    {
        if ((increaseFlag && scale >= 2)
            || (!increaseFlag && scale <= 0.5))
        {
            return;
        }
        scale += (increaseFlag ? 0.1f : -0.1f);
        player.localScale = Vector3.one * scale;
    }
}
