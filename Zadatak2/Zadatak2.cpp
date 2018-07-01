#include <iostream>
#include <thread>
#include <chrono>
using namespace std::chrono_literals;
using std::chrono::system_clock;

int main() {
    std::pair<int,int> a,b,x;
    char fil[20][40];
    std::cout << "A redak(1-18): ";  std::cin >> a.first;
    std::cout << "A stupac(1-38): "; std::cin >> a.second;
    std::cout << "B redak(1-18): ";  std::cin >> b.first;
    std::cout << "B stupac(1-38): "; std::cin >> b.second;
    x.first = a.first;
    x.second = a.second;

    while (!(x.first == b.first&&x.second == b.second)) {
	std::this_thread::sleep_until(system_clock::now() + 0.2s); //sleeps for 0.2s
	if(!(x.first == b.first&&x.second == b.second))
	    system("CLS");
			
	if (x.first < b.first&&fil[x.first+1][x.second]!='*')
	    ++x.first;
	else if (x.first > b.first)
	    --x.first;
	else{
	    if (x.second < b.second)
	        ++x.second;
	    else
		--x.second;
	}
        for (unsigned i = 0; i < 20; ++i) {
	    for (unsigned j = 0; j < 40; ++j) {
	        if (i==0||i==19||j==0||j==39)
	            fil[i][j]='*';
	        else if (i==10&&(j==1||j==2||j==3||j==4||j==5))
	            fil[i][j]='*';
	        else if (i == a.first && j == a.second)
	            fil[i][j]='A';
	        else if (i == x.first && j == x.second)
	            fil[i][j]='x';
	        else if (i == b.first && j == b.second)
	            fil[i][j]='B';
	        else
	            fil[i][j]='-';			
	        }
	    std::cout << std::endl;
	}
	for (unsigned i = 0; i < 20; ++i) {
	    for (unsigned j = 0; j < 40; ++j){
	        std::cout << fil[i][j];
	        }
	    std::cout << std::endl;
	}
    }
    system("pause");
    return 0;
}
