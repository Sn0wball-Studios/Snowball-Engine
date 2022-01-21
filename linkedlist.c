#include "linkedlist.h"

void linkedlist_insert(listitem_t** root, void* data, size_t size)
{
    listitem_t* item = malloc(sizeof(*item));
    item->data = data;
    item->size = size;

    item->next = *root;

    *root = item;
}

void linkedlist_remove(listitem_t** root, listitem_t* item)
{
    listitem_t* previous = NULL;
    listitem_t* current = *root;


    if (current != NULL && current == item)
    {
        *root = current->next; // Changed head
        free(current);
        return;
    }

    while(current != item)
    {
        current = item->next;
        if(!current){return;}

         previous = current;
         //move to next link
         current = current->next;
    }


    previous->next = current->next;

    free(current);

}

void linkedlist_foreach(listitem_t* root, void(*iterationFunc)(listitem_t*))
{
    listitem_t* current = root;
    while(current != NULL)
    {
        iterationFunc(current);

        current = current->next;
    }
}