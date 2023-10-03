#include <stdio.h>
#include <stdlib.h>
#include <CL/cl.h>
#include <iostream>
#include <time.h>
#include <chrono>

using namespace std::chrono;
using namespace std;

#define PRINT 1

int SZ = 100000000;


//Create three vectors
int *v1;
int *v2;
int *v3;

//Create three memory objects
cl_mem bufV1, bufV2, bufV3;

//ToDo: Add comment (what is the purpose of this variable)
cl_device_id device_id;
//ToDo: Add comment (what is the purpose of this variable)
cl_context context;
//ToDo: Add comment (what is the purpose of this variable)
cl_program program;
//ToDo: Add comment (what is the purpose of this variable)
cl_kernel kernel;
//ToDo: Add comment (what is the purpose of this variable)
cl_command_queue queue;
cl_event event = NULL;

int err;

//ToDo: Add comment (what is the purpose of this function)
cl_device_id create_device();
//ToDo: Add comment (what is the purpose of this function)
void setup_openCL_device_context_queue_kernel(char *filename, char *kernelname);
//ToDo: Add comment (what is the purpose of this function)
cl_program build_program(cl_context ctx, cl_device_id dev, const char *filename);
//ToDo: Add comment (what is the purpose of this function)
void setup_kernel_memory();
//ToDo: Add comment (what is the purpose of this function)
void copy_kernel_args();
//ToDo: Add comment (what is the purpose of this function)
void free_memory();

void init(int *&A, int size);
void print(int *A, int size);

int main(int argc, char **argv)
{
    if (argc > 1)
        SZ = atoi(argv[1]);

    auto start = high_resolution_clock::now();

    std::cout << "Initial Vectors:\n";
    init(v1, SZ);
    init(v2, SZ);
    //For pointer v3, create memory size for the size of an int multiplied by size of vector (SZ)
    v3 = (int *)malloc(sizeof(int) * SZ);


    //ToDo: Add comment (what is the purpose of this variable)
    size_t global[1] = {(size_t)SZ};

    //initial vector
  //  print(v1, SZ);
   // print(v2, SZ);

    setup_openCL_device_context_queue_kernel((char *)"./vector_ops.cl", (char *)"add_vectors");

    setup_kernel_memory();
    copy_kernel_args();

    //ToDo: Add comment (what is the purpose of this function? What are its arguments? Check the documenation to find more https://www.khronos.org/registry/OpenCL/sdk/2.2/docs/man/html/clEnqueueNDRangeKernel.html)
    clEnqueueNDRangeKernel(queue, kernel, 1, NULL, global, NULL, 0, NULL, &event);
    clWaitForEvents(1, &event);

    //Read the result matrix
    clEnqueueReadBuffer(queue, bufV3, CL_TRUE, 0, SZ * sizeof(int), &v3[0], 0, NULL, NULL);

    //result vector
    std::cout << "Results:\n";
 //   print(v3, SZ);

    //frees memory for device, kernel, queue, etc.
    //you will need to modify this to free your own buffers
    free_memory();

    auto stop = high_resolution_clock::now();

    auto duration = duration_cast<microseconds>(stop - start);


    cout << "Time taken by function: "
         << duration.count() << " microseconds" << endl;

}

void init(int *&A, int size)
{
    A = (int *)malloc(sizeof(int) * size);

    for (long i = 0; i < size; i++)
    {
        A[i] = rand() % 100; // any number less than 100
    }
}

void print(int *A, int size)
{
    if (PRINT == 0)
    {
        return;
    }

    if (PRINT == 1 && size > 15)
    {
        for (long i = 0; i < 5; i++)
        {                        //rows
            printf("%d ", A[i]); // print the cell value
        }
        printf(" ..... ");
        for (long i = size - 5; i < size; i++)
        {                        //rows
            printf("%d ", A[i]); // print the cell value
        }
    }
    else
    {
        for (long i = 0; i < size; i++)
        {                        //rows
            printf("%d ", A[i]); // print the cell value
        }
    }
    printf("\n----------------------------\n");
}

void free_memory()
{
    //free the buffers
    clReleaseMemObject(bufV1);
    clReleaseMemObject(bufV2);
    clReleaseMemObject(bufV3);

    //free opencl objects
    clReleaseKernel(kernel);
    clReleaseCommandQueue(queue);
    clReleaseProgram(program);
    clReleaseContext(context);

    free(v1);
    free(v2);
    free(v3);
}


void copy_kernel_args()
{
    //Set the arguments, which include the size of the vector, v1, v2 & v3
   clSetKernelArg(kernel, 0, sizeof(int), (void *)&SZ);
   clSetKernelArg(kernel, 1, sizeof(cl_mem), (void *)&bufV1);
   clSetKernelArg(kernel, 2, sizeof(cl_mem), (void *)&bufV2);
   clSetKernelArg(kernel, 3, sizeof(cl_mem), (void *)&bufV3);

    if (err < 0)
    {
        perror("Couldn't create a kernel argument");
        printf("error = %d", err);
        exit(1);
    }
}

