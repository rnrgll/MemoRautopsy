using UnityEngine;

namespace DesignPattern
{
    public static class Manager
    {
        
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            //매니저 생성 및 초기화 진행
        }
    }
}