using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    internal class AlgorithmsAndDataStructures
    {
        public static void RunExamples()
        {
            // Sorting Algorithms
            int[] numbers = { 64, 34, 25, 12, 22, 11, 90 };
            Console.WriteLine("Original array: " + string.Join(", ", numbers));

            BubbleSort(numbers);
            Console.WriteLine("After Bubble Sort: " + string.Join(", ", numbers));

            numbers = new int[] { 64, 34, 25, 12, 22, 11, 90 };
            QuickSort(numbers, 0, numbers.Length - 1);
            Console.WriteLine("After Quick Sort: " + string.Join(", ", numbers));

            // Searching Algorithms
            int searchValue = 22;
            int linearSearchResult = LinearSearch(numbers, searchValue);
            Console.WriteLine($"Linear Search: {searchValue} found at index {linearSearchResult}");

            int binarySearchResult = BinarySearch(numbers, searchValue);
            Console.WriteLine($"Binary Search: {searchValue} found at index {binarySearchResult}");

            // Stack implementation
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Console.WriteLine($"Stack top: {stack.Peek()}");
            Console.WriteLine($"Popped from stack: {stack.Pop()}");

            // Queue implementation
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("First");
            queue.Enqueue("Second");
            queue.Enqueue("Third");
            Console.WriteLine($"Queue front: {queue.Peek()}");
            Console.WriteLine($"Dequeued from queue: {queue.Dequeue()}");

            // LinkedList implementation
            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.AddLast(1);
            linkedList.AddLast(2);
            linkedList.AddLast(3);
            linkedList.AddFirst(0);
            Console.WriteLine("Linked List: " + string.Join(", ", linkedList));

            // Binary Search Tree implementation
            BinarySearchTree bst = new BinarySearchTree();
            bst.Insert(50);
            bst.Insert(30);
            bst.Insert(70);
            bst.Insert(20);
            bst.Insert(40);
            Console.WriteLine("Binary Search Tree (Inorder traversal):");
            bst.InorderTraversal();
        }

        // Bubble Sort
        private static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        // Quick Sort
        private static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(arr, low, high);
                QuickSort(arr, low, partitionIndex - 1);
                QuickSort(arr, partitionIndex + 1, high);
            }
        }

        private static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            int temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;

            return i + 1;
        }

        // Linear Search
        private static int LinearSearch(int[] arr, int x)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == x)
                    return i;
            }
            return -1;
        }

        // Binary Search
        private static int BinarySearch(int[] arr, int x)
        {
            int left = 0, right = arr.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (arr[mid] == x)
                    return mid;
                if (arr[mid] < x)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return -1;
        }
    }

    // Binary Search Tree implementation
    public class BinarySearchTree
    {
        private class Node
        {
            public int Data;
            public Node Left, Right;
            public Node(int item)
            {
                Data = item;
                Left = Right = null;
            }
        }

        private Node root;

        public BinarySearchTree()
        {
            root = null;
        }

        public void Insert(int data)
        {
            root = InsertRec(root, data);
        }

        private Node InsertRec(Node root, int data)
        {
            if (root == null)
            {
                root = new Node(data);
                return root;
            }

            if (data < root.Data)
                root.Left = InsertRec(root.Left, data);
            else if (data > root.Data)
                root.Right = InsertRec(root.Right, data);

            return root;
        }

        public void InorderTraversal()
        {
            InorderRec(root);
            Console.WriteLine();
        }

        private void InorderRec(Node root)
        {
            if (root != null)
            {
                InorderRec(root.Left);
                Console.Write(root.Data + " ");
                InorderRec(root.Right);
            }
        }
    }
}
