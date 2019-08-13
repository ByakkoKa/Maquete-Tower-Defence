using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IHittable
{
    void Hit();
}

public interface IDamageable
{
    void TakeHit(int damage); 
}

public interface IWaitTime
{
    IEnumerator WaitTime(string waitTime);
}

