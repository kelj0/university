#ifndef list_H
#define list_H

struct Node{
	int val;
	Node* pPrev;
	Node* pNext;
};
class list{
public:
	list();
	void push_back(int val);
	Node *begin();
private:
	
	Node* head = nullptr;
	Node* tail = nullptr;
};

#endif
