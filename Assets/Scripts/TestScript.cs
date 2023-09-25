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
        MyListTest();
    }
    public void MyListTest()
    {
        MyList myList = new MyList();
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

public class MyList
{
    private readonly int _defaultCapacity = 4;
    private readonly int[] _emptyArray = new int[0];
    private int _arrSize = 0;
    private int[] m_array;

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
                int[] newArray = new int[value];
                Array.Copy(m_array, newArray, _arrSize);
                m_array = newArray;
            }
        }
    }

    public MyList()
    {
        m_array = _emptyArray;
    }
    public MyList(int capacity)
    {
        m_array = new int[Capacity];
    }
    public int this[int index]
    {
        get { return m_array[index]; }
        set { m_array[index] = value; }
    }
    private void IncreaseCapacity()
    {
        int newCapacity = m_array.Length == 0 ? _defaultCapacity : Capacity * 2;
        Capacity = newCapacity;
    }
    public void Add(int item)
    {
        if(Count == Capacity) IncreaseCapacity();
        this[Count] = item;
        Count++;
    }
    public void Insert(int index, int item)
    {
        if (Count == Capacity) IncreaseCapacity();
        if(index < Count)
        {
            Array.Copy(m_array, index, m_array, index+1, Count-index);
        }
        this[index] = item;
        Count++;
    }
    public int IndexOf(int item)
    {
        for(int i=0; i<Count; i++)
        {
            if(m_array[i] == item) return i;
        }
        return -1;
    }

    public bool Remove(int item)
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
        Count--;
        if(index<Count)
        {
            Array.Copy(m_array, index + 1, m_array, index, Count - index);
        }
        
    }

    public bool Contains(int item)
    {
        return IndexOf(item) >= 0;
    }

    public void Clear()
    {
        Array.Clear(m_array, 0, Count);
        Count = 0;
    }
}