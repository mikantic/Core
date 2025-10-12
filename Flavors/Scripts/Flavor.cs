using System;

namespace Core.Flavor
{
    /// <summary>
    /// interface for a flavor
    /// </summary>
    public interface IFlavor
    {

    }
    
    /// <summary>
    /// basic flavor
    /// </summary>
    [Serializable]
    public class Basic : IFlavor
    {

    }

    /// <summary>
    /// savory flavor
    /// </summary>
    [Serializable]
    public class Savory : IFlavor
    {

    }

    /// <summary>
    /// spicy flavor
    /// </summary>
    [Serializable]
    public class Spicy : IFlavor
    {

    }

    /// <summary>
    /// salty flavor
    /// </summary>
    [Serializable]
    public class Salty : IFlavor
    {

    }

    /// <summary>
    /// sweet flavor
    /// </summary>
    [Serializable]
    public class Sweet : IFlavor
    {

    }

    /// <summary>
    /// bitter flavor
    /// </summary>
    [Serializable]
    public class Bitter : IFlavor
    {

    }

    /// <summary>
    /// minty flavor
    /// </summary>
    [Serializable]
    public class Minty : IFlavor
    {

    }

    /// <summary>
    /// sour flavor
    /// </summary>
    [Serializable]
    public class Sour : IFlavor
    {

    }

    /// <summary>
    /// rotten flavor
    /// </summary>
    [Serializable]
    public class Rotten : IFlavor
    {

    }
}