using UnityEngine;

public static class LayerMaskExtensions
{
    public static bool Contains(this LayerMask mask, int layer)
    {
        return (mask.value & (1 << layer)) > 0;
    }

    public static T AddComponentInParent<T>(this GameObject go) where T : Component
    {
        Transform parent = go.transform.parent;
        
        if (parent != null)
        {
            GameObject goParent = parent.gameObject;
            return goParent.AddComponent<T>();
        }

        return go.AddComponent<T>();
    }
}