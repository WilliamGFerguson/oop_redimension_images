using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OOP_Projet1
{
    // Lire 6274847_oop_projet01.docx
    internal class Program
    {
        static void Main()
        {
            string dir = GetValidDirectory();

            var loader = new ImageLoader();
            List<Image> images = loader.LoadImages(dir);

            if (images.Count == 0)
            {
                Console.WriteLine("\n\nAucune images valides dans ce dossier");
                return;
            }

            if (PromptUserToContinue(images.Count))
            {
                Console.Clear();
                Console.WriteLine("Traitement des images en cours...\n");

                foreach (var image in images)
                {
                    image.DoubleSize();
                }

                ConsoleService.Success("\nToutes les images ont été traitées avec succès.");
            }
            else
            {
                Console.WriteLine("\nTraitement annulé.");
            }
        }

        static string GetValidDirectory()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Entrez le chemin du dossier: ");
                string path = Console.ReadLine();

                if (Directory.Exists(path))
                    return path;

                ConsoleService.Error("\nChemin invalide.");
                Console.ReadKey();
            }
        }

        static bool PromptUserToContinue(int imageCount)
        {
            while (true)
            {
                Console.Write($"\n\nNombre d'images valides : ");
                ConsoleService.Success($"{imageCount}");
                Console.WriteLine("C  -  Continuer");
                Console.WriteLine("X  -  Annuler");

                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "c":
                        return true;
                    case "x":
                        return false;
                    default:
                        ConsoleService.Error("Entrée invalide. Appuyez sur une touche.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}