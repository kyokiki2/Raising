using System;

[Serializable]
public class EffectConfig
{
    public Jump CharPickUp;
    public Jump DeliveryPickUp;
    public Jump DeliveryManPickUp;
    public Jump MoneyPay;
    public Jump MoneyGet;
    public float SpawnTime;
    public float SpawnPickUp;
    public float TakeTime;
    public float MoneyHeight;

    [Serializable]
    public class Jump
    {
        public float Power;
        public float Duration;
    }
}

