using System;
using UnityEngine;

[Serializable]
public class BallHolder
{
    public BallShooting BallShooting;
    public Transform BallTranform;
    public BallHolder(BallShooting ballShooting, Transform ballTranform)
    {
        this.BallShooting = ballShooting;
        this.BallTranform = ballTranform;
    }
}

