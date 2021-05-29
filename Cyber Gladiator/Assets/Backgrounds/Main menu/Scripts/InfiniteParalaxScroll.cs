using UnityEngine;

public class InfiniteParalaxScroll : MonoBehaviour
{
    public float Speed = 1;
    [SerializeField] public Layer[] Layers;

    [System.Serializable]
    public class Layer
    {
        public Transform Transform;
        [Range(0, 1)] public float Weight;
        public float Width;
    }

    void Update()
    {
        foreach (Layer layer in Layers)
            layer.Transform.localPosition = new Vector3(Time.time * Speed * layer.Weight % layer.Width - layer.Width / (Speed > 0 ? 2 : -2), layer.Transform.localPosition.y, layer.Transform.localPosition.z);
    }
}