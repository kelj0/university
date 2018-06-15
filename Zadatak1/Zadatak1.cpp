#include <iostream>
#include <thread>
#include <chrono>

using namespace std::chrono_literals;
using std::chrono::system_clock;

int main() {
	std::pair<int,int> a;
	std::pair<int,int> b;
	std::pair<int, int> x;
	std::cout << "A redak: ";
	std::cin >> a.first;
	std::cout << "A stupac: ";
	std::cin >> a.second;
	std::cout << "B redak: ";
	std::cin >> b.first;
	std::cout << "B stupac: ";
	std::cin >> b.second;

	x.first = a.first;
	x.second = a.second;

	while (!(x.first == b.first&&x.second == b.second)) {
		std::this_thread::sleep_until(system_clock::now() + 0.2s); //sleeps for 0.2s
		if(!(x.first == b.first&&x.second == b.second))
			system("CLS");
		if (x.first < b.first)
			++x.first;
		else if (x.first > b.first)
			--x.first;
		else{
			if (x.second < b.second)
				++x.second;
			else
				--x.second;
		}
		for (unsigned i = 1; i < 21; ++i) {
			for (unsigned j = 1; j < 41; ++j) {
				if (i == a.first && j == a.second)
					std::cout << "A";
				else if (i == x.first && j == x.second)
					std::cout << "x";
				else if (i == b.first && j == b.second)
					std::cout << "B";
				else
					std::cout << "-";			
			}
			std::cout << std::endl;
		}
	}
	system("pause");
	return 0;
}