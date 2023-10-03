#include <iostream>
#include <time.h>
#include <cstdlib>
#include <chrono>
#include <pthread.h>
#include <fstream>

using namespace std::chrono;
using namespace std;

struct ThreadArgs {
    int startRow;
    int endRow;
};

const int m = 400;
const int n = 400;
const int p = 300;

int matrixA[m][n];
int matrixB[n][p];
int matrixC[m][p];

void* matrixMultiply(void* args) {
    ThreadArgs* threadArgs = static_cast<ThreadArgs*>(args);

    for (int i = threadArgs->startRow; i < threadArgs->endRow; ++i) {
        for (int y = 0; y < p; ++y) {
            for (int k = 0; k < n; ++k) {
                matrixC[i][y] += matrixA[i][k] * matrixB[k][y];
            }}}

    return nullptr;
}

void *initMatrixA(void *arg){   
    for (int i = 0; i < m; ++i) {
        for (int j = 0; j < n; ++j) {
                    matrixA[i][j] = rand() % 100;
    }}
    return nullptr;
}

void *initMatrixB(void *arg){
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < p; ++j) {
                    matrixB[i][j] = rand() % 100;
    }}return nullptr;
}

void *initMatrixC(void *arg){
    for (int i = 0; i < m; ++i) {
        for (int j = 0; j < p; ++j) {
                    matrixC[i][j] = 0;
    }}return nullptr;
}


int main(){
    auto start = high_resolution_clock::now();

    srand(time(0));
   
    const int numThreads = 8;
    const int numMatrices = 3;
    
    pthread_t thread[numThreads];

    pthread_create(&thread[0], nullptr, initMatrixA, nullptr);
    pthread_create(&thread[1], nullptr, initMatrixB, nullptr);
    pthread_create(&thread[2], nullptr, initMatrixC, nullptr);

    for (int i = 0; i < 3; i++)
    {
        pthread_join(thread[i],nullptr);
    }
    
    //Allocate x rows per thread
    ThreadArgs threadArgs[numThreads];
    int rowsPerThread = m / numThreads;

    for (int i = 0; i < numThreads; ++i) {
        threadArgs[i].startRow = i * rowsPerThread;
        threadArgs[i].endRow = (i == numThreads - 1) ? m : (i + 1) * rowsPerThread;
        pthread_create(&thread[i], nullptr, matrixMultiply, &threadArgs[i]);
    }

     for (int i = 0; i < 8; i++){
        pthread_join(thread[i], nullptr);
    }
    
    auto stop = high_resolution_clock::now();
    auto duration = duration_cast<microseconds>(stop - start);
    cout << "Time taken by function: " << duration.count() << " microseconds" << endl;

    //Write matrices to txt file
    ofstream myfile;
    myfile.open("AA.txt");

    myfile << "Matrix A" << endl << endl;
     for (int i = 0; i < m; i++) {
        for (int y = 0; y < n; y++) {
            myfile << matrixA[i][y] << ",";
        }
        myfile << endl;
    }

    myfile << "Matrix B" << endl;
     //print Matrix B
     for (int i = 0; i < n; i++) {
        for (int y = 0; y < p; y++) {
            myfile << matrixB[i][y] << ",";
        }
        myfile << endl;
    }

    myfile << "Matrix C" << endl;
     //print Matrix C
    for (int i = 0; i < m; i++) {
        for (int y = 0; y < p; y++) {
            myfile << matrixC[i][y] << ",";
        }
    myfile << endl;
    }

    return 0;
}