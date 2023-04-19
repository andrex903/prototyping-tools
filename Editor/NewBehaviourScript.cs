#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Redeev.PrototypingTools
{
    public static class NewBehaviourScript
    {
        [MenuItem("GameObject/Create Player Prototype")]
        public static void CreatePlayerPrototype()
        {
            GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Packages/com.redeev.prototyping-tools/Prefabs/PlayerAndCamera.prefab", typeof(GameObject));
            if (prefab != null)
            {
                PrefabUtility.InstantiatePrefab(prefab);
            }
        }
    }
}
#endif