namespace OOP_Projet1.Services
{
    /// <summary>
    /// Classe pour afficher des messages d'erreur ou de succès
    /// </summary>
    internal static class ConsoleService
    {
        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