void setup_kernel_memory()
{
   bufV1 = clCreateBuffer(context, CL_MEM_READ_ONLY, SZ * sizeof(int), NULL, NULL);
   bufV2 = clCreateBuffer(context, CL_MEM_READ_ONLY, SZ * sizeof(int), NULL, NULL);
   bufV3 = clCreateBuffer(context, CL_MEM_WRITE_ONLY, SZ * sizeof(int), NULL, NULL);

   clEnqueueWriteBuffer(queue, bufV1, CL_TRUE, 0, SZ * sizeof(int), &v1[0], 0, NULL, NULL);
   clEnqueueWriteBuffer(queue, bufV2, CL_TRUE, 0, SZ * sizeof(int), &v2[0], 0, NULL, NULL);
   clEnqueueWriteBuffer(queue, bufV3, CL_TRUE, 0, SZ * sizeof(int), &v3[0], 0, NULL, NULL);
}

void setup_openCL_device_context_queue_kernel(char *filename, char *kernelname)
{
    device_id = create_device();
    cl_int err;

    //ToDo: Add comment (what is the purpose of clCreateBuffer function?)
    context = clCreateContext(NULL, 1, &device_id, NULL, NULL, &err);
    if (err < 0)
    {
        perror("Couldn't create a context");
        exit(1);
    }

    program = build_program(context, device_id, filename);

    //ToDo: Add comment (what is the purpose of clCreateCommandQueueWithProperties function?)
    queue = clCreateCommandQueueWithProperties(context, device_id, 0, &err);
    if (err < 0)
    {
        perror("Couldn't create a command queue");
        exit(1);
    };


    kernel = clCreateKernel(program, kernelname, &err);
    if (err < 0)
    {
        perror("Couldn't create a kernel");
        printf("error =%d", err);
        exit(1);
    };
}

cl_program build_program(cl_context ctx, cl_device_id dev, const char *filename)
{

    cl_program program;
    FILE *program_handle;
    char *program_buffer, *program_log;
    size_t program_size, log_size;

    /* Read program file and place content into buffer */
    program_handle = fopen(filename, "r");
    if (program_handle == NULL)
    {
        perror("Couldn't find the program file");
        exit(1);
    }
    fseek(program_handle, 0, SEEK_END);
    program_size = ftell(program_handle);
    rewind(program_handle);
    program_buffer = (char *)malloc(program_size + 1);
    program_buffer[program_size] = '\0';
    fread(program_buffer, sizeof(char), program_size, program_handle);
    fclose(program_handle);

    //ToDo: Add comment (what is the purpose of clCreateProgramWithSource function? What are its arguments?)
    program = clCreateProgramWithSource(ctx, 1,
                                        (const char **)&program_buffer, &program_size, &err);
    if (err < 0)
    {
        perror("Couldn't create the program");
        exit(1);
    }
    free(program_buffer);

    /* Build program 

   The fourth parameter accepts options that configure the compilation. 
   These are similar to the flags used by gcc. For example, you can 
   define a macro with the option -DMACRO=VALUE and turn off optimization 
   with -cl-opt-disable.
   */
    err = clBuildProgram(program, 0, NULL, NULL, NULL, NULL);
    if (err < 0)
    {

        /* Find size of log and print to std output */
        clGetProgramBuildInfo(program, dev, CL_PROGRAM_BUILD_LOG,
                              0, NULL, &log_size);
        program_log = (char *)malloc(log_size + 1);
        program_log[log_size] = '\0';
        clGetProgramBuildInfo(program, dev, CL_PROGRAM_BUILD_LOG,
                              log_size + 1, program_log, NULL);
        printf("%s\n", program_log);
        free(program_log);
        exit(1);
    }

    return program;
}

cl_device_id create_device() {

   cl_platform_id platform;
   cl_device_id dev;
   int err;

   /* Identify a platform */
   err = clGetPlatformIDs(1, &platform, NULL);
   if(err < 0) {
      perror("Couldn't identify a platform");
      exit(1);
   } 

   // Access a device
   // GPU
   err = clGetDeviceIDs(platform, CL_DEVICE_TYPE_GPU, 1, &dev, NULL);
   if(err == CL_DEVICE_NOT_FOUND) {
      // CPU
      printf("GPU not found\n");
      err = clGetDeviceIDs(platform, CL_DEVICE_TYPE_CPU, 1, &dev, NULL);
   }
   if(err < 0) {
      perror("Couldn't access any devices");
      exit(1);   
   }

   return dev;
}