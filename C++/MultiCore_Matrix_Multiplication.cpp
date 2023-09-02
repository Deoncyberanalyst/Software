#include <iostream>
#include <time.h>
#include <cstdlib>
#include <chrono>
#include <omp.h>
#include <fstream>

using namespace std::chrono;
using namespace std;

//void* is a generic pointer type that can point to any data type
/*
 when you pass a void* to a function, you usually need to know the
 actual data type it points to within the function, and you may need
 to cast it back to the correct type.
*/

int main()
{
    auto start = high_resolution_clock::now();
    int m = 400;
    int n = 400;
    int p = 300;

    srand(time(0));
    
    /*
    To multiply an m×n matrix by an n×p matrix, 
    the ns must be the same,
    and the result is an m×p matrix.
    */
    
    int matrixA[m][n] = {{0}};
    int matrixB[n][p] = {{0}};
    int matrixC[m][p] = {{0}};

    //populate Matrix A
    for (int i = 0; i < m; i++){
        for (int y = 0; y < n; y++){
            matrixA[i][y] = (rand() % 100) ;
        }
    }

    //populate Matrix B
     for (int i = 0; i < n; i++){
        for (int y = 0; y < p; y++){
            matrixB[i][y] = (rand() % 100);
        }
    }

    //Setting values to 0 to avoid garbage values in Matrix C
    for (int i = 0; i < m; i++){
        for (int y = 0; y < p; y++){
            matrixC[i][y] = 0;
        }
    }


    for (int i = 0; i < m; i++) {
        for (int y = 0; y < p; y++) {
            for (int k = 0; k < n; k++) {
                matrixC[i][y] += matrixA[i][k] * matrixB[k][y];
            }
        }
    }


    //Write matrices to txt file
    ofstream myfile;
    myfile.open("matrixMultiplication.txt");
    myfile << "Matrix A" << std::endl << std::endl;
     for (int i = 0; i < m; i++) {
        for (int y = 0; y < n; y++) {
            myfile << matrixA[i][y] << ",";
        }
        myfile << std::endl;
    }

    myfile << "Matrix B" << std::endl;
     //print Matrix B
     for (int i = 0; i < n; i++) {
        for (int y = 0; y < p; y++) {
            myfile << matrixB[i][y] << ",";
        }
        myfile << std::endl;
    }

    myfile << "Matrix C" << std::endl;
     //print Matrix C
    for (int i = 0; i < m; i++) {
        for (int y = 0; y < p; y++) {
            myfile << matrixC[i][y] << ",";
        }
    myfile << std::endl;
    }

    auto stop = high_resolution_clock::now();
    auto duration = duration_cast<microseconds>(stop - start);
    myfile << "Time taken by function: " << duration.count() << " microseconds" << std::endl;
    std::cout << "Time taken by function: " << duration.count() << " microseconds" << std::endl;

    myfile.close();
    return 0;
}