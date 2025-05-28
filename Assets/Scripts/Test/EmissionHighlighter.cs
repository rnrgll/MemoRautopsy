using UnityEngine;

public class EmissionHighlighter : MonoBehaviour
{
    private Renderer _renderer;
    private Material _material;

    [ColorUsage(true, true)]
    public Color highlightColor = Color.yellow;

    public float intensity = 1.5f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material; // 인스턴싱됨
    }

    public void HighlightOn()
    {
        _material.EnableKeyword("_EMISSION");
        _material.SetColor("_EmissionColor", highlightColor * intensity);
        DynamicGI.SetEmissive(_renderer, highlightColor);
    }

    public void HighlightOff()
    {
        _material.DisableKeyword("_EMISSION");
        _material.SetColor("_EmissionColor", Color.black);
        DynamicGI.SetEmissive(_renderer, Color.black);
    }
}