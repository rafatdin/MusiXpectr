using UnityEngine;
using System.Collections;

public class Coin {
    public GameObject original;
    public Material material;
    public Vector3 scale;
    public Vector3 position;
    public int spectrumNumber;

    public Coin()
    {
        scale = new Vector3(0.8f, 0.8f, 0.5f);
    }
    
    public Coin(GameObject Original,Vector3 Position, Material Material)
    {
        original = Original;
        material = Material;
        position = Position;
    }


    public Coin(Material Material, Vector3 Scale, Vector3 Position)
    {
        material = Material;
        scale = Scale;
        position = Position;
    }
    /// <summary>
    /// I want to get first several numbers from spectrum class to change the scale of
    /// coin depending on its spectrum number generated randomly between min and max
    /// </summary>
    /// <param name="Scale"></param>
    /// <param name="Position"></param>
    /// <param name="Min"></param>
    /// <param name="Max"></param>
    public Coin(Vector3 Scale, Vector3 Position, int min, int max)
    {
        scale = Scale;
        position = Position;
        spectrumNumber = Random.Range(min, max);
    }

    Vector3 GetScale()
    {
        return scale;
    }

    Vector3 GetPosition()
    {
        return position;
    }

    void SetScale(Vector3 Scale)
    {
        scale = Scale;
    }

    void SetPosition(Vector3 Position)
    {
        position = Position;
    }

    void SetMaterial(Material Material)
    {
        material = Material;
    }

}
