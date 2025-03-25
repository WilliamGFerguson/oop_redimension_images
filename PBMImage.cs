using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Projet1
{
    /// <summary>
    /// Classe pour les images PBM, hérite de la classe Image
    /// </summary>
    internal class PBMImage : Image
    {
        public PBMImage(string filePath, int width, int height, List<int> pixels)
            : base(filePath, width, height, pixels) { }

        public override void DoubleSize()
        {
            int newHeight = (Height * 2) - 1;
            int newWidth = (Width * 2) - 1;
            List<int> doublePixels = new List<int>();

            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    // Si la position (x, y) est une position originale (pair), on copie le pixel
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        int originalIndex = (y / 2) * Width + (x / 2);
                        doublePixels.Add(Pixels[originalIndex]);
                    }
                    // Si la position est entre 2 pixels horizontaux
                    else if (x % 2 == 1 && y % 2 == 0)
                    {
                        int leftIndex = (y / 2) * Width + (x / 2);
                        int rightIndex = leftIndex + 1;

                        if (rightIndex < Width * Height && Pixels[leftIndex] == 1 && Pixels[rightIndex] == 1)
                            doublePixels.Add(1);
                        else
                            doublePixels.Add(0);
                    }
                    // Si la position est entre 2 pixels verticaux
                    else if (x % 2 == 0 && y % 2 == 1)
                    {
                        int topIndex = (y / 2) * Width + (x / 2);
                        int bottomIndex = topIndex + Width;

                        if (bottomIndex < Pixels.Count && Pixels[topIndex] == 1 && Pixels[bottomIndex] == 1)
                            doublePixels.Add(1);
                        else
                            doublePixels.Add(0);
                    }
                    // Si la position est diagonale (au centre de 4 pixels)
                    else
                    {
                        doublePixels.Add(0);
                    }
                    Thread.Sleep(100);
                }
            }
            List<string> newLines = new List<string>
                {
                    "P1",
                    $"{newWidth} {newHeight}"
                };

            // Utilisation du LINQ au lieu d'un loop pour convertir la liste de int en liste de string
            newLines.AddRange(doublePixels.ConvertAll(p => p.ToString()).ToList());
            Save(newLines);
        }
    }
}
