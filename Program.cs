using System;

namespace PancakeSorting
{
    // Oğuzhan Göynük 1306240038

    public class PancakeNode
    {
        public int Size { get; set; }           // pankekin boyutu
        public PancakeNode Next { get; set; }   // next node
        public PancakeNode Prev { get; set; }   // prev node

        public PancakeNode(int size)
        {
            Size = size;
            Next = null;
            Prev = null;
        }
    }

    // çift bağlı liste sınıfı
    public class DoublyLinkedList
    {
        public PancakeNode Head { get; set; }   // listenin başı
        public int Count { get; private set; }  // pankek sayısı

        public DoublyLinkedList()
        {
            Head = null;
            Count = 0;
        }

        // listeye yeni pankek ekler
        public void Add(int size)
        {
            PancakeNode newNode = new PancakeNode(size);

            if (Head == null)
            {
                Head = newNode;
            }
            else
            {
                PancakeNode current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = newNode;
                newNode.Prev = current;
            }
            Count++;
        }

        // listeyi ekrana yazdırır
        public void Display()
        {
            if (Head == null)
            {
                Console.WriteLine("liste boş");
                return;
            }

            PancakeNode current = Head;
            while (current != null)
            {
                Console.Write(current.Size + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        // üstten başlayarak ilk k pankeki ters çevirir
        public void Flip(int k)
        {
            if (k <= 1 || k > Count || Head == null)
                return;

            PancakeNode current = Head;
            PancakeNode newHead = Head;

            for (int i = 1; i < k; i++)
            {
                newHead = newHead.Next;
            }

            PancakeNode afterFlip = newHead.Next;

            PancakeNode prev = afterFlip;
            current = Head;

            for (int i = 0; i < k; i++)
            {
                PancakeNode next = current.Next;

                current.Next = prev;
                current.Prev = next;

                prev = current;
                current = next;
            }

            if (afterFlip != null)
            {
                afterFlip.Prev = Head;
            }

            Head = newHead;
            Head.Prev = null;

        }

        // verilen aralıkta en büyük pankekin pozisyonunu bulur
        private int FindMaxPosition(int currSize)
        {
            PancakeNode current = Head;
            int maxSize = current.Size;
            int maxPos = 1;
            int pos = 1;

            while (current != null && pos <= currSize)
            {
                if (current.Size > maxSize)
                {
                    maxSize = current.Size;
                    maxPos = pos;
                }
                current = current.Next;
                pos++;
            }

            return maxPos;
        }

        // pancake sorting algoritması
        public void PancakeSort()
        {
            for (int currSize = Count; currSize > 1; currSize--)
            {
                int maxPos = FindMaxPosition(currSize);

                if (maxPos == currSize)
                    continue;

                if (maxPos != 1)
                {
                    Flip(maxPos);
                }

                Flip(currSize);
            }
        }



        // listenin sıralı olup olmadığını kontrol eder
        public bool IsSorted()
        {
            if (Head == null || Head.Next == null)
                return true;

            PancakeNode current = Head;
            while (current.Next != null)
            {
                if (current.Size > current.Next.Size)
                    return false;
                current = current.Next;
            }
            return true;
        }
    }

    // ana program
    class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList pancakes1 = new DoublyLinkedList();
            pancakes1.Add(3);
            pancakes1.Add(2);
            pancakes1.Add(4);
            pancakes1.Add(1);

            Console.WriteLine("ilk durum:");
            pancakes1.Display();

            pancakes1.PancakeSort();

            Console.WriteLine("son durum:");
            pancakes1.Display();

            DoublyLinkedList pancakes2 = new DoublyLinkedList();
            pancakes2.Add(7);
            pancakes2.Add(12);
            pancakes2.Add(6);
            pancakes2.Add(3);

            Console.WriteLine("ilk durum:");
            pancakes2.Display();

            pancakes2.PancakeSort();

            Console.WriteLine("son durum:");
            pancakes2.Display();

            DoublyLinkedList pancakes3 = new DoublyLinkedList();
            pancakes3.Add(1);
            pancakes3.Add(7);
            pancakes3.Add(4);
            pancakes3.Add(2);

            Console.WriteLine("ilk durum:");
            pancakes3.Display();

            pancakes3.PancakeSort();

            Console.WriteLine("son durum:");
            pancakes3.Display();
        }
    }
}
