using UnityEngine;

public static class TransformExtension
{
    public static void InitTransform(this Transform tfm, Transform parent = null)
    {
        if (parent != null)
            tfm.SetParent(parent);

        tfm.InitLocalPosition();
        tfm.InitLocalRotation();
        tfm.InitScale();
    }

    public static void InitLocalPosition(this Transform tfm)
    {
        tfm.localPosition = Vector3.zero;
    }

    public static void InitLocalRotation(this Transform tfm)
    {
        tfm.localRotation = Quaternion.identity;
    }
    public static void InitScale(this Transform tfm)
    {
        tfm.localScale = Vector3.one;
    }

    public static void SetTransform(this Transform tfm, Transform tfmInfo)
    {
        tfm.localPosition = tfmInfo.localPosition;
        tfm.localRotation = tfmInfo.localRotation;
        tfm.localScale = tfmInfo.localScale;
    }

}
