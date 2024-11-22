 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using System;

public class ShuffleColor : MonoBehaviour
{
    public StartPoint[] startArray;
    public EndPoint[] endArray;
    private WireColor[] colors = { WireColor.Red, WireColor.Orange, WireColor.Green, WireColor.Yellow };
    private void OnEnable()
    {
        ShuffleArray(startArray);
        ShuffleArray(endArray);
        AssignRandomColors(startArray,endArray);
    }

    private void ShuffleArray<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1); // ���� �ε��������� ������ �ε����� ����
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    // �迭�� �� ��ҿ� ������ ���� �Ҵ�
    private void AssignRandomColors(StartPoint[] startArray,EndPoint[] endArray)
    {
        // colors �迭�� �������� ����
        ShuffleArray(colors);

        // �迭�� �� ��ҿ� ������ ���� �Ҵ�
        for (int i = 0; i < startArray.Length; i++)
        {
            startArray[i].setColor(colors[i]); // ���� colors �迭�� ������ ������� �Ҵ�
            startArray[i].setMaterial();
            endArray[i].setColor(colors[i]);
            endArray[i].setMaterial();
        }
    }
}
