#include "Vector.h"

int main() {
	Vector vec1;
	Vector vec2(10);
	Vector vec3(10, 2);
	
	std::cout << "vec1 capacity:" << vec1.get_capacity() <<std::endl;
	std::cout << "vec2 capacity:" << vec2.get_capacity() << std::endl;
	std::cout << "vec3 capacity:" << vec3.get_capacity()
		<< "\nvec3[4]==" << vec3.get_elem(4) << std::endl;

	std::cout << "Printing vec3...\n";
	for (int i = 0; i < vec3.get_size();i++) {
		std::cout << vec3.get_elem(i) << " ";
	}std::cout << std::endl;

	std::cout << "Inserting 5 at 5th place..\n";
	vec3.insert(3, 5);

	std::cout << "Printing vec3...\n";
	for (int i = 0; i < vec3.get_size(); i++) {
		std::cout << vec3.get_elem(i) << " ";
	}std::cout << std::endl;
		
	return 0;
}