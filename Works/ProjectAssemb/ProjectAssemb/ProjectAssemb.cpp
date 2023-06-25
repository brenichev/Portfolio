// ProjectAssemb.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

using namespace std;
int main()
{
	setlocale(0, "");
	const float seconds = 60; //перевод
	const float kvt = 1000; //кватт
	const float c = 4200; //теплоемкость
	const float temp = 100; //конечная температура
	const float density = 1; //плотность воды
	float p = 1000; //мощность нагревателя
	float userAmount; // масса воды, вводит пользователь
	float userTime = 600;// желаемое время нагрева
	float kpd = 100;
	bool ok = false;
	do
	{
		cout << "Введите m - объем воды (л.): ";
		cin >> userAmount;
		if (cin.fail() || (userAmount <= 0))
		{
			cout << "Неверный ввод! Повторите попытку: " << endl;
			cin.clear();
			cin.ignore(32767, '\n');
		}
		else
			ok = true;
	} while (!ok);
	cout << endl;

	float userTemp;
	ok = false;
	do
	{
		cout << "Введите t0 - начальную температуру воды (°C) ";
		cin >> userTemp;
		if (cin.fail() || (userTemp >= 100))
		{
			cout << "Неверный ввод! Повторите попытку: " << endl;
			cin.clear();
			cin.ignore(32767, '\n');
		}
		else
			ok = true;
	} while (!ok);
	cout << endl;

	ok = false;
	do
	{
		cout << "Введите P - мощность (Ватт): ";
		cin >> p;
		if (cin.fail() || (p <= 0))
		{
			cout << "Неверный ввод! Повторите попытку: " << endl;
			cin.clear();
			cin.ignore(32767, '\n');
		}
		else
			ok = true;
	} while (!ok);
	cout << endl;


	ok = false;
	do
	{
		cout << "Введите n - КПД нагревателя (%): ";
		cin >> kpd;
		if (cin.fail() || (kpd > 100) || (kpd <= 0))
		{
			cout << "Неверный ввод! Повторите попытку: " << endl;
			cin.clear();
			cin.ignore(32767, '\n');
		}
		else
			ok = true;
	} while (!ok);
	cout << endl;


	ok = false;
	do
	{
		cout << "Введите T - желаемое время нагрева (мин.) " << userAmount << "л. воды до кипения: ";
		cin >> userTime;
		if (cin.fail() || userTime <= 0)
		{
			cout << "Неверный ввод! Повторите попытку: " << endl;
			cin.clear();
			cin.ignore(32767, '\n');
		}
		else
			ok = true;
	} while (!ok);
	cout << endl;

	float res;
	float resp;
	float energy;
	__asm
	{
		finit //инициализация сопроцессора
		fld temp //ввод конечной температуры
		fsub userTemp //разница между конечной и заданной пользователем температурой
		fmul density //плотность воды
		fmul c // темплоемкость воды
		fmul userAmount //количество воды для нагрева
		fld st
		fdiv p // мощность нагревателя
		fdiv kpd // деление на кпд
		fmul temp // *100%
		fdiv seconds //перевод в минуты
		fstp res //вывод в результирующую переменную
		fdiv userTime // расчет мощности, необходимой для нагрева за заданное время
		fdiv kpd
		fmul temp
		fdiv seconds //перевод в секунды
		fstp resp
		fld res //ввод времени нагрева
		fdiv seconds //перевод в часы
		fld p
		fdiv kvt // перевод ватт в кватт
		fmulp st(1), st //вычисление кВт*ч
		fstp energy
	}
	cout << endl << "Реальное время нагрева воды до кипения: " << res << " (мин.)" << endl;
	cout << "Мощность, необходимая для доведения " << userAmount << "л. воды до кипения за " << userTime << " (мин.) равна " << resp << " (Ватт/ч)" << endl;
	cout << "Количество энергии, которое потребовалось для нагрева воды " << energy << " (кВт * ч)" << endl;

	system("pause");

	return 0;
}


