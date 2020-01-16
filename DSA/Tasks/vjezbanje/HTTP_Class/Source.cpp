#include <iostream>
#include "HTTP_klijent.h"


int main() {
	HTTP_klijent test("www.test.com",443);
	std::cout << "Testing class:\n-URL: "
		<< test.get_url()
		<< "\n-PORT: "
		<< test.get_port()
		<<std::endl;

	std::cout << "\n\nTesting random char returning..\n"
		<< test.get_random_word()
		<< std::endl;


	return 0;
}