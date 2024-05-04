def ssort(L):
    
    if L == []:
        return L
    
    else:
        S = selectSmallest(L)
        L.remove(S)
        return [S] + ssort(L)
    
    
def selectSmallest(L):
    
    if L == []:
        return L
    
    else:

        size = len(L)-1
        min = L[0]
         
        for i in range(size):
            if L[i+1] < min:
                min = L[i+1]
                
        if L[0] < min:
            return L[0]
        
        else:
            return min
    
       
print(ssort( [100,532,631,534,212,332,122,6323212,138,125,131,125]))
