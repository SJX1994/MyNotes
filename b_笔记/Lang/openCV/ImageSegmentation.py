import cv2
from matplotlib import pyplot as plt
import os


# 变量
Path = 'Lang\openCV\meme.jpg'
img = cv2.imread(Path)
img_color = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

# 常量


print(img)
print(os.path.exists(Path))


# 条件

gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
ret, step1 = cv2.threshold(gray, 230, 255, cv2.THRESH_BINARY)

print(ret)
# 输出
plt.figure("befor")
plt.imshow(img_color, cmap="gray")

plt.figure("after")
plt.imshow(step1, cmap="gray")
plt.show()

# cv2.destroyAllWindows()
