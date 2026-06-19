namespace OOP_Projet1.Interfaces
{
    /// <summary>
    /// Interface pour définir les méthodes
    /// </summary>
    internal interface IResizableImage
    {
        public void DoubleSize();
        public void Save(List<string> newPixels);
    }
}
