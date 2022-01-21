#ifndef __ENGINE_LINKED_LIST__
#define __ENGINE_LINKED_LIST__
#include <stdlib.h>
typedef struct listitem_t
{
    void* data;
    size_t size;
    
    struct listitem_t* next;
}listitem_t;

void linkedlist_insert(listitem_t** root, void* data, size_t size);
void linkedlist_remove(listitem_t** root, listitem_t* item);
void linkedlist_foreach(listitem_t* root, void(*iterationFunc)(listitem_t*));

#endif