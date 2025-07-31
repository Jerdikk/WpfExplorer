using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExplorer
{
    public class DirectoryScanner
    {
        // Рекурсивное сканирование директории и построение дерева
        public TreeNode<MyFilesStruct> ScanDirectory(string rootPath)
        {
            var rootDirectory = new DirectoryInfo(rootPath);
            MyFilesStruct myFilesStruct = new MyFilesStruct();
            myFilesStruct.fullName = rootDirectory.FullName;
            myFilesStruct.fileName = rootDirectory.Name;
            myFilesStruct.tag = rootDirectory;
            myFilesStruct.typeFile = TypesFile.Directory;
            var rootNode = new TreeNode<MyFilesStruct>(myFilesStruct);
              
            ScanDirectoryRecursive(rootDirectory, rootNode);
            return rootNode;
        }

        private void ScanDirectoryRecursive(DirectoryInfo directory, TreeNode<MyFilesStruct> parentNode)
        {
            try
            {
                // Сканируем файлы в текущей директории
                foreach (var file in directory.GetFiles())
                {
                    MyFilesStruct fileStruct = new MyFilesStruct();
                    fileStruct.fullName = file.FullName;
                    fileStruct.fileName = file.Name;
                    fileStruct.typeFile = TypesFile.File;
                    fileStruct.tag = file;
                    parentNode.AddChild(new TreeNode<MyFilesStruct>(fileStruct));
                       
                }

                // Рекурсивно сканируем поддиректории
                foreach (var subDir in directory.GetDirectories())
                {
                    MyFilesStruct fileStruct = new MyFilesStruct();
                    fileStruct.fullName = subDir.FullName;
                    fileStruct.fileName = subDir.Name;
                    fileStruct.typeFile = TypesFile.Directory;
                    fileStruct.tag = subDir;
                    var dirNode = new TreeNode<MyFilesStruct>(fileStruct);                    

                    parentNode.AddChild(dirNode);
                    ScanDirectoryRecursive(subDir, dirNode);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Пропускаем директории, к которым нет доступа
            }
            catch (DirectoryNotFoundException)
            {
                // Пропускаем директории, которые не найдены
            }
        }

        // Рекурсивный вывод дерева в консоль    
        
    /*    public void PrintTree(FileSystemNode node, string indent = "", bool last = true)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("└─ ");
                indent += "   ";
            }
            else
            {
                Console.Write("├─ ");
                indent += "│  ";
            }

            // Выводим имя узла и размер (для файлов)
            var sizeInfo = node.IsDirectory ? "" : $" ({node.Size} bytes)";
            Console.WriteLine(node.Name + sizeInfo);

            // Рекурсивно выводим детей
            for (int i = 0; i < node.Children.Count; i++)
            {
                PrintTree(node.Children[i], indent, i == node.Children.Count - 1);
            }
        }
    */
    
    }

}
