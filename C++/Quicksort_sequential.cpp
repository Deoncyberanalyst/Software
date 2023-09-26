#include <iostream>
#include <time.h>
#include <fstream>
#include <chrono>


struct listArgs{
    int arraySize;
    int *list;
};

void writeValuesToFile(void *args){
    listArgs *itemsArgs1 = static_cast<listArgs *>(args);
    int size = itemsArgs1->arraySize;

    std::ofstream myfile;
    myfile.open("Matrix.txt", std::ios::app);
    myfile << std::endl << "Matrix A" << std::endl << std::endl;
    for (int i = 0; i < size; i++) myfile << i+1 << ". " <<itemsArgs1 -> list[i] << std::endl;
}

int partition(int arr[], int s, int e){
    int pivot = arr[e];
    int pIndex = s;

    for (int i = s; i < e; i++)
    {
        if(arr[i] < pivot)
        {
            //swap
            int temp = arr[i];
            arr[i] = arr[pIndex];
            arr[pIndex] = temp;
            pIndex++;
        }
    }

    int temp = arr[e];
    arr[e] = arr[pIndex];
    arr[pIndex] = temp;

    return pIndex;
}

void quicksort(int arr[], int s, int e)
{
    if (s < e){
        int p = partition(arr, s, e);
        quicksort(arr, s, (p-1));
        quicksort(arr, p+1, e);
    }
}

void initValues(void *args)
{
    listArgs *itemsArgs1 = static_cast<listArgs *>(args);
    int size = itemsArgs1->arraySize;

    for (int i = 0; i < size; i++) itemsArgs1->list[i] = rand() % 1000;
}

int main(){
    auto start = std::chrono::high_resolution_clock::now();
    srand(time(0));
    const int ARRAY_SIZE = 1000000;

    listArgs args;
    int *items = new int[ARRAY_SIZE];
    args.arraySize = ARRAY_SIZE;
    args.list = items;

    initValues(&args);

    writeValuesToFile(&args);

    quicksort(items, 0, ARRAY_SIZE-1);

    writeValuesToFile(&args);

    //Print values
    auto stop = std::chrono::high_resolution_clock::now();
    auto duration = std::chrono::duration_cast<std::chrono::microseconds>(stop - start);
    std::cout << "Time taken by function: " << duration.count() << " microseconds" << std::endl;
    //for (int i = 0; i < ARRAY_SIZE; i++) std::cout << i+1 << ". " << items[i] << std::endl;
    return 0;
}
