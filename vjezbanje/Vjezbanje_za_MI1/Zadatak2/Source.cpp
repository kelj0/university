#include <iostream>
#include <vector>
#include <ctime>
#include <chrono>
#include <time.h>
using namespace std::chrono;

int main() {
	srand(static_cast<unsigned int>(time(0)));
	auto start = high_resolution_clock::now();
	
	int suma = 0;
	std::vector<int> v;
	for (int i = 0; i < 10000;++i) {
		v.push_back((rand()%5)+1);
	}
	for (int i:v) {
		suma += i;
	}
	suma = suma / 10000;
	auto end = high_resolution_clock::now();
	std::cout << "Prosijek: "<< suma <<"\nTime:" << duration_cast<milliseconds>(end - start).count() << "ms\n";
	return 0;
}