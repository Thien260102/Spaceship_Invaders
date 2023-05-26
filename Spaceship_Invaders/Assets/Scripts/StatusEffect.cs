using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public enum StatusEffectTypes
{
    Burn,
    Stun,
}

public static class StatusEffectManager
{
    public static void InflictStatusEffect(Entity e, StatusEffectTypes type, float duration, int damage = 1000)
    {
        switch (type)
        {
            case StatusEffectTypes.Burn:
            {
                Burn b = new Burn(duration, e, damage);
                e.statusEffects.Add(b);
                b.OnInflicted();
                return;
            }
            default:
            {
                return;
            }
        }
    }
}

[System.Serializable]
public abstract class StatusEffect
{
    public Entity entity;
    public float duration, remainingDuration;

    public StatusEffect(float t, Entity e)
    {
        duration = t;
        remainingDuration = t;
        entity = e;
    }

    public virtual void OnInflicted()
    {
        //dostuff
    }

    public virtual void OnUpdate()
    {
        //dostuff
    }

    public virtual void OnConditionalUpdate()
    {
        if (Condition())
        {
            //dostuff
        }
    }

    public virtual void OnEnd()
    {
        //dostuff
    }

    public virtual bool Condition()
    {
        return true;
    }
}
