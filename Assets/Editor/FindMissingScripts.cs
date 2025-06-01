using UnityEngine;
using UnityEditor;

public class FindMissingScripts : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    public static void FindMissing()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"Missing script in: {GetHierarchyPath(go)}", go);
                    count++;
                }
            }
        }

        Debug.Log($"총 {count}개의 Missing Script가 발견됨.");
    }

    static string GetHierarchyPath(GameObject obj)
    {
        return obj.transform.parent == null ? obj.name : GetHierarchyPath(obj.transform.parent.gameObject) + "/" + obj.name;
    }
}