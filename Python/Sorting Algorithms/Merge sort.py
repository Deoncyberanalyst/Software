def rec_merge(left,right):
    
    if left == []:
        return right
    
    if right == []:
        return left
    
    if(left[0] < right[0]):
        return [left[0]] + rec_merge(left[1:],right)
    else:
        return [right[0]] + rec_merge(left,right[1:])        
                 
                 
def rec_merge_sort(m):
    
    if len(m) <= 1:
        return m
    
    middle = len(m) // 2
    left = m[:middle]
    right = m[middle:]
    
    left = rec_merge_sort(left)
    right = rec_merge_sort(right)
    
    return rec_merge(left,right)


print(rec_merge_sort([1,4,2,3,5,6]))