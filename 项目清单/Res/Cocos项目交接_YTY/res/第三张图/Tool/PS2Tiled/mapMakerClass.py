
from PIL import Image
import numpy

namePath = 'E:\\Cocos\\第三张图\\地图规划\\色块图2.jpg'

image = Image.open(namePath)

# x = 1116  # 起始点x
# y = 757  # 起始点y
# xPluse = 53.65  # x间隔像素
# yPluse = 32.65  # x间隔像素
# lineSize = 51  # tiled行数
# columnSize = 51  # tiled列数
# colorBlockSize = 37  # 色块数量（ps中吸色）
# deviationIn = 6  # 容差
# firstgid = 60
# colorCode = []

Dic = {
    # 0:   [255, 228, 207],
    1:   [255, 221, 189],
    2:   [255, 228, 207],
    3:   [254, 196, 114],
    4:   [246, 189, 108],
    # 5:   [254, 196, 114],
    6:   [203, 211, 92],
    7:   [194, 202, 90],
    # 8:   [194, 202, 90],
    9:   [42, 145, 92],
    # 10:  [43, 135, 88],
    11:  [44, 153, 98],
    12:  [153, 205, 79],
    # 13:  [163, 214, 83],
    14:  [164, 215, 84],
    # 15:  [255, 211, 92],
    16:  [255, 211, 92],
    17:  [255, 223, 100],
    # 18:  [255, 211, 92],
    19:  [209, 255, 253],
    # 20:  [177, 255, 255],
    21:  [177, 255, 255],
    22:  [124, 254, 242],
    # 23:  [93, 250, 241],
    24:  [94, 250, 239],
    25:  [212, 142, 108],
    26:  [200, 133, 104],
    # 27:  [200, 133, 104],
    28:  [100, 202, 180],
    # 29:  [103, 190, 173],
    30:  [103, 190, 173],
    31:  [255, 226, 122],
    # miss
    32:  [241, 106, 110],
    # 33:  [229, 103, 115],
    34:  [229, 103, 115],
    35:  [255, 226, 122],
    36:  [0, 0, 0]
}


class GetTiled:

    # name = image.show()

    def __init__(self,  x, y, xPluse, yPluse, lineSize, columnSize, colorBlockSize, deviationIn, firstgid):
        self.x = x
        self.y = y
        self.xPluse = xPluse
        self.yPluse = yPluse
        self.lineSize = lineSize
        self.columnSize = columnSize
        self.colorBlockSize = colorBlockSize
        self.deviationIn = deviationIn
        self.firstgid = firstgid

    def RGB_to_ColorCode_ex(self, argument, deviation):
        arg = numpy.array(argument)

        rgb = [212, 142, 108]
        r = numpy.where(
            numpy.logical_and(arg >= rgb[0]-deviation, arg <= rgb[0]+deviation)
        )
        g = numpy.where(
            numpy.logical_and(arg >= rgb[1]-deviation, arg <= rgb[1]+deviation)
        )
        b = numpy.where(
            numpy.logical_and(arg >= rgb[1]-deviation, arg <= rgb[1]+deviation)
        )

        if (arg[r].size > 0) & (arg[g].size > 0) & (arg[b].size > 0):
            print("1")
        else:
            print("noooooooo")

    def RGB_to_ColorCode(self, argument, deviation):
        arg = numpy.array(argument)

        for i in range(self.colorBlockSize):

            if i in Dic:

                rgb = Dic[i]
                r = numpy.where(numpy.logical_and(
                    arg >= rgb[0]-deviation, arg <= rgb[0]+deviation))
                g = numpy.where(numpy.logical_and(
                    arg >= rgb[1]-deviation, arg <= rgb[1]+deviation))
                b = numpy.where(numpy.logical_and(
                    arg >= rgb[2]-deviation, arg <= rgb[2]+deviation))

                if (arg[r].size > 0) & (arg[g].size > 0) & (arg[b].size > 0):
                    # if(i == 36):

                    #     print(str(len(r[0]))+" " +
                    #           str(len(g[0]))+" "+str(len(b[0])))
                    # print("asdasd:"+str(g[0])+"type:"+str(type(r)))
                    # if(len(r[0]) == 1 & len(g[0]) == 1 & len(b[0]) == 1):
                    return i + self.firstgid
                    # print(i)

    def Count_Line(self, yNow):
        colorCode = []
        for i in range(self.lineSize):
            xNow = self.x + self.xPluse*i
            xNow = round(xNow)
            # print(str(xNow)+","+str(yNow))
            r, g, b = image.getpixel((xNow, yNow))

            # print("block"+str(i)+":")
            # print(r, g, b)

            argg = [r, g, b]

            num = self.RGB_to_ColorCode(argg, self.deviationIn)
            colorCode.append(num)

        print(str(colorCode).replace(' ', ''))
        colorCode.clear()

    def getTiledDecode(self):
        for j in range(self.columnSize):
            yNow = self.y + self.yPluse*j
            yNow = round(yNow)

            self.Count_Line(yNow)


class GetRGB:
    def __init__(self, starX, starY, plusX, plusY, line, column):
        self.starX = starX
        self.starY = starY
        self.plusX = plusX
        self.plusY = plusY
        self.line = line
        self.column = column

    def getAllRGB(self):
        IDcounter = 0
        for j in range(self.column):
            yNow = self.starY + self.plusY*j
            for i in range(self.line):
                xNow = self.starX + self.plusX*i
                xNow = round(xNow)
                IDcounter += 1
                self.getRGB(xNow, yNow, IDcounter-1)

    def getRGB(self, x, y, id):
        r, g, b = image.getpixel((x, y))
        argg = [r, g, b]
        print("色块ID:"+str(id)+":  "+str(x) + "," +
              str(y) + "  RGB颜色:"+str(argg)+",")


# (starX, starY, plusX, plusY, line, column)
gr = GetRGB(4034, 350, 291, 142, 2, 18)
gr.getAllRGB()
# (x, y, xPluse, yPluse, lineSize, columnSize, colorBlockSize, deviationIn, firstgid)
gt = GetTiled(687, 286, 51, 51, 51, 51, 50, 6, 1)
gt.getTiledDecode()
