using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Event
{
    [CreateAssetMenu(fileName = "SceneChangeStep", menuName = "Data/Event/SceneChangeStep")]
    public class SceneChangeStep : BaseEventStep
    {
        [SerializeField] private Define.Scene scene;
        [SerializeField] private bool isAsyncLoad;
        
        [Header("Async load setting")]
        [SerializeField] private bool effect;
        [SerializeField] private float delay;
        [SerializeField] private LoadSceneMode loadSceneMode;
        
        public override void Run(EventSequenceRunner runner)
        {
            if (isAsyncLoad)
            {
                Manager.Scene.AsncLoadScene(scene,delay,effect,(int)loadSceneMode);
            }
            else
            {
                Manager.Scene.LoadScene(scene);
            }
        }
    }
}