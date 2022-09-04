namespace wzebra.kit.data
{
    [System.Flags]
    [System.Serializable]
    public enum TransformMask
    {
        PositionX = 1,
        PositionY = 2,
        PositionZ = 4,

        RotationX = 8,
        RotationY = 16,
        RotationZ = 32,

        ScaleX = 64,
        ScaleY = 128,
        ScaleZ = 256,
    }
}