#include <iostream>
#include <list>
#include <fstream>
#include <vector>
#include <stack>

int main() {
	std::list<int> l;
	std::ifstream in("brojevi.txt");

	int tmp;
	while (in >> tmp)
		l.push_back(tmp);

	// Remove members that are < 0
	auto it = l.begin();
	for (auto it = l.begin(); it != l.end();++it) {
		if (*it < 0) {
			it = l.erase(it);
			if(it!=l.begin())
				--it;
		}
	}

	// List *3
	for (auto it = l.begin(); it != l.end(); ++it)
		*it = *it * 3;

	// Vec == reverse list
	std::vector<int> v;
	for (auto it = l.rbegin(); it != l.rend(); ++it)
		v.push_back(*it);

	//Vec *3
	for (auto it = v.begin(); it != v.end(); ++it)
		*it = *it * 3;

	//stack == vec
	std::stack<int> s;
	for (unsigned i = 0; i < v.size(); ++i)
		s.push(v[i]);

	// print stack
	while(!s.empty()) {
		std::cout << s.top() << " ";
		s.pop();
	}


	in.close();
	std::cout << std::endl;
	return 0;
}