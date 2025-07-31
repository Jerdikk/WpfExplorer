using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExplorer
{
    // Класс для представления узла дерева
    public class TreeNode<T>
    {
        public T Data { get; set; }
        public List<TreeNode<T>> Children { get; set; }

        public TreeNode(T data)
        {
            Data = data;
            Children = null;
        }

        // Метод для добавления дочернего узла
        public void AddChild(TreeNode<T> child)
        {
            if (Children == null)
                Children = new List<TreeNode<T>>();
            Children.Add(child);
        }
    }

    // Класс для работы с деревом
    public class Tree<T>
    {
        //private TreeNode<MyFilesStruct> tree;

        public TreeNode<T> Root { get; set; }

        public Tree(T rootData)
        {
            Root = new TreeNode<T>(rootData);
        }

        public Tree(TreeNode<T> tree)
        {
            this.Root = tree;
        }

        // Обход дерева в глубину (DFS) с предварительным порядком
        public void TraverseDFS(TreeNode<T> node, Action<T> visit)
        {
            if (node == null) return;

            visit(node.Data); // Посещаем текущий узел

            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    TraverseDFS(child, visit); // Рекурсивно обходим всех детей
                }
            }
        }

        // Обход дерева в ширину (BFS)
        public void TraverseBFS(Action<T> visit)
        {
            if (Root == null) return;

            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                visit(currentNode.Data); // Посещаем текущий узел

                if (currentNode.Children != null)
                {
                    foreach (var child in currentNode.Children)
                    {
                        queue.Enqueue(child); // Добавляем всех детей в очередь
                    }
                }
            }
        }
    }
}
