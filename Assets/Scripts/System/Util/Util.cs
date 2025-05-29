using UnityEngine;

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
    }
}