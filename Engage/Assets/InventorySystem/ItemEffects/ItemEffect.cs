// The ItemEffect abstract class defines the base which all effects should implement

using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
    public abstract void Apply(GameObject user);
}
