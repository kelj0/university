#include <iostream>
#include <algorithm>
#include <array>

bool check(int i) {
	return i % 2==0;
}

int main() {
	std::array<int, 10> arr = { 3,5,7,6,3,2,1,4,5,6 };
	std::array<int, 10>::iterator iter = std::find_if(arr.begin(), arr.end(),check);
	if (iter != arr.end())//if iter is not pointing on last element of array+1
		std::cout << "First even num in arr is " << *iter << std::endl;
	else
		std::cout << "No even nums in this array\n";
	
	return 0;
}