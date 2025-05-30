using System.Collections.Generic;
using Content.Interactable;
using UnityEngine;
using Utility;

namespace System.Data
{
    public class ClueDatabase
    {
        //딕셔너리로 데이터 관리
        private Dictionary<Define.ClueId, ClueData> _clueDB;
        public IReadOnlyDictionary<Define.ClueId, ClueData> ClueDB => _clueDB;
        public void Init()
        {
            //아이템 불러오기
            _clueDB = new();

            var datas = UnityEngine.Resources.LoadAll<ClueData>("Data");

            foreach (var data in datas)
            {
                Define.ClueId key = data.clueId;

                if (!_clueDB.ContainsKey(key))
                {
                    _clueDB.Add(key, data);
                    
                }
                else
                {
                    Debug.Log($"중복된 아이템 : {key}");
                }
            }
            
            Debug.Log($"Clue DB - 사건 데이터 로드 완료. 총 개수 {_clueDB.Count}");
        }

        public ClueData GetClueData(Define.ClueId clueId)
        {
            if (_clueDB.TryGetValue(clueId, out ClueData data))
            {
                return data;
            }
            Debug.Log($"{clueId.ToString()}를 찾을 수 없습니다.");
            return null;
        }
    }
}