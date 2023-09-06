import numpy as np
from scipy import stats
import matplotlib.pyplot as plt

X = [2,3,4,5,6,7,8,9,10,11,12]
Y = [1/36,2/36,3/36,4/36,5/36,6/36,5/36,4/36,3/36,2/36,1/36] 

for i in range(len(Y)):
      Y[i] *= 100
    
      

plt.stem(X, Y)
plt.xlabel('2 NFT sum')
plt.ylabel('probability')
plt.title('Probability of 2 NFT sum')
plt.show()