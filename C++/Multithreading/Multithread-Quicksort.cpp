#include <iostream>
#include <time.h>
#include <fstream>
#include <chrono>
#include <omp.h>


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
        
        #pragma omp task
        quicksort(arr, s, (p-1));

        #pragma omp task
        quicksort(arr, p+1, e);

    }
}

int main(){
    auto start = std::chrono::high_resolution_clock::now();
  //  srand(time(0));
    const int ARRAY_SIZE = 100000;

    int *items = new int[ARRAY_SIZE];

    #pragma omp parallel for
    for (int i = 0; i < ARRAY_SIZE; i++) {
        items[i] = rand() % 1000000;
    }

    std::ofstream myfile;
    myfile.open("Matrix.txt", std::ios::app);
    myfile << std::endl << "Matrix UNSORTED" << std::endl << std::endl;
    for (int i = 0; i < ARRAY_SIZE; i++) myfile << i+1 << ". " <<items[i] << std::endl;

    
    quicksort(items, 0, ARRAY_SIZE-1);

    myfile << std::endl << "\n\n MATRIX SORTED!" << std::endl << std::endl;
    for (int i = 0; i < ARRAY_SIZE; i++) myfile << i+1 << ". " <<items[i] << std::endl;

    //Print values
    auto stop = std::chrono::high_resolution_clock::now();
    auto duration = std::chrono::duration_cast<std::chrono::microseconds>(stop - start);
    std::cout << "Time taken by function: " << duration.count() << " microseconds" << std::endl;
    //for (int i = 0; i < ARRAY_SIZE; i++) std::cout << i+1 << ". " << items[i] << std::endl;

    delete[] items;
    return 0;
}