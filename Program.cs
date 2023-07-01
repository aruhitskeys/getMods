// See https://aka.ms/new-console-template for more information
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string destinationFolder = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.UserProfile
            ),
            "Downloads"
        );

        string sourceFolder;
        if (args.Length > 0)
            sourceFolder = args[0];
        else
            sourceFolder = Directory.GetCurrentDirectory();

        RetrieveFiles(sourceFolder, destinationFolder);
    }

    static void RetrieveFiles(string sourceFolder, string destinationFolder)
    {
        Directory.CreateDirectory(destinationFolder);
        string[] files = Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories);
        foreach (string file in files)
        {
            string extension = Path.GetExtension(file);
            if (
                extension.Equals(".ttmp2", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".pmp", StringComparison.OrdinalIgnoreCase))
            {
                string fileName = Path.GetFileName(file);
                string destinationPath = Path.Combine(destinationFolder, fileName);

                if (File.Exists(destinationPath))
                {
                    string uniqeFileName = GetUniqueFileName(destinationFolder, fileName);
                    destinationPath = Path.Combine(destinationFolder, uniqeFileName);
                }

                File.Move(file, destinationPath);
                Console.WriteLine($"Moved file: {fileName}");
            }
        }
        Console.WriteLine("File retrieval complete.");
    }

    static string GetUniqueFileName(string folder, string fileName)
    {
        string baseFileName = Path.GetFileNameWithoutExtension(fileName);
        string extension = Path.GetExtension(fileName);
        string uniqeFileName = fileName;
        int counter = 1;

        while (File.Exists(Path.Combine(folder, uniqeFileName)))
        {
            uniqeFileName = $"{baseFileName}_{counter}{extension}";
            counter++;
        }

        return uniqeFileName;
    }
}