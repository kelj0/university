#include "Cvijet.h"


Cvijet::Cvijet(sf::RenderWindow* window){
	Pwindow = window;
}

void Cvijet::draw(){
	//FLOWER MIDDLE
	sf::CircleShape flowercir(40);
	flowercir.setFillColor(sf::Color::Yellow);
	flowercir.setOutlineThickness(20);
	flowercir.setOutlineColor(sf::Color::Red);
	flowercir.setPosition(100, 100);
	
	//FLOWER STICK
	sf::RectangleShape line(sf::Vector2f(150, 6));
	line.rotate(90);
	line.setFillColor(sf::Color::Green);
	line.setPosition(144,200);
	
	//LEAF
	sf::ConvexShape leaf;
	leaf.setFillColor(sf::Color::Green);
	leaf.setPointCount(4);
	leaf.setPoint(0, sf::Vector2f(50, 50));
	leaf.setPoint(1, sf::Vector2f(80, 40));
	leaf.setPoint(2, sf::Vector2f(120, 10));
	leaf.setPoint(3, sf::Vector2f(70, 30));
	leaf.setPosition(90,203);
		
	//SUN
	sf::CircleShape sun;
	sun.setFillColor(sf::Color::Yellow);
	sun.setRadius(15);
	sun.setPosition(360, 27);

	Pwindow->draw(sun);
	Pwindow->draw(flowercir);
	Pwindow->draw(line);
	Pwindow->draw(leaf);
	Pwindow->display();
	std::this_thread::sleep_until(system_clock::now() + 0.3s);

	//MOVE 1
	sun.setRadius(20);
	sun.setPosition(355, 25);
	flowercir.setOutlineThickness(15);
	flowercir.setRadius(50);
	flowercir.setPosition(90, 85);
	Pwindow->clear();
	Pwindow->draw(sun);
	Pwindow->draw(flowercir);
	Pwindow->draw(line);
	Pwindow->draw(leaf);
	Pwindow->display();
	std::this_thread::sleep_until(system_clock::now() + 0.3s);
	
	//MOVE 2
	sun.setRadius(25);
	sun.setPosition(350,20);
	flowercir.setOutlineThickness(10);
	flowercir.setRadius(60);
	flowercir.setPosition(80,70);
	Pwindow->clear();
	Pwindow->draw(sun);
	Pwindow->draw(flowercir);
	Pwindow->draw(line);
	Pwindow->draw(leaf);
	Pwindow->display();
	std::this_thread::sleep_until(system_clock::now() + 1s);

	//JOKE :P
	Pwindow->clear();
	sf::Text text;
	sf::Font font;
	font.loadFromFile("arial.ttf");
	text.setString("Pls giv Smoni");
	text.setFont(font);
	text.setPosition(100, 50);
	text.setRotation(45);
	text.setCharacterSize(64);
	text.setFillColor(sf::Color::White);
	Pwindow->draw(text);
	Pwindow->display();
	std::this_thread::sleep_until(system_clock::now() + 1.2s);
	Pwindow->clear();
}