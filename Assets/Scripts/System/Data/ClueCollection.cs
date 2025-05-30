using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

namespace System.Data
{
    [System.Serializable]
    public class ClueCollection
    {
        private Dictionary<int, List<Define.ClueId>> _cluesByDay;
        public IReadOnlyDictionary<int, List<Define.ClueId>> CluesByDay => _cluesByDay;
        
        
        //초기화
        public void Init()
        {
            _cluesByDay = new();
        }
        
        
        /// <summary>
        /// 단서 수집 내역에 수집한 단서 id(clueId)를 저장한다.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="clueId"></param>
        public void CollectClue(int day, Define.ClueId clueId)
        {
            if (!_cluesByDay.ContainsKey(day))
                _cluesByDay[day] = new List<Define.ClueId>();
            
            //중복 체크
            if(!_cluesByDay[day].Contains(clueId))
                _cluesByDay[day].Add(clueId);
        }

        /// <summary>
        /// 해당 날짜의 단서 수집 내역을 반환한다.
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public IEnumerable<Define.ClueId> GetCluesByDay(int day)
        {
            return _cluesByDay.TryGetValue(day, out List<Define.ClueId> list) ? list : null;
        }

        public bool HasClue(Define.ClueId clueId)
        {
            return _cluesByDay.Values.Any(list => list.Contains(clueId));
        }
        
    }
}