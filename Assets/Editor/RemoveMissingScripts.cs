using UnityEngine;
using UnityEditor;

public class RemoveMissingScripts
{
    [MenuItem("Tools/Remove All Missing Scripts in Scene")]
    static void RemoveAllMissingScripts()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (GameObject go in allObjects)
        {
            int removed = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
            if (removed > 0)
            {
                Debug.LogWarning($"Removed {removed} missing scripts from: {go.name}", go);
                count += removed;
            }
        }

        Debug.Log($"총 {count}개의 Missing Script를 삭제했습니다.");
    }
}