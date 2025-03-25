using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Projet1
{
    /// <summary>
    /// Classe interne pour trier les fichiers valides dans le dossier
    /// </summary>
    internal class ImageLoader
    {
        string[] supportedExtensions = { ".pbm", ".pgm" };
        public List<Image> LoadImages(string directory)
        {
            Console.WriteLine("\nFiltrage des fichiers en cours...");
            Thread.Sleep(500);

            List<string> allFiles = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories).ToList();
            List<Image> validImages = new List<Image>();

            foreach (string file in allFiles)
            {
                Console.WriteLine("\n" + Path.GetFileName(file));

                string extension = Path.GetExtension(file);

                if (!supportedExtensions.Contains(extension))
                {
                    ConsoleService.Error($"Extension non supporté");
                    continue;
                }

                List<string> lines = File.ReadAllLines(file).ToList();

                if ((extension == ".pbm" && lines.Count < 3) || (extension == ".pgm" && lines.Count < 4))
                {
                    ConsoleService.Error("Nombre de lignes invalide");
                    continue;
                }

                int maxGrayValue = 0;

                if (extension == ".pgm" && !int.TryParse(lines[2], out maxGrayValue))
                {
                    ConsoleService.Error("Valeur de gris invalide pour PGM.");
                    continue;
                }

                List<string> dimensions = lines[1].Split(' ').ToList();
                List<int> pixels = TryParsePixels(lines, extension);
                int width, height;

                if (!TryParseDimensions(dimensions, pixels, extension, maxGrayValue, out width, out height))
                {
                    continue;
                }

                Image image = extension == ".pbm" ? new PBMImage(file, width, height, pixels) : new PGMImage(file, width, height, pixels, maxGrayValue);
                ConsoleService.Success("Fichier valide");
                validImages.Add(image);
            }

            return validImages;
        }

        private List<int> TryParsePixels(List<string> lines, string extension)
        {
            List<int> pixels = new List<int>();

            if (lines.Count < 3 && extension == ".pbm")
                return pixels;

            if (lines.Count < 4 && extension == ".pgm")
                return pixels;

            int lineSkip = extension == ".pbm" ? 2 : 3;

            foreach (string pixel in lines.Skip(lineSkip))
            {
                if (!char.IsDigit(pixel[0]))
                    return new List<int>();

                int parsedPixel = int.Parse(pixel);
                pixels.Add(parsedPixel);
            }
            return pixels;
        }

        private bool TryParseDimensions(List<string> dimensions, List<int> pixels, string extension, int maxGrayValue, out int width, out int height)
        {
            width = 0;
            height = 0;

            if (dimensions.Count < 2)
            {
                ConsoleService.Error("Dimensions manquantes.");
                return false;
            }

            if (!int.TryParse(dimensions[0].Trim(), out width) || !int.TryParse(dimensions[1].Trim(), out height))
            {
                ConsoleService.Error("Dimensions non numérique.");
                return false;
            }

            if (width <= 0 || height <= 0)
            {
                ConsoleService.Error("Dimensions négatives.");
                return false;
            }

            int expectedPixelCount = width * height;

            if (pixels.Count != expectedPixelCount)
            {
                ConsoleService.Error($"Nombre de pixels ne correspond pas aux dimensions.");
                return false;
            }

            int grayValue = extension == ".pbm" ? 1 : maxGrayValue;
            bool pixelsValid = ValidatePixels(pixels, extension, grayValue);

            if (!pixelsValid)
                return false;

            return true;
        }

        private bool ValidatePixels(List<int> pixels, string extension, int maxGrayValue)
        {
            switch (extension)
            {
                case ".pbm":
                    foreach (int pixel in pixels)
                    {
                        if (pixel != 0 && pixel != 1)
                        {
                            ConsoleService.Error("Pixel hors tons");
                            return false;
                        }
                    }
                    break;
                case ".pgm":
                    foreach (int pixel in pixels)
                    {
                        if (pixel < 0 || maxGrayValue < pixel)
                        {
                            ConsoleService.Error("Pixel hors tons");
                            return false;
                        }
                    }
                    break;
            }
            return true;
        }
    }
}
