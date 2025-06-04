using UnityEngine;
using UnityEditor;

public class MissingScriptScanner
{
    [MenuItem("Tools/Scan All Objects For Missing Scripts")]
    public static void Scan()
    {
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(
            FindObjectsSortMode.None
        );

        int missingCount = 0;

        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.Log($"Missing script in: {GetHierarchyPath(go)}", go);
                    missingCount++;
                }
            }
        }

        Debug.Log($"Total missing scripts found: {missingCount}");
    }

    static string GetHierarchyPath(GameObject obj)
    {
        string path = obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = obj.name + "/" + path;
        }
        return path;
    }
}
