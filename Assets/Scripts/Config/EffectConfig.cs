using System;

[Serializable]
public class EffectConfig
{
    public Jump CharPickUp;
    public Jump DeliveryPickUp;
    public Jump DeliveryManPickUp;
    public Jump MoneyPickUp;

    [Serializable]
    public class Jump
    {
        public float Power;
        public float Duration;
    }
}

