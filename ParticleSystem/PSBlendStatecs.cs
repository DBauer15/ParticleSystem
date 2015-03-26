using Microsoft.Xna.Framework.Graphics;

public static class PSBlendState
{
    public static BlendState Multiply = new BlendState
    {
        ColorSourceBlend = Blend.DestinationColor,
        ColorDestinationBlend = Blend.Zero,
        ColorBlendFunction = BlendFunction.Add
    };
    public static BlendState Screen = new BlendState
    {
        ColorSourceBlend = Blend.InverseDestinationColor,
        ColorDestinationBlend = Blend.One,
        ColorBlendFunction = BlendFunction.Add
    };
    public static BlendState Darken = new BlendState
    {
        ColorSourceBlend = Blend.One,
        ColorDestinationBlend = Blend.One,
        ColorBlendFunction = BlendFunction.Min
    };
    public static BlendState Lighten = new BlendState
    {
        ColorSourceBlend = Blend.One,
        ColorDestinationBlend = Blend.One,
        ColorBlendFunction = BlendFunction.Max
    };
}