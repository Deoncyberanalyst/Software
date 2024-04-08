using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
namespace Vector
{
    public class Vector<T>
    {
        private const int DEFAULT_CAPACITY = 10;
        private T[] data;
        public int Count { get; private set; } = 0;
        public int Capacity { get; private set; } = 0;
        
        public Vector(int capacity)
        {
            data = new T[capacity];
        }
        // This is the implementation of the default constructor
        public Vector() : this(DEFAULT_CAPACITY) { }
        // An Indexer is a special type of property that allows a class or structure to be accessed the same way as array for its internal collection. 
        // For example, introducing the following indexer you may address an element of the vector as vector[i] or vector[0] or ...
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                return data[index];
            }
            set
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                data[index] = value;
            }
        }
        // This private method allows extension of the existing capacity of the vector by another 'extraCapacity' elements.
        // The new capacity is equal to the existing one plus 'extraCapacity'.
        // It copies the elements of 'data' (the existing array) to 'newData' (the new array), and then makes data pointing to 'newData'.
        private void ExtendData(int extraCapacity)
        {
            T[] newData = new T[data.Length + extraCapacity];
            for (int i = 0; i < Count; i++) newData[i] = data[i];
            data = newData;
            Capacity = data.Length;
        }
        // This method adds a new element to the existing array.
        // If the internal array is out of capacity, its capacity is first extended to fit the new element.
        public void Add(T element)
        {
            if (Count == data.Length) ExtendData(DEFAULT_CAPACITY);
            data[Count++] = element;
        }
        // This method searches for the specified object and returns the zero‐based index of the first occurrence within the entire data structure.
        // This method performs a linear search; therefore, this method is an O(n) runtime complexity operation.
        // If occurrence is not found, then the method returns –1.
        // Note that Equals is the proper method to compare two objects for equality, you must not use operator '=' for this purpose.
        public int IndexOf(T element)
        {
            for (var i = 0; i < Count; i++)
            {
                if (data[i].Equals(element)) return i;
            }
            return -1;
        }
        public void Insert(int index, T element)
        {
            Capacity = data.Length;
            if (index < 0 || index > Capacity-1 || index > Count) throw new IndexOutOfRangeException();
            if (Count == Capacity) ExtendData(DEFAULT_CAPACITY);
            
            if (index == Count) 
            {
                Add(element);
            }   
            else
            {
                if (Count-1 > Capacity) ExtendData(DEFAULT_CAPACITY);
                this[Count++] = element;
                for (int i = Count-1; i > index; i--)
                {
                    T valueA = this[i];
                    T valueb = this[i-1];
                    this[i-1] = valueA;
                    this[i] = valueb;
                }
            }
        }
        public void Clear()
        {
             T[] clearData = new T[0];
             data = clearData;
             Count = 0;
        }
        public bool Contains(T element)
        {
           return IndexOf(element) != -1;
        }
        public bool Remove(T element)
        {
            if (Contains(element)){
                RemoveAt(IndexOf(element));
                return true;
            }
            else return false;
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count-1) throw new IndexOutOfRangeException();
            
            T valueA = this[Count-1];
            for (int i = Count-1; i > index; i--)
            {
                T valueB = this[i-1];
                this[i-1] = valueA;
                valueA = valueB;
            }
            Count--;
            T[] newData = new T[Count];
            for (int i = 0; i < Count; i++) newData[i] = data[i];
            data = newData;
            Capacity = Count;
        }
        public override string ToString()
        {
            String text = "Vector Content:\n[";
            for (int i = 0; i < Count; i++) 
            {
                text += $"{this[i]}";
                if (i != Count-1) text+= ", ";
            }
            text += "]\n";
           return text;
        }
    }
}