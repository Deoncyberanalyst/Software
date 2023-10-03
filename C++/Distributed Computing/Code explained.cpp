#include <stdio.h>
#include <stdlib.h>
#include <CL/cl.h>

#define PRINT 1


//The purpose of this script is to configure the OpenCL environment. The actual code that runs is the kernel file, which is in a different file
//Create variables
//Create an integer to hold the value of the size of the input array
int SZ = 8;
int *v;


//The purpose of this script is to set up the OpenCL environment and pass the data to the kernel
//Below are variable declarations

//cl_mem is a datatype and it's used to hold a reference to the OpenCL memory buffer.
//It holds the data that needs to be send to the device.
cl_mem bufV;

//This is the openCL deviceID. It could be the GPU or CPU.
cl_device_id device_id;

//This is the OpenCL context. The context is the environment in which OpenCL objects 
//, such as memory buffers and command queues) are created and managed
cl_context context;


//Is an OpenCL object that stores the OpenCL program.
//This holds a collection of kernels designed to be executed on the devices.
cl_program;

//This holds the OpenCL kernel. So, it holds a function.
//The kernel is loaded into the program.
cl_ kernel;

//A queue used to kernels for execution on a device.
//In other words the command queue is used to tell the device/s which kernel to execute
cl_command_queue queue;

int err;

//If there's an event in the code, this variable will hold the outcome of said event
cl_event event = NULL;


//The below code creates function prototypes.
//This code identifies a device. The first preference is a GPU, if the API call to clGetDeviceIDs fails for the GPU, then it will attempt to use the CPU. If both devices fail, an error is reported. 
//The function returns the device ID of the CPU or GPU.
cl_device_id create_device();

//This function sets up the OpenCL environment.
void setup_openCL_device_context_queue_kernel(char *filename, char *kernelname);

//This function compiles the OpenCL program
cl_program build_program(cl_context ctx, cl_device_id dev, const char *filename);

//This function allocates memory on the devices (CPU or GPU)
void setup_kernel_memory();

//Copy v1,v2,v3 vectors and created data to the device
void copy_kernel_args();

//After output is received, this function frees up memory on the devices (CPU or GPU)
void free_memory();

//Initialises the vectors and then prints its values⁡
void init(int *&A, int size);
void print(int *A, int size);

int main(int argc, char **argv)
{
    if (argc > 1)
        //Converts the second line argument to an integer and assigns it to SZ
        SZ = atoi(argv[1]);

    //Adds random values to an array of size n. n = SZ
    init(v, SZ);



    //Creates an array called global of size 1. It holds the value of SZ
    size_t global[1] = {(size_t)SZ};

    //print values
    print(v, SZ);

    //Pass the kernel and pass in which kernel to execute.
    //This contains the source code of what we want to do.
    setup_openCL_device_context_queue_kernel((char *)"./vector_ops.cl", (char *)"square_magnitude");

    setup_kernel_memory();
    copy_kernel_args();

    //ToDo: Add comment (what is the purpose of this function? What are its arguments? Check the documentation to find more https://www.khronos.org/registry/OpenCL/sdk/2.2/docs/man/html/clEnqueueNDRangeKernel.html)
    //This function submits the kernel for execution
    //Parameter 3 specifies the what dimension the array we are passing in. In this case, we are passing in a one dimensional array
    clEnqueueNDRangeKernel(queue, kernel, 1, NULL, global, NULL, 0, NULL, &event);
    clWaitForEvents(1, &event);

    //ToDo: Add comment (what is the purpose of this function? What are its arguments?)
    //We are reading back the buffer, meaning we are waiting for an output.
    //Index 2 holds the value returned from the device's memory
    //Index 4 holds the how much data is returned from device memory.
    //Index 5 holds what's being returned   
    clEnqueueReadBuffer(queue, bufV, CL_TRUE, 0, SZ * sizeof(int), &v[0], 0, NULL, NULL);

    //result vector
    print(v, SZ);

    //frees memory for device, kernel, queue, etc.
    //you will need to modify this to free your own buffers⁡
    free_memory();
}

void init(int *&A, int size)
{
    A = (int *)malloc(sizeof(int) * size);

    for (long i = 0; i < size; i++)
    {
        A[i] = rand() % 100; // any number less than 100⁡
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
    clReleaseMemObject(bufV);

    //free opencl objects
    clReleaseKernel(kernel);
    clReleaseCommandQueue(queue);
    clReleaseProgram(program);
    clReleaseContext(context);

    free(v);
}


void copy_kernel_args()
{
    //ToDo: Add comment (what is the purpose of clSetKernelArg function? What are its arguments?)
    //clSetKernelArg is an API call to OpenCL.
    //It is used to specify the input data and parameters that the kernel function will operate on.
    //In short, this API call provides the parameters to the function that will be passed to the kernel for execution
    //Parameters passed are:
    //The kernel, which we want to set the arguments to.
    //Refers to the starting index
    //Size of the argument passed in
    //This is a pointer to the actual data we want to pass in.
    clSetKernelArg(kernel, 0, sizeof(int), (void *)&SZ);
    clSetKernelArg(kernel, 1, sizeof(cl_mem), (void *)&bufV);

    if (err < 0)
    {
        perror("Couldn't create a kernel argument");
        printf("error = %d", err);
        exit(1);
    }
}

void setup_kernel_memory()
{
     //ToDo: Add comment (what is the purpose of clCreateBuffer function? What are its arguments?) 
    //The second parameter of the clCreateBuffer is cl_mem_flags flags. Check the OpenCL documentation to find out what is it's purpose and read the List of supported memory flag values ⁡
    bufV = clCreateBuffer(context, CL_MEM_READ_WRITE, SZ * sizeof(int), NULL, NULL);

    // Copy matrices to the GPU
    clEnqueueWriteBuffer(queue, bufV, CL_TRUE, 0, SZ * sizeof(int), &v[0], 0, NULL, NULL);
}

void setup_openCL_device_context_queue_kernel(char *filename, char *kernelname)
{
    device_id = create_device();
    cl_int err;

    //This API call creates the OpenCL context (effectively an openCL environment)
    ///Parameters are:
    //Used to specify special context properties. We set it to NULL because we don't require special properties for this task.
    //Next parameter specifies the number of devices the context will be associated with
    //A pointer the device ID which will be executing the kernel
    //A call back function which states what to do in the event of an error
    //A pointer which holds the error code
    context = clCreateContext(NULL, 1, &device_id, NULL, NULL, &err);
    if (err < 0)
    {
        perror("Couldn't create a context");
        exit(1);
    }

    program = build_program(context, device_id, filename);

    //ToDo: Add comment (what is the purpose of clCreateCommandQueueWithProperties function?)
    //This API call creates the command queue which manages the execution of kernels
    //Parameters are:
    //The context which we created earlier
    //The device that the command queue will operate
    //Special properties which we dont require.
    //A pointer which holds the error code
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

    
    //clCreateProgramWithSource is an API call.
    //This API call creates the program (which will be executed on a device), based on the kernel we provided earlier.
    //The parameters are:
    //The context we created earlier
    //THe number of source codes we provided
    //Defines an array of pointers to a string in which source code strings are provided
    //Defines the size of the source code in string
    //A pointer which holds the error code
    program = clCreateProgramWithSource(ctx, 1, (const char **)&program_buffer, &program_size, &err);
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