using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Projet1
{
    /// <summary>
    /// Classe abstraite représentant tous les images supportés
    /// </summary>
    internal abstract class Image : IResizableImage
    {
        public string FilePath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<int> Pixels { get; set; }

        protected Image(string filePath, int width, int height, List<int> pixels)
        {
            FilePath = filePath;
            Width = width;
            Height = height;
            Pixels = pixels;
        }

        public abstract void DoubleSize();

        public void Save(List<string> newLines)
        {
            string fileName = Path.GetFileName(FilePath);
            string outputFile = Path.Combine(Path.GetDirectoryName(FilePath), $"double - {fileName}");

            File.WriteAllLines(outputFile, newLines);

            ConsoleService.Success($"Copie sauvegardée:\n{outputFile}\n");
        }
    }
}
