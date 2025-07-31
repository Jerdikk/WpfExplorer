using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfExplorer
{
    public class FileTreeComparer
    {
        public enum CompareMode
        {
            NameOnly,
            NameAndSize,
            NameSizeAndContent
        }

        public async Task<List<string>> CompareDirectoriesAsync(
            string dir1,
            string dir2,
            CompareMode mode = CompareMode.NameSizeAndContent)
        {
            var tree1 = await BuildFileTreeAsync(dir1, mode);
            var tree2 = await BuildFileTreeAsync(dir2, mode);

            return FindDifferences(tree1, tree2);
        }

        private async Task<Dictionary<string, FileEntry>> BuildFileTreeAsync(string rootPath, CompareMode mode)
        {
            var fileTree = new Dictionary<string, FileEntry>();
            await ScanDirectoryAsync(new DirectoryInfo(rootPath), fileTree, rootPath, mode);
            return fileTree;
        }

        private async Task ScanDirectoryAsync(DirectoryInfo directory,
            Dictionary<string, FileEntry> fileTree,
            string rootPath,
            CompareMode mode)
        {
            try
            {
                // Параллельная обработка файлов
                var fileTasks = directory.GetFiles().Select(async file =>
                {
                    var relativePath = file.FullName.Substring(rootPath.Length).TrimStart(Path.DirectorySeparatorChar);
                    var entry = new FileEntry
                    {
                        RelativePath = relativePath,
                        Size = file.Length,
                        LastWriteTime = file.LastWriteTimeUtc
                    };

                    if (mode == CompareMode.NameSizeAndContent)
                    {
                        entry.Hash = CalculateFileHash(file.FullName);
                        if (entry.Hash == null)
                        {
                            return;
                        }
                    }

                    lock (fileTree)
                    {
                        fileTree[relativePath] = entry;
                    }
                    Debug.WriteLine(relativePath);
                });

                await Task.WhenAll(fileTasks);

                // Параллельная обработка поддиректорий
                var dirTasks = directory.GetDirectories().Select(subDir =>
                    ScanDirectoryAsync(subDir, fileTree, rootPath, mode));

                await Task.WhenAll(dirTasks);
            }
            catch (UnauthorizedAccessException) { }
            catch (DirectoryNotFoundException) { }
        }

        private string CalculateFileHash(string filePath)
        {
            try
            {
                /// sha256
                /*using var sha256 = SHA256.Create();
                using var stream = File.OpenRead(filePath);
                var hashBytes = await sha256.ComputeHashAsync(stream);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();*/
                MD5 sha256 = MD5.Create();
                FileStream stream = File.OpenRead(filePath);
                var hashBytes = sha256.ComputeHash(stream);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
            catch (UnauthorizedAccessException) { }
            catch (FileNotFoundException) { }
            catch (IOException) { }
            catch (Exception ex) { }
            return null;
        }

        private List<string> FindDifferences(
            Dictionary<string, FileEntry> tree1,
            Dictionary<string, FileEntry> tree2)
        {
            var differences = new List<string>();
            var allPaths = tree1.Keys.Union(tree2.Keys).Distinct();

            foreach (var path in allPaths)
            {
                tree1.TryGetValue(path, out var entry1);
                tree2.TryGetValue(path, out var entry2);

                if (entry1 == null)
                {
                    differences.Add($"Файл добавлен: {path}");
                    continue;
                }

                if (entry2 == null)
                {
                    differences.Add($"Файл удалён: {path}");
                    continue;
                }

                if (entry1.Size != entry2.Size)
                {
                    differences.Add($"Размер отличается: {path} ({entry1.Size} vs {entry2.Size})");
                    continue;
                }

                if (entry1.Hash != null && entry2.Hash != null && entry1.Hash != entry2.Hash)
                {
                    differences.Add($"Содержимое отличается: {path}");
                }
            }

            return differences;
        }
    }

    public class FileEntry
    {
        public string RelativePath { get; set; }
        public long Size { get; set; }
        public DateTime LastWriteTime { get; set; }
        public string Hash { get; set; }
    }
}
