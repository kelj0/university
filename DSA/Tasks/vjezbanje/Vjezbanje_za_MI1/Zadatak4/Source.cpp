#include <iostream>
#include <fstream>
#include <queue>

int main() {
	std::queue<int> q;
	std::ifstream in("brojevi.txt");
	std::ofstream out("output.txt");
	if (!in || !out) {
		std::cout << "Error loading\writing(to) file\n";
		return 1;
	}

	int temp;
	int counter=0;
	while(in>>temp){
		q.push(temp);
		if (counter == 4) {
			for (unsigned i = 0; i < 5; ++i) {
				out << q.front()<< " ";
				q.pop();
			}
			counter = -1;
		}
		++counter;
	}


	in.close();
	out.close();
	std::cout << std::endl;
	return 0;
}