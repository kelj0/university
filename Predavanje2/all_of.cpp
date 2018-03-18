/*#include <iostream>
#include <algorithm>
#include <array>

int main() {
	std::array<int, 10> arr = { 1,2,3,4,5,6,7,8,9,10 };
	//C++11 introduces lambdas allow you to write an inline, anonymous functor
	if (std::all_of(arr.begin(), arr.end(), [](int i) {return i%2;}))
		std::cout << "All numbers are odd\n";
	else
		std::cout << "All numbers are NOT odd\n";
	
	return 0;
}*/