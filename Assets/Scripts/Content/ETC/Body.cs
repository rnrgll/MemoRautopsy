using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Body : MonoBehaviour
{
    public float freezeTime = 0.3f;
    public string animationName = "Idle"; // 실행할 애니메이션 이름
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(animationName, 0, 0f);
        StartCoroutine(FreezeAndBake(freezeTime));
    }

    private System.Collections.IEnumerator FreezeAndBake(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.enabled = false; // 현재 포즈에서 정지

        // foreach (var smr in GetComponentsInChildren<SkinnedMeshRenderer>())
        // {
        //     Mesh bakedMesh = new Mesh();
        //     smr.BakeMesh(bakedMesh);
        //
        //     GameObject bakedObj = new GameObject(smr.name + "_Baked");
        //     bakedObj.transform.position = smr.transform.position;
        //     bakedObj.transform.rotation = smr.transform.rotation;
        //
        //     var mf = bakedObj.AddComponent<MeshFilter>();
        //     mf.sharedMesh = bakedMesh;
        //
        //     var mr = bakedObj.AddComponent<MeshRenderer>();
        //     mr.sharedMaterials = smr.sharedMaterials;
        // }
        //
        // Debug.Log("✅ 포즈 고정 및 베이크 완료!");
    }
    
#if UNITY_EDITOR

        [ContextMenu("📦 Bake Pose and Save Mesh")]
        private void BakeAllMeshesIntoOneObject()
        {
            var smRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            if (smRenderers.Length == 0)
            {
                Debug.LogWarning("SkinnedMeshRenderer가 없습니다.");
                return;
            }

            // 부모 오브젝트 생성
            GameObject bakedGroup = new GameObject($"{gameObject.name}_BakedGroup");
            bakedGroup.transform.position = transform.position;
            bakedGroup.transform.rotation = transform.rotation;

            foreach (var smr in smRenderers)
            {
                // 1. Mesh Bake
                Mesh bakedMesh = new Mesh();
                smr.BakeMesh(bakedMesh);

                // 2. Asset 저장 (Editor only)
                string assetPath = $"Assets/{smr.name}_BakedMesh.asset";
                AssetDatabase.CreateAsset(bakedMesh, assetPath);
                AssetDatabase.SaveAssets();

                // 3. GameObject 생성
                GameObject meshObject = new GameObject(smr.name + "_Baked");
                meshObject.transform.SetParent(bakedGroup.transform);
                meshObject.transform.position = smr.transform.position;
                meshObject.transform.rotation = smr.transform.rotation;
                meshObject.transform.localScale = smr.transform.lossyScale;

                // 4. MeshRenderer + MeshFilter 추가
                var mf = meshObject.AddComponent<MeshFilter>();
                mf.sharedMesh = AssetDatabase.LoadAssetAtPath<Mesh>(assetPath);

                var mr = meshObject.AddComponent<MeshRenderer>();
                mr.sharedMaterials = smr.sharedMaterials;
            }

            Debug.Log("✅ 포즈 Bake 및 그룹 완료: " + bakedGroup.name);
            Selection.activeGameObject = bakedGroup;
        }
    
#endif
}
