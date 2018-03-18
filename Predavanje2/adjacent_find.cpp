#include <iostream>
#include <algorithm>
#include <array>

bool check(int a, int b) {
	return a%2==1 && b%2==1;
}
int main() {
	std::array<int, 10> arr = { 1,2,3,5,4,5,6,7,8,9 };
	std::array<int, 10>::iterator iter=std::adjacent_find(arr.begin(),arr.end(),check);
	std::cout << "First 2 odd numbers are : " << *iter << ", " << *(iter+1)<< std::endl;
	return 0;
}