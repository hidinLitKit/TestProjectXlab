using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClass
{
    public int i;
    public int j;
}
public class TestScript : MonoBehaviour
{
    void Start()
    {
        long l = long.MaxValue; 
        int d = 0;
        d = (int)l;

        //obj - ссылочная структура
        object obj = d; //boxing -> d завернули в ссылочный тип и поместили в кучу
        int d2 = (int)obj; //unboxing -> произошло дублирование переменной d

        TestFunc(ref d); //out ref in
        Debug.Log(d); 

        MyClass mc = new MyClass();
        TestFunc(mc);

        TestFunc(out MyClass mc2);

        object[] objArray = new object[10000]; // в стеке большой кусок памяти, все данные в куче

        List<int> listInt = new List<int>(); //изначально 4, при расширении ищет более объёмную(2^n) ячейку памяти. capacity задает ячейку памяти по умолчанию (сокращает аллокацию)
        //MyListTest();

        runTest();
    }
    public void MyListTest()
    {
        MyList<int> myList = new MyList<int>();
        myList.Add(1);
        myList.Add(5);
        myList.Insert(1, 3);
        myList.Capacity = 4;
        myList.Remove(3);
        myList.RemoveAt(0);

        for (int i = 0; i < myList.Count; ++i)
        {
            Debug.Log(myList[i]);
        }
    }
    public void runTest()
    {
        MyList<MyClass> myList = new MyList<MyClass>();
        List<MyClass> standList = new List<MyClass>();
        System.Diagnostics.Stopwatch sw;

        sw = System.Diagnostics.Stopwatch.StartNew();
        for(int i = 0; i <1000000;i++)
        {
            myList.Add(new MyClass());
        }
        myList.Insert(500999, new MyClass());
        myList.RemoveAt(690345);
        Debug.Log($"Time elapsed for MyList: {sw.ElapsedMilliseconds}");
        sw.Stop();

        sw = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < 1000000; i++)
        {
            standList.Add(new MyClass());
        }
        standList.Insert(500999, new MyClass());
        standList.RemoveAt(690345);
        Debug.Log($"Time elapsed for Standart List: {sw.ElapsedMilliseconds}");
        sw.Stop();

        //MyList быстрее потому что в методах меньше проверок, чем в стандартном List



    }
    public void TestFunc(ref int d)
    {
        d = 10;
        Debug.Log(d);
    }
    public void TestFunc(MyClass mc)
    {
        //out -> mc = new MyClass()
        mc.i = 10;
        mc.j = 10;
    }
    public void TestFunc(out MyClass mc2)
    {
        mc2 = new MyClass();    
    }

}

public class MyList<T>
{
    private readonly int _defaultCapacity = 4;
    private readonly T[] _emptyArray = new T[0];
    private int _arrSize = 0;
    private T[] m_array;

    public int Count
    {
        get { return _arrSize; }
        set { _arrSize = value; }
    }
    public int Capacity
    {
        get
        {
            return m_array.Length;
        }
        set
        {
            if(value > m_array.Length)
            {
                System.Array.Resize(ref m_array, value);
            }
        }
    }

    public MyList()
    {
        m_array = _emptyArray;
    }
    public MyList(int capacity)
    {
        m_array = new T[Capacity];
    }
    public T this[int index]
    {
        get { return m_array[index]; }
        set { m_array[index] = value; }
    }
    public void Add(T item)
    {
        if (Count == Capacity) IncreaseCapacity();
        this[Count] = item;
        Count++;
    }
    private void IncreaseCapacity()
    {
        int newCapacity = m_array.Length == 0 ? _defaultCapacity : Capacity * 2;
        Capacity = newCapacity;
    }
    
    private bool CheckIndexRange(int index)
    {
        if (index < 0 || index >= Count)
            throw new ArgumentOutOfRangeException();

        return true;
    }
    public void Insert(int index, T item)
    {
        CheckIndexRange(index);
        if (Count == Capacity) IncreaseCapacity();
        if(index < Count)
        {
            Array.Copy(m_array, index, m_array, index+1, Count-index);
        }
        Count++;
        this[index] = item;
        
    }
    public int IndexOf(T item) 
    {
        for(int i=0; i<Count; i++)
        {
            if( this.Equals(item)) return i;
        }
        return -1;
    }

    public bool Remove(T item)
    {
        int index = IndexOf(item);
        if(index>=0)
        {
            RemoveAt(index);
            return true;
        }
        return false;

    }
    public void RemoveAt(int index)
    {
        CheckIndexRange(index);
        Count--;
        if(index<Count)
        {
            Array.Copy(m_array, index + 1, m_array, index, Count - index);
        }
        
    }

    public bool Contains(T item)
    {
        return IndexOf(item) >= 0;
    }

    public void Clear()
    {
        Array.Clear(m_array, 0, Count);
        Count = 0;
    }
}