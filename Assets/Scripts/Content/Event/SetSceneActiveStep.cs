using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Event
{
    [CreateAssetMenu(fileName = "SetSceneActiveStep", menuName = "Data/Event/SetSceneActiveStep")]
    public class SetSceneActiveStep : BaseEventStep
    {
        [SerializeField] private Define.Scene sceneName;
        [SerializeField] private bool isActive;

        public override void Run(EventSequenceRunner runner)
        {
            Util.SetSceneObjectsActive(sceneName, isActive);

            if (isActive)
            {
                Scene loadedScene = SceneManager.GetSceneByName(Define.SceneNames[sceneName]);
                
                if (loadedScene.IsValid())
                {
                    SceneManager.SetActiveScene(loadedScene);
                }
            }
            
            
            runner.NextStep();
        }
    }
}