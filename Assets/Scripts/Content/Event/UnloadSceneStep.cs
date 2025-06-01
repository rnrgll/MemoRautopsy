using UnityEngine;
using UnityEngine.SceneManagement;

namespace Event
{
    [CreateAssetMenu(fileName = "UnloadSceneStep", menuName = "Data/Event/UnloadSceneStep")]

    public class UnloadSceneStep : BaseEventStep
    {
        [SerializeField] private string sceneName;

        public override void Run(EventSequenceRunner runner)
        {
            SceneManager.UnloadSceneAsync(sceneName);
            runner.NextStep();
        }
    }
}