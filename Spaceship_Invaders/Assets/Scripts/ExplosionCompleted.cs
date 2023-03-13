using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCompleted : MonoBehaviour
{
    public GameObject Explosion;

    public void ExplosionEnd(int i)
    {
        Debug.Log("ExplosionCompleted");
        Destroy(Explosion);
    }
}
