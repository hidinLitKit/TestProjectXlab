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

        //obj - ��������� ���������
        object obj = d; //boxing -> d ��������� � ��������� ��� � ��������� � ����
        int d2 = (int)obj; //unboxing -> ��������� ������������ ���������� d

        TestFunc(ref d); //out ref in
        Debug.Log(d); 

        MyClass mc = new MyClass();
        TestFunc(mc);

        TestFunc(out MyClass mc2);

        object[] objArray = new object[10000]; // � ����� ������� ����� ������, ��� ������ � ����

        List<int> listInt = new List<int>(); //���������� 4, ��� ���������� ���� ����� ��������(2^n) ������ ������. capacity ������ ������ ������ �� ��������� (��������� ���������)
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