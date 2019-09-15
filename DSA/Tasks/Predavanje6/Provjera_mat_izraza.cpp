#include <iostream>
#include <string>
#include <stack>

int main() {
	std::stack<char> s;
	std::string equation;

	std::cout << "Enter equation to check: ";
	std::getline(std::cin, equation);
	for (char i : equation) {
		if (i=='('){
			s.push(i);
		}
		else if (i == ')') {
			if (s.empty() || s.top() != '(') {
				std::cout << "Equation is wrong!\n";
				return 1;
			}
			else
				s.pop();
		}
	}
	if (s.empty())
		std::cout << "Equation is good!\n";
	else 
		std::cout << "Equation is wrong!\n";

	return 0;
}
