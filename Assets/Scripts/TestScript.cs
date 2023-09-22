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