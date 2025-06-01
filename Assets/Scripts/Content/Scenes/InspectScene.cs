using System.Collections.Generic;
using Event;
using Managers;
using UnityEngine;
using Utility;

namespace Scenes
{
    public class InspectScene : BaseScene
    {
        [SerializeField] private int _day;
        [SerializeField] private Transform _autopsyTable;
        
        protected override void Init()
        {
            base.Init();
            SceneType = Define.SceneType.Inspect;
            //todo: day를 플레이어 진행상황에 맞게 가져오기
            _day = Manager.Data.GameDay;
        }

        public override void OnEnterScene()
        {
            
            LoadBodyDummy();
        }

        private void LoadBodyDummy()
        {
            //프리팹 로드
            GameObject dummyPrefaab = Resources.Load<GameObject>($"Prefabs/BodyDummy{_day}");
            
            //인스턴스 생성 + 계층 구조 설정 및 위치 지정
            GameObject dummyInstance = Instantiate(dummyPrefaab, _autopsyTable);
            dummyInstance.transform.SetAsLastSibling();
        }
        
        

        public override void OnExitScene()
        {
            
        }
    }
}