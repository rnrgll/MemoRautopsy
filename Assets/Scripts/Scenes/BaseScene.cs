using UnityEngine;
using Util;

namespace Scenes
{
    public abstract class BaseScene : MonoBehaviour
    {
        public Define.SceneType SceneType { get; protected set; } = Define.SceneType.Unknown;

        private void Awake() => Init();

        protected virtual void Init()
        {
            
        }

        public abstract void Clear();
    }
}