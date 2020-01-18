#include "http.h"

int main() {
	http klijent("https://www.bla.com",443);
	std::cout << "GET:" << klijent.get()<<std::endl;
	std::cout << "POST:" << klijent.post() << std::endl;

	return 0;
}