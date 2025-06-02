using System.Collections.Generic;

namespace Utility
{
    public class Define
    {
        /// <summary>
        /// 열거형 Scene에 해당하는 씬 이름 문자열을 매핑한 딕셔너리
        /// Scene 열거형을 통해 문자열 이름을 가져올 수 있다.
        /// </summary>
        public static readonly Dictionary<Scene, string> SceneNames = new()
        {
            { Scene.Title, "TitleScene" },
            { Scene.Lab , "LabScene"},
            { Scene.Inspect , "InspectScene"},
            { Scene.LabMap , "LabMap"},
            { Scene.BadEnding , "BadEndingScene"},
            { Scene.Ending , "EndingScene"},
            
        };
        
        /// <summary>
        /// 게임 내에서 사용되는 씬을 구분하기 위한 열거형
        /// </summary>
        public enum Scene
        {
            Title,
            Lab,
            Inspect,
            LabMap,
            BadEnding,
            Ending,
            
        }
        
        public enum SceneType
        {
            Unknown,
            Title,
            Main,
            Inspect,
            Memory,
            UI
        }
        
        
        public enum ClueId
        {
            day1_arm_injection,
            day1_finger_scratch,
            day1_head_injury,
            day2_hand_paper,
            day2_mouth_injury,
            day2_neck_injection,
            day3_stomach_injection,
            day3_mouth_chip,
            day3_arm_burn,
            
        }

    }
}