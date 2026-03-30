using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToolUtils
{
    public static Graphic FetchPanel(Transform transform, string path)
    {
        return FetchItem<Graphic>(transform, path);
    }

    public static void AddButtonAction(Transform transform, string path, UnityAction call)
    {
        transform.Find(path).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(call);
    }

    public static T FetchItem<T>(Transform transform, string path)
    {
        return transform.Find(path).GetComponent<T>();
    }

    public static List<T> FetchItemList<T>(Transform transform, string path, string prefix, int count)
    {
        List<T> list = new List<T>();
        if (count <= 0)
        {
            return list;
        }

        for (int i = 0; i < count; i++)
        {
            string localPath = path;
            if (path != null && path.Length > 0)
            {
                if ((path + "/").Contains("//") == false)
                {
                    localPath = path + "/";
                }
            }
            string fullPath = localPath + prefix + i.ToString();
            T item = transform.Find(fullPath).GetComponent<T>();
            if (item != null)
            {
                list.Add(item);
            }
        }
        return list;
    }
}