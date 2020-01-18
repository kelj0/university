#include <iostream>
#include "list.h"

int main() {
	list mylist;

	for (unsigned int i = 0; i < 10; ++i) {
		mylist.push_back(i);
	}
	Node *tmp = mylist.begin();
	std::cout << "Printing mylist in forward way...\n";
	while (tmp!=nullptr){
		std::cout << tmp->val << " ";
		tmp = tmp->pNext;
	}

	std::cout << std::endl << std::endl;
	return 0;
}
