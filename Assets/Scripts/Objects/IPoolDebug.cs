using UnityEngine;

namespace Objects
{
    public interface IPoolDebugInfo
    {
        string PoolName { get; }
        GameObject Prefab { get; }
        int TotalCount { get; }
        int ActiveCount { get; }
        
#if UNITY_EDITOR
        void EditorSpawnOne();
        void EditorReturnAll();
        void EditorClear();
        Object GetPrefab();
#endif
    }
}