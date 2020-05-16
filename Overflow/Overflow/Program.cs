using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overflow
{
    class Program
    {
        static void Main(string[] args)
        {
            Overflow_areas collision = new Overflow_areas();
            System.Console.WriteLine(collision.size);
            collision.Add("nguyen", "thanh tan");
            collision.Add("xin", "Chao");
            System.Console.WriteLine(collision.size);
            foreach (var item in collision.Keys)
            {
                System.Console.WriteLine(item);
            }
            System.Console.WriteLine(collision.GetValue("nguyen"));
            System.Console.WriteLine(collision.GetValue("xin"));

            //collision.Add(31, "31");
            //collision.Add(21, "21");
            //Console.WriteLine(collision.GetValue(31));
            //Console.WriteLine(collision.GetValue(21));

        }
    }

    class Info
    {
        public object Key { get; set; }
        public object Value { get; set; }
        public Info(object key, object value) { this.Key = key; this.Value = value; }
    }

    class Overflow_areas
    {
        Info inf;
        public Info[,] array;
        public int size;
        public int NumberOfArray;
        public int count;
        #region Get all Keys from list HashTables
        public object[] Keys { get => GetKeys().ToArray(); }
        public IEnumerable<object> GetKeys()
        {
            foreach (Info item in array)
            {
                if (item == null)
                    continue;
                yield return item.Key;
            }
        }
        #endregion

        #region Get all Values from list HashTables
        public object[] Values { get => GetValues().ToArray(); }
        public IEnumerable<object> GetValues()
        {
            foreach (Info item in array)
            {
                if (item == null)
                    continue;
                yield return item.Value;
            }
        }
        #endregion

        #region constructor
        public Overflow_areas()
        {
            size = 5;
            NumberOfArray = 1;
            array = new Info[NumberOfArray, size];
            count = 0;
        }
        #endregion

        #region Hash key to index of array
        public int HashCode(object key)
        {
            var sum = 0;
            var arrChars = key.ToString().ToCharArray();
            for (int i = 0; i < arrChars.Length; i++)
            {
                sum += arrChars[i] + (i + 1);
            }
            int hashcode = sum.GetHashCode();
            return (hashcode % size);

        }
        #endregion

        #region Add key and value to list HashTables
        public void Add(object key, object value)
        {
            var index = HashCode(key);
            var IndexOfArray = 0;
            inf = new Info(key, value);
            if (index > array.Length)
            {
                ReSize(index);
            }
            while (array[IndexOfArray, index] != null)
            {
                if (IndexOfArray < NumberOfArray - 1)
                {
                    IndexOfArray++;
                }
                else
                    AddArray();
            }

            array[IndexOfArray, index] = inf;
            count++;
        }
        #endregion

        #region Get Value from your key, which you input to method
        public object GetValue(object key)
        {
            var index = HashCode(key);
            var IndexOfArray = 0;
            while (!array[IndexOfArray, index].Key.Equals(key))
            {
                if (IndexOfArray < NumberOfArray - 1)
                {
                    IndexOfArray++;
                }
                else
                    return null;
            }
            return array[IndexOfArray, index].Value;
        }
        #endregion

        #region Like the name of method it remove something...
        public void Remove(object key)
        {
            var index = HashCode(key);
            var IndexOfArray = 0;
            while (!array[IndexOfArray, index].Key.Equals(key))
            {
                if (IndexOfArray < NumberOfArray - 1)
                {
                    IndexOfArray++;
                }
                else
                {
                    Console.WriteLine("Don't find key in HashTables");
                    return;
                }
            }
            Console.WriteLine("Xóa thành công");
            count--;
        }
        #endregion

        #region change new size of array for greater than old size
        public void ReSize(int index)
        {
            size = index + 1;
            Info[,] array2 = new Info[NumberOfArray, size];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array2[i, j] = array[i, j];
                }
            }
            array = array2;
        }
        #endregion

        public void AddArray()
        {
            NumberOfArray++;
            Info[,] array2 = new Info[NumberOfArray, size];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array2[i, j] = array[i, j];
                }
            }
            array = array2;
        }


        #region [ Clear() ] Xóa Toàn Bộ Key-Value Trong HashTable
        public void Clear()
        {
            size = 1000;
            NumberOfArray = 1;
            array = new Info[NumberOfArray, size];
            count = 0;
        }
        #endregion

        public void printHash()
        {
            Info[,] temp = array;

            Console.WriteLine("HashTable Hiện Tại:");
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for(int j =0; j< temp.GetLength(1); j++)
                if (temp[i,j] != null)
                {
                    Console.WriteLine("key = " + temp[i,j].Key + ", val = " + temp[i,j].Value);
                }
            }
            Console.WriteLine();
        }
    }
}