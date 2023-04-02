using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCompleted : MonoBehaviour
{
    public void ExplosionEnd(int i)
    {
        Debug.Log("ExplosionCompleted");
        Destroy(gameObject);
    }
}
