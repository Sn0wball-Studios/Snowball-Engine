#ifndef __SNOWBALL_NODE__
#define __SNOWBALL_NODE__
typedef struct node_t
{
	const char* name;

	
	//todo linked list
	struct node_t* nodes[16];
}node_t;

int size = sizeof(node_t);
#endif