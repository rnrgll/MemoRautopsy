using Managers;
using UnityEngine;

namespace Managers
{
    public static class Manager
    {
        private static GameObject _instance;
        
        //Manager 등록
        public static SceneManagerEx Scene => SceneManagerEx.Instance;
        public static UIManager UI => UIManager.Instance;
        public static DataManager Data => DataManager.Instance;
        public static EventSystemManager Event => EventSystemManager.Instance;
        
        
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            //매니저 생성 및 초기화 진행
            if (_instance != null) return;

            var prefab = Resources.Load<GameObject>("Prefabs/@Manager");
            _instance = GameObject.Instantiate(prefab);
            _instance.gameObject.name = "@Manager";
            GameObject.DontDestroyOnLoad(_instance);
            
            // SceneManagerEx.SingletonInit();
            // UIManager.SingletonInit();
            
        }
    }
}