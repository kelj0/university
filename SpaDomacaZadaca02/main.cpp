#include <SFML/Graphics.hpp>
#include "Console.h"


void mouseLocation(sf::Vector2i localPosition)
{
	std::cout<<localPosition.x<<", "<<localPosition.y<<std::endl;
}

int main() {
	srand(time(0));

	sf::RenderWindow window(sf::VideoMode(1000, 720), "GAME OF LIFE");
	window.setFramerateLimit(60);

	Console console(&window);
	console.setText();

	 bool startProcess = false;
	
	while (window.isOpen()) {
		sf::Event event;
		while (window.pollEvent(event)) {
			if (event.type == sf::Event::Closed)
				window.close();
		}
		sf::Vector2i localPosition = sf::Mouse::getPosition(window);
		mouseLocation(localPosition);

		if (sf::Mouse::isButtonPressed(sf::Mouse::Left) && console.backButton.getGlobalBounds().contains((sf::Vector2f)sf::Mouse::getPosition(window)))
			console.startWhiteboard = false;
		if (sf::Mouse::isButtonPressed(sf::Mouse::Left) && console.whiteBoard.getGlobalBounds().contains((sf::Vector2f)sf::Mouse::getPosition(window))) {
			console.startWhiteboard = true;			
			console.gen = 0;
		}
		if(console.startWhiteboard){
			console.clearAndMakeEmptyBoard();
		}
		else
			console.draw();
	}
	return 0;
}