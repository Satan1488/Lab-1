#include <iostream>

using namespace std;

class Celoe
{
public:
	long N;
	char *dvo;
	static int p;
	long long des;
	Celoe(int i) {
		p++;
		N = i;
		dvo = new char[N];

	}
	Celoe(Celoe &x) {
		p++;
		N = x.N;
		dvo = new char[N];
		for (int i = 0;i < N;i++)
			dvo[i] = x.dvo[i];
	}
	Celoe(int i, char *_dvo) {
		p++;
		N = i;
		dvo = new char[N];
		for (int i = 0;i < N;i++)
			dvo[i] = _dvo[i];
	}
	Celoe(long long des, int i) {
		p++;
		this->des = des;
		dvo = new char[i];
		N = i;
		convert(des);
	}
	void print() {
		for (int i = 0;i < N;i++)
			cout << dvo[i];
	}
	~Celoe() {
		p--;
		delete[]dvo;
	}

	Celoe addup(Celoe &a) {
			char *it = new char[N + a.N];
			Celoe t_long = (this->N > a.N) ? *this : a;
			for (int i = 0;i < t_long.N;i++) {
				it[i] = t_long.dvo[i];
			}
			int max = (this->N > a.N) ? N : a.N;
			int min = (this->N < a.N) ? N : a.N;
			int i;
			int k = min;
			if (a.N >this->N)
				for (int n = 0;n < min - 1;i++) {
					for (i = max - 1;i >= max - min;i--) {
						if (((a.dvo[i] == '1') && (this->dvo[i] == '0')) || ((a.dvo[i] == '0') && (this->dvo[i] == '1'))) {
							it[k--] = '1';
						}
						if ((a.dvo[i] == '0') && (this->dvo[i] == '0')) {
							it[k--] = '0';
						}
						if ((a.dvo[i] == '1') && (this->dvo[i] == '1')) {
							it[k--] = '0';
							it[k--] = '1';
						}
					}
				}
			it[max + 1] = '\0';
			return  Celoe(N + a.N, it);
	}

	Celoe subtraction(Celoe &a) {
		long long h = this->des - a.des;
		char *it = new char[a.N + this->N];
		int k = 0;
		int n = 0;
		for (int i = 0;h != 0;i++) {
			it[i] = h % 2 + 48;
			h /= 2;
			n++;
		}
		char *buf = new char[a.N + this->N];
		k = n;
		for (int i = 0;i < n && k != 0;i++) {
			buf[--k] = it[i];
		}
		for (int i = 0;i < n;i++) {
			it[i] = buf[i];
		}
		it[n] = '\0';
		delete[N]buf;
		return 0;
	}

	Celoe multiplication(Celoe &a) {
		long long h = this->des*a.des;
		char *it = new char[a.N+this->N];
		int k = 0;
		int n = 0;
		for (int i = 0;h!=0;i++) {
			it[i] = h % 2 + 48;
			h /= 2;
			n++;
		}
		char *buf = new char[a.N+this->N];
		k = n;
		for (int i = 0;i < n && k != 0;i++) {
			buf[--k] = it[i];
		}
		for (int i = 0;i < n;i++) {
			it[i] = buf[i];
		}
		it[n] = '\0';
		delete[N]buf;
		return 0;
	}

	Celoe division(Celoe &a) {
		long long h = this->des/a.des  ;
		char *it = new char[a.N + this->N];
		int k = 0;
		int n = 0;
		for (int i = 0;h != 0;i++) {
			it[i] = h % 2 + 48;
			h /= 2;
			n++;
		}
		char *buf = new char[a.N + this->N];
		k = n;
		for (int i = 0;i < n && k != 0;i++) {
			buf[--k] = it[i];
		}
		for (int i = 0;i < n;i++) {
			it[i] = buf[i];
		}
		it[n] = '\0';
		delete[N]buf;
		return 0;
	}
private:
	void convert(long long _abyrvalg) {
		int k = 0;
		long long z = des;
		for (int i = 0;i < N;i++) {
			dvo[i] = z % 2 + 48;
			z /= 2;
			k++;
		}
		char *buf = new char[N];
		for (int i = 0;i < N && k != 0;i++) {
			buf[--k] = dvo[i];
		}
		for (int i = 0;i < N;i++) {
			dvo[i] = buf[i];
		}
		delete[N]buf;
	}
};

int Celoe::p = 0;

int main()
{
Celoe::p = 0;
	Celoe l(8, 4);
	l.print();// вывод 
	l.division(Celoe(3,2));//деление 
	l.addup(Celoe(3, 2));// сложение 
	l.multiplication(Celoe(3, 2));//умнофени
	l.subtraction(Celoe(3, 2));//вычетание 
	getchar();
	getchar();
}