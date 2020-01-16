/*Kreirajte kontejner od 10 slučajnih brojeva između 1 i 100. Pitajte korisnika
da odabere smjer kojim želi ispisati brojeve (ASC ili DESC).
Koristeći hrpu ili prioritetni red (prema želji),
ispišite brojeve u zadanom smjeru.*/

#include <iostream>
#include <ext/pb_ds/priority_queue.hpp> // #include <priority_queue>
#include <time.h>
#include <string>

int main(){
    srand(static_cast<unsigned int>(time(0)));
    
    std::string s;
    std::cout << "ASC or DESC: ";
    std::cin >> s;
    
    std::priority_queue<int,std::vector<int>,std::greater<int>> pqA;
    std::priority_queue<int> pqD;
    
    if(s=="ASC"){
        for (unsigned i = 0 ;i<10;++i)
            pqA.push((rand()%100)+1);
        
        
        while(!pqA.empty()){
            std::cout << pqA.top() << " ";
            pqA.pop();
        }
    }
    else if(s=="DESC"){
        for (unsigned i = 0 ;i<10;++i)
            pqD.push((rand()%100)+1);
        
        while(!pqD.empty()){
            std::cout << pqD.top() << " ";
            pqD.pop();
        }
    }else
        std::cout << "Why you entered invalid input?..";



    std::cout << std::endl;
    return 0;
}
