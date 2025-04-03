using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace FSGTool
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string readmePath = "project-structure.md";
                
                if (!File.Exists(readmePath))
                {
                    Console.WriteLine("Error: project-structure.md file not found");
                    return;
                }

                var content = File.ReadAllText(readmePath);
                var structure = ExtractStructure(content);

                Console.WriteLine($"Content:\n{content}");

                if (structure.Length == 0)
                {
                    Console.WriteLine("No valid folder structure found in project-structure.md");
                    Console.WriteLine("Debug: Found code block:\n" + string.Join("\n", structure));
                    return;
                }

                ProcessStructure(structure);
                Console.WriteLine("Folder structure created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static string[] ExtractStructure(string content)
        {
            // Improved regex to handle different code block formats
            var match = Regex.Match(content, @"```(?:[^\n]*\n)?([\s\S]*?)```", RegexOptions.Singleline);
            if (!match.Success) return Array.Empty<string>();

            var codeBlockContent = match.Groups[1].Value.Trim();
            var lines = codeBlockContent.Split('\n');

            // Filter out empty lines and potential language specifiers
            return lines.Where(line => 
                !string.IsNullOrWhiteSpace(line) &&
                !IsLanguageSpecifier(line.Trim())
            ).ToArray();
        }

        static bool IsLanguageSpecifier(string line)
        {
            // Common code block language identifiers
            var languages = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { 
                "text", "plaintext", "tree", "bash", "sh", 
                "powershell", "cmd", "markdown", "yaml", "json" 
            };
            return languages.Contains(line);
        }

        static void ProcessStructure(string[] structure)
        {
            var hierarchy = new List<string>();

            foreach (var line in structure)
            {
                var (depth, entry) = ParseLine(line);
                
                // Maintain hierarchy based on depth
                hierarchy = hierarchy.Take(depth).ToList();
                
                var parentDir = hierarchy.Any() 
                    ? Path.Combine(hierarchy.ToArray()) 
                    : Directory.GetCurrentDirectory();

                var fullPath = Path.Combine(parentDir, entry);

                if (IsDirectory(entry))
                {
                    Directory.CreateDirectory(fullPath);
                    hierarchy.Add(entry.TrimEnd('/'));
                }
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                    if (!File.Exists(fullPath))
                        File.Create(fullPath).Close();
                }
            }
        }

        static (int depth, string entry) ParseLine(string line)
        {
            // Handle different indentation patterns (spaces/tabs)
            var indent = Regex.Match(line, @"^[\s│]*").Length;
            var depth = indent / 4;  // Adjust this divisor for different indent sizes

            // Enhanced cleaning of tree characters
            var cleaned = Regex.Replace(line, @"[├└│─]", " ")
                             .Trim()
                             .Replace("  ", " ");

            var entry = cleaned.Contains(" ") 
                ? cleaned.Split(' ').Last().Trim()
                : cleaned;

            return (depth, entry);
        }

        static bool IsDirectory(string entry) => 
            entry.EndsWith("/") || Directory.Exists(entry);
    }
}