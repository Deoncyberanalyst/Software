__kernel void add_vectors(const int size,
                      const __global int* v1,const __global int* v2,__global int* v3) {
    
    const int globalIndex = get_global_id(0);    
 
    v3[globalIndex] = v2[globalIndex] + v1[globalIndex];
}
