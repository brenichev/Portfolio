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


string res = "";
int mn = 1;
extern "C"
void print(int a)
{
	res = res + std::to_string(a);
	cout << a;
}

int main()
{
	setlocale(0, "");
	
	bool ok = false;
	bool done = false;
	string str = "";
	//Проверка правильности ввода
	do
	{
		cout << "Введите двоичное число(8 бит) ";
		ok = false;
		done = false;
		cin >> str;
		ok = str.length() <= 8; // заменить на == для ввода в формате 8 бит

		int i = 0;
		while(ok && (str.length() > i))
		{
			if (str[i] != '0' && str[i] != '1')
				ok = false;
			i++;
		}
		if (!ok)
			cout << "Неверное число" << endl;
		done = true;
	} while (!ok || !done);
	
	//Перевод в 8 с помощью С++
	int c = atoi(str.c_str());
	int i = str.length();
	int rest = 0;
	int digit = c;
	int sum = 0, two = 1;
	while (i > 0)
	{
		rest = digit % 10;
		digit = digit / 10;
		sum = sum + rest * two;
		two = two * 2;
		i = i - 1;
	}
	std::cout << "Результат при переводе в C++ = " << std::oct << sum << endl;

	cout << "Результат при переводе в ассемблере = ";
	__asm
	{
		//перевод из 2 в 10
		mov eax, c //запись числа для перевода в eax
		mov esi, 1 //множитель для перевода в 10
		mov edi, 2 //увеличение множителя
		xor ecx, ecx //обнуление для суммирования в этот регистр
		mov ebx, 10 // делитель
		repeat1:
		xor edx, edx //обнуление частного
		div ebx // деление на делитель
		push eax // запись результата в стек
		mov eax, edx // перемещение числа для перевода (1 или 0 умножаются на степень двойки)
		mul esi
		add ecx, eax //суммирование в ecx результата перевода
		mov eax, esi //увеличение степени двойки
		mul edi
		mov esi, eax
		pop eax //возвращение числа из стека
		and eax, eax // проверка не равен ли нулю остаток от деления
		jnz repeat1

		mov eax, ecx // перемещение переведенного в 10 систему числа
		xor ecx, ecx
		mov ebx, 8 //делитель
		repeat2:
		xor edx, edx // обнуление частного
		div ebx //деление на делитель
		push edx //перемещение остатка в стек
		inc ecx //подсчет количества символов в результате
		and eax, eax // проверка не равен ли нулю остаток от деления
		jnz repeat2

		mov ebx, ecx // запись счетчика в ebx чтобы не обнулился в процессе вывода

		Write:
		call print // вывод на экран последнего числа из стека
		pop eax // достаем следующее число из стека
		dec ebx // уменьшаем счетчик
		jnz Write

	}

	string oct = "";
	for (; sum; sum /= 8)
		oct = char('0' + sum % 8) + oct;
	cout << endl << endl;
	if (oct == res)
		cout << "=== Результаты перевода совпали ===";
	else
		cout << "=== Результаты перевода не совпали ===";
	cout << endl << endl;
	system("pause");
	return 0;
}
