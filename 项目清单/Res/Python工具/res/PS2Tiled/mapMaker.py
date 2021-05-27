from PIL import Image
import numpy


namePath = 'E:\\Cocos\\第二张图\\地图规划\\色块图.jpg'

image = Image.open(namePath)

# name = image.show()
x = 1120  # 起始点x
y = 760  # 起始点y
xPluse = 51.35  # x间隔像素
yPluse = 30.6  # x间隔像素
lineSize = 51  # tiled行数
columnSize = 51  # tiled列数
colorBlockSize = 50  # 色块数量（ps中吸色）
deviationIn = 6  # 容差
colorCode = []
firstgid = 0


def RGB_to_ColorCode_ex(argument, deviation):
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


def RGB_to_ColorCode(argument, deviation):
    arg = numpy.array(argument)
    Dic = {  # 色块信息（ps中吸色RGB）
        0:  [249, 240, 241],

        2:  [255, 0, 0],
        3:  [42, 145, 92],

        5:  [44, 153, 98],
        6:  [153, 205, 79],

        8:  [164, 215, 84],
        9:  [255, 231, 115],
        10:  [255, 211, 92],
        11:  [255, 223, 100],

        13:  [0, 255, 0],
        14:  [200, 133, 104],

        16:  [203, 211, 92],
        17:  [194, 202, 90],


        20:  [34, 216, 227],

        22:  [62, 229, 248],
        23:  [209, 255, 253],

        25:  [177, 255, 255],
        26:  [162, 1, 255],

        28:  [94, 250, 239],
        29:  [239, 106, 109],

        31:  [229, 103, 115],
        32:  [254, 225, 121],
        33:  [255, 226, 122],
        34:  [240, 147, 106],

        36:  [230, 132, 103],
        39: [0, 0, 0]
    }
    # rgb = [212, 142, 108]
    for i in range(colorBlockSize):

        if i in Dic:
            rgb = Dic[i]
            r = numpy.where(numpy.logical_and(
                arg >= rgb[0]-deviation, arg <= rgb[0]+deviation))
            g = numpy.where(numpy.logical_and(
                arg >= rgb[1]-deviation, arg <= rgb[1]+deviation))
            b = numpy.where(numpy.logical_and(
                arg >= rgb[2]-deviation, arg <= rgb[2]+deviation))
            if (arg[r].size > 0) & (arg[g].size > 0) & (arg[b].size > 0):

                # print(i)
                return i + firstgid


def Count_Line(yNow):
    for i in range(lineSize):
        xNow = x + xPluse*i
        xNow = round(xNow)
        # print(str(xNow)+","+str(yNow))
        r, g, b = image.getpixel((xNow, yNow))

        # print("block"+str(i)+":")
        # print(r, g, b)

        argg = [r, g, b]

        num = RGB_to_ColorCode(argg, deviationIn)
        colorCode.append(num)

    print(str(colorCode).replace(' ', ''))
    colorCode.clear()


def getTiledDecode():
    for j in range(columnSize):
        yNow = y + yPluse*j
        yNow = round(yNow)

        Count_Line(yNow)


getTiledDecode()
