#include <iostream>
#include <time.h>
#include <cstdlib>
#include <chrono>
#include <omp.h>
#include <fstream>

using namespace std::chrono;

using namespace std;

int main() {
    int rows = 3;
    int columns = 5;
    int bColumns = 4;

    srand(time(0));

    // 3 x 5
    int matrixA[rows][columns] = {{1, 2, 3, 4, 4},
                         {2, 2, 3, 5, 5},
                         {3, 2, 3, 6, 6}};

    // 3 x 4
    int matrixB[columns][4] = {{1, 2, 3, 4},
                         {1, 2, 3, 4},
                         {1, 2, 3, 4},
                         {1, 2, 3, 4},
                         {1, 2, 3, 4}};

    // 3 x 4
    int matrixC[rows][columns] = {{0,0,0,0},
                                  {0,0,0,0}, 
                                  {0,0,0,0}};
     // Initialize the result matrix with zeros


   for (int i = 0; i < rows; i++)
   {
    for (int y = 0; y < bColumns; y++)
    {
        for (int k = 0; k < columns; k++ )
        {
            matrixC[i][y] += matrixA[i][k] * matrixB[k][y];
        }
    }
   }



    ofstream myfile;
     myfile.open("text.txt");
   for (int i = 0; i < rows; i++)
   {
    for (int y = 0; y < bColumns; y++)
    {   
        cout << matrixC[i][y]  << ", ";
        myfile << matrixC[i][y]  << ", ";
    }
     cout << endl;
     myfile << endl;
   }

   myfile.close(); //
   

    return 0;
}
