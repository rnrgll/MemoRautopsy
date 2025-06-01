using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public static class Util
    {
        public static void SetCanvasGroupState(CanvasGroup cg, float alpha, bool interactable, bool blocksRaycast)
        {
            if (cg == null) return;

            cg.alpha = alpha;
            cg.interactable = interactable;
            cg.blocksRaycasts = blocksRaycast;
        }

        public static void UIEnable(CanvasGroup cg)
        {
            SetCanvasGroupState(cg, 1f, true, true);
        }

        public static void UIDisable(CanvasGroup cg)
        {
            SetCanvasGroupState(cg, 0f, false, false);
        }

        public static void SetSceneObjectsActive(Define.Scene curScene, bool isActive)
        {
            Scene scene = SceneManager.GetSceneByName(Define.SceneNames[curScene]);
            if (!scene.IsValid())
            {
                Debug.LogWarning($"[SetSceneObjectsActive] Invalid scene: {curScene}");
                return;
            }

            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject obj in rootObjects)
            {
                obj.SetActive(isActive);
            }
        }
    }
}