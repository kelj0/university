#include <iostream>
#include <fstream>
#include <string>

int main() {
	std::ifstream in("LakeHuron.txt");
	std::string temp;
	std::string najveci;
	int godina;
	
	std::string tmp;
	std::getline(in,temp);
	std::getline(in, temp, ',');
	std::getline(in,tmp,',');
	std::getline(in,najveci);
	godina = std::stod(tmp);
	
	while (std::getline(in, temp, ',')) {
		std::getline(in,tmp,',');
		std::getline(in,temp);
		if (std::stod(temp) > std::stod(najveci)) {
			najveci = temp;
			godina = std::stod(tmp);
		}
	}
	std::cout << "Godina najveceg vodostaja: " << godina;
	std::cout << std::endl;
	in.close();
	return 0;
}