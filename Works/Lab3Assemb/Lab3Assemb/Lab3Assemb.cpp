

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
	int n = -1;	
	float x;
	float eps = -1;
	bool ok;

	ok = false;
	do
	{
		cout << "Введите значение x: ";
		cin >> x;
		if (cin.fail())
		{
			cout << "Неверный ввод" << endl;
			cin.clear();
			cin.ignore(32767, '\n');
		}
		else
			ok = true;
	} while (!ok);


	ok = false;
	do
	{
		cout << "Введите значение n(не меньше 1): ";
		cin >> n;
		if (n < 1 || cin.fail())
		{
			cout << "Неверный ввод" << endl;
			cin.clear();
			cin.ignore(32767, '\n');
		}
		else
			ok = true;
	} while (!ok);

	ok = false;
	do
	{
		cout << "Введите значение eps(не меньше 0): ";
		cin >> eps;
		if (eps < 0 || cin.fail())
		{
			cout << "Неверный ввод" << endl;
			cin.clear();
			cin.ignore(32767, '\n');
		}
		else 
			ok = true;
	} while (!ok);

	float sumN = x;
	float sumEps = x;

	double res;
	__asm
	{
		finit //инициализация сопроцессора
		fild n //добавление в регистр n
		fld sumN //добавление в регистр sumN
		fld x //добавление в регистр x
		call calcN //Вызов процедуры для вычисления функции с заданным n(кол-вом элементов)

		fld eps //добавление в регистр eps
		fld sumEps//добавление в регистр sumEps
		call calcEps //Вызов процедуры для вычисления функции с заданным eps(точностью)

		jmp endProg

		calcN:
		fld1 
		fld st(1)
		
		cycle1:
		call next1
		fld st(3) 
		fadd st(0), st(1) //вычисление суммы
		fstp st(4)
		fld1
		fsubp st(5), st //уменьшение счетчика
		fldz
		fcomip st, st(5) //проверка, если заданное число элементов найдено
		je end1
		jmp cycle1

		next1:
		fmul st(0), st(2)
		fmul st(0), st(2) //возведение в куб числителя
		fld1
		faddp st(2), st //увеличение знаменателя (факториал)
		fdiv st(0), st(1) //деление числителя на знаменатель
		fld1
		faddp st(2), st //увеличение знаменателя (факториал)
		fdiv st(0), st(1) //деление числителя на знаменатель
		fchs //смена знака
		ret

		end1 :
		fxch st(3)
		fstp sumN //запись результата в переменную
		fxch st(1)
		ffree st(4) //очистка вещественного регистра
		ffree st(3)
		ffree st(2)
		ffree st(1)
		ret

		calcEps:
		fld1
		fld st(1)

		cycle2:
		call next2
		fld st(4)
		fadd st(0), st(1) //добавление элемента к суммме
		fstp st(5)
		fld st(0)
		fabs // модуль
		fcomip st(0), st(4) //достигнута ли заданная точность
		jbe end2
		jmp cycle2

		next2:
		fmul st(0), st(2)
		fmul st(0), st(2)
		fld1
		faddp st(2), st
		fdiv st(0), st(1)
		fld1
		faddp st(2), st
		fdiv st(0), st(1)
		fchs
		ret

		end2 :
		fxch st(4)
		fstp sumEps
		ffree st(3)
		ffree st(2)
		ffree st(1)
		ffree st(0)
		ret

		endProg:
	}

	cout << "Значение cos(x) при x = " << x << ":" << endl;
	cout << "C++:            " << sin(x) << endl;
	cout << "Assembler(n):   " << sumN << endl;
	cout << "Assembler(eps): " << sumEps << endl;
	system("pause");

	return 0;
}


/*cycle2:
call next2
fld st(4)
fld st(0)
fadd st(0), st(2)
fxch st(1)
fsubr st(0), st(1)
fabs
fcomip st(0), st(5)
fstp st(5)
jbe end2
jmp cycle2*/
// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
