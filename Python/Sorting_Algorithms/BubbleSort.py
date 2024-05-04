def bubble_sort(L):

    if len(L) <= 1:
        return L
    
    else:
        
        T = bubble_sort(L[1:])
        if L[0] <= T[0]:
            return [L[0]] + T
        
        else:
            
            P = [L[0]] + T[1:]
            Q = bubble_sort(P)
        return [T[0]]+ Q
        

values = [51,233,22,6,324]
print(bubble_sort(values))