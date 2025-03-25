using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Projet1
{
    /// <summary>
    /// Classe pour les images PGM, hérite de la classe Image
    /// </summary>
    internal class PGMImage : Image
    {
        public int MaxGrayValue { get; set; }

        public PGMImage(string filePath, int width, int height, List<int> pixels, int maxGrayValue)
            : base(filePath, width, height, pixels)
        {
            MaxGrayValue = maxGrayValue;
        }

        public override void DoubleSize()
        {
            int newHeight = (Height * 2) - 1;
            int newWidth = (Width * 2) - 1;
            List<int> doublePixels = new List<int>();

            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    int newPixel;

                    // Si la position (x, y) est une position originale (pair, pair), on copie le pixel
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        int originalIndex = (y / 2) * Width + (x / 2);
                        newPixel = Pixels[originalIndex];
                    }
                    // Si la position est entre 2 pixels horizontaux (impair, pair)
                    else if (x % 2 == 1 && y % 2 == 0)
                    {
                        int leftIndex = (y / 2) * Width + (x / 2);
                        int rightIndex = leftIndex + 1;

                        // Vérifier si rightIndex est valide
                        if ((x / 2) + 1 < Width)
                        {
                            newPixel = (Pixels[leftIndex] + Pixels[rightIndex]) / 2;
                        }
                        else
                        {
                            newPixel = Pixels[leftIndex];
                        }
                    }
                    // Si la position est entre 2 pixels verticaux (pair, impair)
                    else if (x % 2 == 0 && y % 2 == 1)
                    {
                        int topIndex = (y / 2) * Width + (x / 2);
                        int bottomIndex = topIndex + Width;

                        // Vérifier si bottomIndex est valide
                        if ((y / 2) + 1 < Height)
                        {
                            newPixel = (Pixels[topIndex] + Pixels[bottomIndex]) / 2;
                        }
                        else
                        {
                            newPixel = Pixels[topIndex];
                        }
                    }
                    // Si la position est diagonale (impair, impair)
                    else
                    {
                        int topLeftIndex = (y / 2) * Width + (x / 2);
                        int topRightIndex = topLeftIndex + 1;
                        int bottomLeftIndex = topLeftIndex + Width;
                        int bottomRightIndex = bottomLeftIndex + 1;

                        int sum = Pixels[topLeftIndex];
                        int count = 1;

                        // Vérifier si topRightIndex est valide
                        if ((x / 2) + 1 < Width)
                        {
                            sum += Pixels[topRightIndex];
                            count++;
                        }

                        // Vérifier si bottomLeftIndex est valide
                        if ((y / 2) + 1 < Height)
                        {
                            sum += Pixels[bottomLeftIndex];
                            count++;
                        }

                        // Vérifier si bottomRightIndex est valide
                        if ((x / 2) + 1 < Width && (y / 2) + 1 < Height)
                        {
                            sum += Pixels[bottomRightIndex];
                            count++;
                        }

                        newPixel = sum / count;
                    }

                    doublePixels.Add(newPixel);
                }
            }

            List<string> newLines = new List<string>
                {
                    "P2",
                    $"{newWidth} {newHeight}",
                    $"{MaxGrayValue}"
                };

            // Utilisation du LINQ au lieu d'un loop pour convertir la liste de int en liste de string
            newLines.AddRange(doublePixels.ConvertAll(p => p.ToString()).ToList());
            Save(newLines);
        }
    }
}
