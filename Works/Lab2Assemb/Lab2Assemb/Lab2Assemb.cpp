// Lab2Assemb.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <math.h>
#include <conio.h>
#include <stdio.h>
#include <locale.h>
#include <Windows.h>
#include <cstdlib>
#include <string>
#include <clocale>
#include <sstream>
#include <iomanip>
using namespace std;

int main()
{
    setlocale(0, "");

    const int rows = 3;
    const int columns = 4;

    short arr2[rows][columns];

    short arr_max[rows];

    int count1 = columns;
    int count = columns * rows - 1;

    short arr1[columns];
    short Index1[6];

    for (int i = 0; i < 5; i++)
        Index1[i] = 0;

    short Index2[6];

    for (int i = 0; i < 5; i++)
        Index2[i] = 0;
    cout << "Введите массив(4 элемента) ";
    for (int i = 0; i < columns; i++)
    {
        cin >> arr1[i];
    }

    cout << endl;
    short max1, max2;
    short countMax, countMax2;

    for (int i = 0; i < rows; i++)
    {
        cout << "Введите строку массива(" << columns << " элемента) ";
        for (int j = 0; j < columns; j++)
        {
            cin >> arr2[i][j];
        }
    }

    /*  3.	Найти максимальный элемент в массиве. Элементов, равных максимальному зна-чению, в массиве может быть несколько, но не более 5.
    Выдать в простые пере-менные максимальное значение и количество этих значений, а в массив – индексы элементов.*/
    __asm 
    {
        
        firstArray:
        mov ecx, count1 // ecx - количество элементов в массиве
        inc ecx
        lea ebx, arr1 //ссылка на массив
        xor edi, edi // индекс элемента для массива с индексами максиумов
        xor esi, esi // индекс элемента массива с данными
        
        mov ax, [ebx] //первый элемент - максимум
        dec ecx
        mov dx, 1 // dx - подсчет количества максимальных элементов

        ROW:
        add esi, 2 //увеличение индекса массива
        dec ecx
        cmp [ebx + esi], ax 
        je EQUAL 
        jg BIGGER
        CNT:
        lea ebx, arr1
        and ecx, ecx
        jnz ROW
        
        jmp secondArray

        BIGGER:
        mov ax, [ebx + esi]
        mov dx, 1
        lea ebx, Index1 //ссылка на массив с индексами максимальных элементов
        push esi
        mov esi, count1 //вычисление индекса числа
        sub esi, ecx
        xor edi, edi
        mov[ebx], esi
        pop esi
        jmp CNT
            
        EQUAL:
        add dx, 1 
        add edi, 2 //увеличение индекса с индексами максимумов
        lea ebx, Index1
        push esi
        mov esi, count1 //вычисление индекса числа
        sub esi, ecx
        mov[ebx + edi], esi
        pop esi
        jmp CNT

        /*=======================================*/

        secondArray :
        mov max1, ax
        mov countMax, dx
        mov ecx, count
        inc ecx
        lea ebx, arr2
        xor edi, edi
        xor esi, esi

        mov ax, [ebx]
        dec ecx
        mov dx, 1

        ROW2:
        add esi, 2
        dec ecx
        cmp[ebx + esi], ax
        je EQUAL2
        jg BIGGER2
        CNT2:
        lea ebx, arr2
        and ecx, ecx
        jnz ROW2

        jmp EXIT

        BIGGER2:
        mov ax, [ebx + esi]
        mov dx, 1
        lea ebx, Index2
        push esi
        mov esi, count
        sub esi, ecx
        xor edi, edi
        mov[ebx], esi
        pop esi
        jmp CNT2

        EQUAL2:
        add dx, 1
        lea ebx, Index2
        add edi, 2
        push esi
        mov esi, count
        sub esi, ecx
        mov[ebx + edi], esi
        pop esi
        jmp CNT2

        EXIT:
        mov max2, ax
        mov countMax2, dx
    }
    cout << endl;
    cout << "=== Одномерный массив ===";
    cout << endl;
    cout << "Максимальный элемент: " << max1 << endl;
    cout << "Количество максимальных элементов " << countMax << endl;
    cout << "Индексы максимальных элементов(нумерация с 0)" << endl;
    for (int i = 0; i < countMax; i++)
    {
        cout << Index1[i] << ' ';
    }

    cout << endl << endl;
    cout << "=== Двумерный массив ===";
    cout << endl;
    cout << "Максимальный элемент: " << max2 << endl;
    cout << "Количество максимальных элементов " << countMax2 << endl;
    cout << "Индексы максимальных элементов(нумерация с 0)" << endl;
    for(int i = 0; i < countMax2; i++)
    {
        cout << Index2[i] << ' ';
    }
    int ac;
    cout << endl;
    for (int i = 0; i < countMax2; i++)
    {
        cout << "[" << Index2[i] / 4 << "]" << "[" << Index2[i] % (rows + 1) << "]" << ' ';
    }
    cout << endl << endl;
    system("pause");
    return 0;
}

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.

