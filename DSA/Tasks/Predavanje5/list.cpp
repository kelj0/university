#include "list.h"

list::list()
{
	tail = nullptr;
	head = nullptr;	
}

void list::push_back(int val)
{
	Node *node = new Node;
	node->val = val;
	
	if ((tail == nullptr)&&(head == nullptr)) {
		tail = node;
		head = node;
	}else {
		tail->pNext = node;
		node->pPrev = tail;
		node->pNext = nullptr;
		tail = node;
	}
}
Node *list::begin() {return head;}