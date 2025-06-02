using System.Collections;
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
            SceneType = Define.SceneType.Inspect;
            //todo: day를 플레이어 진행상황에 맞게 가져오기
            _day = Manager.Data.GameDay;
            base.Init();
        }

        public override void OnEnterScene()
        {
            LoadBodyDummy();
        }
        
        

        private void LoadBodyDummy()
        {
            Debug.Log(_day);
            //프리팹 로드
            GameObject dummyPrefaab = Resources.Load<GameObject>($"Prefabs/BodyDummy{_day}");
            
            //인스턴스 생성 + 계층 구조 설정 및 위치 지정
            GameObject dummyInstance = Instantiate(dummyPrefaab, _autopsyTable);
            dummyInstance.transform.SetAsLastSibling();
        }
        
        

        public override void OnExitScene()
        {
            
        }

        // private IEnumerator ShowAutopsyHint()
        // {
        //     yield return new WaitForSeconds(1f);
        //
        //     Manager.UI.ShowDialouge(new List<string>() { "시신의 상태를 정밀히 살펴보자.", "이상 징후가 보이는 부위를  클릭해 단서를 조사할 수 있다." });
        //     
        // }
        
    }
}