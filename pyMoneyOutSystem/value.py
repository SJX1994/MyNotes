#!/usr/bin/python
# -*- coding: UTF-8 -*-
import numpy
import json
import requests
import uuid
import PySimpleGUI as sg
import os
import random
import time


from datetime import datetime
from sys import platform
from prettytable import PrettyTable


# 公共数据 ↓
# -----游戏名称          'GameName'
# -----市场价格          'MarketPrice'
# -----购买备注          'BuyInRemarks'
# -----成本利润(0.x)         'CostProfit'
# -----修改仓库容量   'GameStockSizeWrite'

# -----交易码             'BuyInCode'
# -----游戏交易时间       'BuyInTime'
# -----回收价格           'BuyInPrice'
# -----游戏库存           'GameStock'
# -----读取仓库容量    'GameStockSizeRead'
root = os.getcwd()

path = os.path.join(root, 'GamesBuy.json')
stockPath = os.path.join(root, 'GamesBuyStock.json')
sellPath = os.path.join(root, 'GamesSellStock.json')

# region json 格式
# 单个游戏交易格式
list_default = [{
    "游戏名称": "NONE",
    "市场价格": "NONE",
    "购买备注": "NONE",
    "成本利润(0.x)": "NONE",
    "交易码": "NONE",
    "交易时间": "NONE",
    "回收价格": "NONE",
    "仓库容量": "NONE"
}]

dire_default = {
    "游戏名称": "NONE",
    "市场价格": "NONE",
    "购买备注": "NONE",
    "成本利润(0.x)": "NONE",
    "交易码": "NONE",
    "交易时间": "NONE",
    "回收价格": "NONE",
    "仓库容量": "NONE"
}
# 游戏库交易格式
Stock_default = [{
    "游戏名称": "NONE",
    "库存": 0,
    "仓库容量": 0,
    "买入价格": 0,
    "卖出价格": 0,
    "最后更新时间": "00"
}]

Stock_Append_default = {
    "游戏名称": "NONE",
    "库存": 0,
    "仓库容量": 0,
    "买入价格": 0,
    "卖出价格": 0,
    "最后更新时间": "00"
}
# 单个卖出交易格式
sell_default = [{
    "交易码": "NONE",
    "游戏名称": "NONE",
    "买入价格": 0,
    "卖出价格": 0,
    "卖出时间": "00",
    "买入时间": "00"

}]

sell_Append_default = {
    "交易码": "NONE",
    "游戏名称": "NONE",
    "买入价格": 0,
    "卖出价格": 0,
    "卖出时间": "00",
    "买入时间": "00"
}
# endregion


# 程序数据栈 ↓

Stock_dataBase = {"terminal_Number": 0}


# 系统版本 ↓


class SystemTest:
    def __init__(self):
        print("目前的操作系统是：")

    def test(self):

        if platform == "linux" or platform == "linux2":
            print(" Linus 的 系统")

        elif platform == "darwin":
            print(" 乔布斯 的 操作系统")

        elif platform == "win32":
            print(" 比尔盖斯 的 操作系统")


# 图形界面 ↓


class GUI:
    def __init__(self):
        print("gui __init__")

    def show(self):

        m_JRAW = JsonReadAndWrite("", 0)
        terminal = ""
        theme = [
            'Black', 'BlueMono', 'BluePurple', 'BrightColors', 'BrownBlue',
            'Dark', 'Dark2', 'DarkAmber', 'DarkBlack', 'DarkBlack1',
            'DarkBlue', 'DarkBlue1', 'DarkBlue10', 'DarkBlue11', 'DarkBlue12',
            'DarkBlue13', 'DarkBlue14', 'DarkBlue15', 'DarkBlue16',
            'DarkBlue17', 'DarkBlue2', 'DarkBlue3', 'DarkBlue4', 'DarkBlue5',
            'DarkBlue6', 'DarkBlue7', 'DarkBlue8', 'DarkBlue9', 'DarkBrown',
            'DarkBrown1', 'DarkBrown2', 'DarkBrown3', 'DarkBrown4',
            'DarkBrown5', 'DarkBrown6', 'DarkGreen', 'DarkGreen1',
            'DarkGreen2', 'DarkGreen3', 'DarkGreen4', 'DarkGreen5',
            'DarkGreen6', 'DarkGrey', 'DarkGrey1', 'DarkGrey2', 'DarkGrey3',
            'DarkGrey4', 'DarkGrey5', 'DarkGrey6', 'DarkGrey7', 'DarkPurple',
            'DarkPurple1', 'DarkPurple2', 'DarkPurple3', 'DarkPurple4',
            'DarkPurple5', 'DarkPurple6', 'DarkRed', 'DarkRed1', 'DarkRed2',
            'DarkTanBlue', 'DarkTeal', 'DarkTeal1', 'DarkTeal10', 'DarkTeal11',
            'DarkTeal12', 'DarkTeal2', 'DarkTeal3', 'DarkTeal4', 'DarkTeal5',
            'DarkTeal6', 'DarkTeal7', 'DarkTeal8', 'DarkTeal9', 'Default',
            'Default1', 'DefaultNoMoreNagging', 'Green', 'GreenMono',
            'GreenTan', 'HotDogStand', 'Kayak', 'LightBlue', 'LightBlue1',
            'LightBlue2', 'LightBlue3', 'LightBlue4', 'LightBlue5',
            'LightBlue6', 'LightBlue7', 'LightBrown', 'LightBrown1',
            'LightBrown10', 'LightBrown11', 'LightBrown12', 'LightBrown13',
            'LightBrown2', 'LightBrown3', 'LightBrown4', 'LightBrown5',
            'LightBrown6', 'LightBrown7', 'LightBrown8', 'LightBrown9',
            'LightGray1', 'LightGreen', 'LightGreen1', 'LightGreen10',
            'LightGreen2', 'LightGreen3', 'LightGreen4', 'LightGreen5',
            'LightGreen6', 'LightGreen7', 'LightGreen8', 'LightGreen9',
            'LightGrey', 'LightGrey1', 'LightGrey2', 'LightGrey3',
            'LightGrey4', 'LightGrey5', 'LightGrey6', 'LightPurple',
            'LightTeal', 'LightYellow', 'Material1', 'Material2',
            'NeutralBlue', 'Purple', 'Reddit', 'Reds', 'SandyBeach',
            'SystemDefault', 'SystemDefault1', 'SystemDefaultForReal', 'Tan',
            'TanBlue', 'TealMono', 'Topanga'
        ]
        colors = [
            'snow', 'ghost white', 'white smoke', 'gainsboro', 'floral white',
            'old lace', 'linen', 'antique white', 'papaya whip',
            'blanched almond', 'bisque', 'peach puff', 'navajo white',
            'lemon chiffon', 'mint cream', 'azure', 'alice blue', 'lavender',
            'lavender blush', 'misty rose', 'dark slate gray', 'dim gray',
            'slate gray', 'light slate gray', 'gray', 'light grey',
            'midnight blue', 'navy', 'cornflower blue', 'dark slate blue',
            'slate blue', 'medium slate blue', 'light slate blue',
            'medium blue', 'royal blue', 'blue', 'dodger blue',
            'deep sky blue', 'sky blue', 'light sky blue', 'steel blue',
            'light steel blue', 'light blue', 'powder blue', 'pale turquoise',
            'dark turquoise', 'medium turquoise', 'turquoise', 'cyan',
            'light cyan', 'cadet blue', 'medium aquamarine', 'aquamarine',
            'dark green', 'dark olive green', 'dark sea green', 'sea green',
            'medium sea green', 'light sea green', 'pale green',
            'spring green', 'lawn green', 'medium spring green',
            'green yellow', 'lime green', 'yellow green', 'forest green',
            'olive drab', 'dark khaki', 'khaki', 'pale goldenrod',
            'light goldenrod yellow', 'light yellow', 'yellow', 'gold',
            'light goldenrod', 'goldenrod', 'dark goldenrod', 'rosy brown',
            'indian red', 'saddle brown', 'sandy brown', 'dark salmon',
            'salmon', 'light salmon', 'orange', 'dark orange', 'coral',
            'light coral', 'tomato', 'orange red', 'red', 'hot pink',
            'deep pink', 'pink', 'light pink', 'pale violet red', 'maroon',
            'medium violet red', 'violet red', 'medium orchid', 'dark orchid',
            'dark violet', 'blue violet', 'purple', 'medium purple', 'thistle',
            'snow2', 'snow3', 'snow4', 'seashell2', 'seashell3', 'seashell4',
            'AntiqueWhite1', 'AntiqueWhite2', 'AntiqueWhite3', 'AntiqueWhite4',
            'bisque2', 'bisque3', 'bisque4', 'PeachPuff2', 'PeachPuff3',
            'PeachPuff4', 'NavajoWhite2', 'NavajoWhite3', 'NavajoWhite4',
            'LemonChiffon2', 'LemonChiffon3', 'LemonChiffon4', 'cornsilk2',
            'cornsilk3', 'cornsilk4', 'ivory2', 'ivory3', 'ivory4',
            'honeydew2', 'honeydew3', 'honeydew4', 'LavenderBlush2',
            'LavenderBlush3', 'LavenderBlush4', 'MistyRose2', 'MistyRose3',
            'MistyRose4', 'azure2', 'azure3', 'azure4', 'SlateBlue1',
            'SlateBlue2', 'SlateBlue3', 'SlateBlue4', 'RoyalBlue1',
            'RoyalBlue2', 'RoyalBlue3', 'RoyalBlue4', 'blue2', 'blue4',
            'DodgerBlue2', 'DodgerBlue3', 'DodgerBlue4', 'SteelBlue1',
            'SteelBlue2', 'SteelBlue3', 'SteelBlue4', 'DeepSkyBlue2',
            'DeepSkyBlue3', 'DeepSkyBlue4', 'SkyBlue1', 'SkyBlue2', 'SkyBlue3',
            'SkyBlue4', 'LightSkyBlue1', 'LightSkyBlue2', 'LightSkyBlue3',
            'LightSkyBlue4', 'SlateGray1', 'SlateGray2', 'SlateGray3',
            'SlateGray4', 'LightSteelBlue1', 'LightSteelBlue2',
            'LightSteelBlue3', 'LightSteelBlue4', 'LightBlue1', 'LightBlue2',
            'LightBlue3', 'LightBlue4', 'LightCyan2', 'LightCyan3',
            'LightCyan4', 'PaleTurquoise1', 'PaleTurquoise2', 'PaleTurquoise3',
            'PaleTurquoise4', 'CadetBlue1', 'CadetBlue2', 'CadetBlue3',
            'CadetBlue4', 'turquoise1', 'turquoise2', 'turquoise3',
            'turquoise4', 'cyan2', 'cyan3', 'cyan4', 'DarkSlateGray1',
            'DarkSlateGray2', 'DarkSlateGray3', 'DarkSlateGray4',
            'aquamarine2', 'aquamarine4', 'DarkSeaGreen1', 'DarkSeaGreen2',
            'DarkSeaGreen3', 'DarkSeaGreen4', 'SeaGreen1', 'SeaGreen2',
            'SeaGreen3', 'PaleGreen1', 'PaleGreen2', 'PaleGreen3',
            'PaleGreen4', 'SpringGreen2', 'SpringGreen3', 'SpringGreen4',
            'green2', 'green3', 'green4', 'chartreuse2', 'chartreuse3',
            'chartreuse4', 'OliveDrab1', 'OliveDrab2', 'OliveDrab4',
            'DarkOliveGreen1', 'DarkOliveGreen2', 'DarkOliveGreen3',
            'DarkOliveGreen4', 'khaki1', 'khaki2', 'khaki3', 'khaki4',
            'LightGoldenrod1', 'LightGoldenrod2', 'LightGoldenrod3',
            'LightGoldenrod4', 'LightYellow2', 'LightYellow3', 'LightYellow4',
            'yellow2', 'yellow3', 'yellow4', 'gold2', 'gold3', 'gold4',
            'goldenrod1', 'goldenrod2', 'goldenrod3', 'goldenrod4',
            'DarkGoldenrod1', 'DarkGoldenrod2', 'DarkGoldenrod3',
            'DarkGoldenrod4', 'RosyBrown1', 'RosyBrown2', 'RosyBrown3',
            'RosyBrown4', 'IndianRed1', 'IndianRed2', 'IndianRed3',
            'IndianRed4', 'sienna1', 'sienna2', 'sienna3', 'sienna4',
            'burlywood1', 'burlywood2', 'burlywood3', 'burlywood4', 'wheat1',
            'wheat2', 'wheat3', 'wheat4', 'tan1', 'tan2', 'tan4', 'chocolate1',
            'chocolate2', 'chocolate3', 'firebrick1', 'firebrick2',
            'firebrick3', 'firebrick4', 'brown1', 'brown2', 'brown3', 'brown4',
            'salmon1', 'salmon2', 'salmon3', 'salmon4', 'LightSalmon2',
            'LightSalmon3', 'LightSalmon4', 'orange2', 'orange3', 'orange4',
            'DarkOrange1', 'DarkOrange2', 'DarkOrange3', 'DarkOrange4',
            'coral1', 'coral2', 'coral3', 'coral4', 'tomato2', 'tomato3',
            'tomato4', 'OrangeRed2', 'OrangeRed3', 'OrangeRed4', 'red2',
            'red3', 'red4', 'DeepPink2', 'DeepPink3', 'DeepPink4', 'HotPink1',
            'HotPink2', 'HotPink3', 'HotPink4', 'pink1', 'pink2', 'pink3',
            'pink4', 'LightPink1', 'LightPink2', 'LightPink3', 'LightPink4',
            'PaleVioletRed1', 'PaleVioletRed2', 'PaleVioletRed3',
            'PaleVioletRed4', 'maroon1', 'maroon2', 'maroon3', 'maroon4',
            'VioletRed1', 'VioletRed2', 'VioletRed3', 'VioletRed4', 'magenta2',
            'magenta3', 'magenta4', 'orchid1', 'orchid2', 'orchid3', 'orchid4',
            'plum1', 'plum2', 'plum3', 'plum4', 'MediumOrchid1',
            'MediumOrchid2', 'MediumOrchid3', 'MediumOrchid4', 'DarkOrchid1',
            'DarkOrchid2', 'DarkOrchid3', 'DarkOrchid4', 'purple1', 'purple2',
            'purple3', 'purple4', 'MediumPurple1', 'MediumPurple2',
            'MediumPurple3', 'MediumPurple4', 'thistle1', 'thistle2',
            'thistle3', 'thistle4', 'gray1', 'gray2', 'gray3', 'gray4',
            'gray5', 'gray6', 'gray7', 'gray8', 'gray9', 'gray10', 'gray11',
            'gray12', 'gray13', 'gray14', 'gray15', 'gray16', 'gray17',
            'gray18', 'gray19', 'gray20', 'gray21', 'gray22', 'gray23',
            'gray24', 'gray25', 'gray26', 'gray27', 'gray28', 'gray29',
            'gray30', 'gray31', 'gray32', 'gray33', 'gray34', 'gray35',
            'gray36', 'gray37', 'gray38', 'gray39', 'gray40', 'gray42',
            'gray43', 'gray44', 'gray45', 'gray46', 'gray47', 'gray48',
            'gray49', 'gray50', 'gray51', 'gray52', 'gray53', 'gray54',
            'gray55', 'gray56', 'gray57', 'gray58', 'gray59', 'gray60',
            'gray61', 'gray62', 'gray63', 'gray64', 'gray65', 'gray66',
            'gray67', 'gray68', 'gray69', 'gray70', 'gray71', 'gray72',
            'gray73', 'gray74', 'gray75', 'gray76', 'gray77', 'gray78',
            'gray79', 'gray80', 'gray81', 'gray82', 'gray83', 'gray84',
            'gray85', 'gray86', 'gray87', 'gray88', 'gray89', 'gray90',
            'gray91', 'gray92', 'gray93', 'gray94', 'gray95', 'gray97',
            'gray98', 'gray99'
        ]
        sg.theme(self.random(theme))

        m_TextSize = {"x": 12, "y": 1}
        m_InputSize = {"x": 37, "y": 1}
        m_Pading = {"l": 10, "r": 10, "u": 5, "d": 10}
        layout = [
            # 游戏名称      'GameName'
            # 市场价格      'MarketPrice'
            # 购买备注  'BuyInRemarks'
            # 修改仓库容量  'GameStockSizeWrite'

            # 交易码        'BuyInCode'
            # 交易时间  'BuyInTime'
            # 回收价格  'BuyInPrice'
            # 游戏库存      'GameStock'
            # 读取仓库容量  'GameStockSizeRead'
            [sg.Button('m_总览',  size=(m_InputSize["x"] + \
                       m_TextSize["x"], m_InputSize["y"]))],

            [sg.Text('游戏名称', pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(
                m_TextSize["x"], m_TextSize["y"])), sg.InputText(default_text='游戏名称', key='-GAMENAME-', size=(m_InputSize["x"], m_InputSize["y"]))],

            [sg.Text('市场价格', pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(
                m_TextSize["x"], m_TextSize["y"])), sg.InputText(default_text=300, key='-MARKETPRICE-', size=(m_InputSize["x"], m_InputSize["y"]))],

            [sg.Text('成本利润(0.x)', pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(
                m_TextSize["x"], m_TextSize["y"])), sg.InputText(default_text=0.3, key='-COSTPROFIT-', size=(m_InputSize["x"], m_InputSize["y"]))],

            [sg.Button('m_报价',  size=(m_InputSize["x"] + \
                       m_TextSize["x"], m_InputSize["y"]))],




            [sg.Text('修改仓库容量', pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(
                m_TextSize["x"], m_TextSize["y"])), sg.InputText(default_text=5, key='-GAMESTOCKSIZEWRITE-', size=(m_InputSize["x"], m_InputSize["y"]))],




            [sg.Button('m_增加/修改',  size=(m_InputSize["x"] + \
                       m_TextSize["x"], m_InputSize["y"]))],

            [sg.Text('回收备注', pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(m_TextSize["x"], m_TextSize["y"])),
             sg.InputText(default_text='客户备注', key='-BUYINREMARKS-', size=(m_InputSize["x"], m_InputSize["y"]))],

            [sg.Text('买入件数', pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(
                m_TextSize["x"], m_TextSize["y"])), sg.InputText(default_text=1, key='-GAMESNUM-', size=(m_InputSize["x"], m_InputSize["y"]))],


            [sg.Button('m_买入',  size=(m_InputSize["x"] + \
                       m_TextSize["x"], m_InputSize["y"]))],





            [sg.Text('交易码', pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(
                m_TextSize["x"], m_TextSize["y"])), sg.InputText(default_text='未生成', key='-BUYINCODE-', size=(m_InputSize["x"], m_InputSize["y"]))],

            [sg.Text('交易时间', pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(
                m_TextSize["x"], m_TextSize["y"])), sg.InputText(default_text='未生成', key='-BUYINTIME-', size=(m_InputSize["x"], m_InputSize["y"]))],

            [sg.Button('m_查找',  size=(m_InputSize["x"] + \
                       m_TextSize["x"], m_InputSize["y"]))],

            [sg.Button('m_卖出',  size=(m_InputSize["x"] + \
                       m_TextSize["x"], m_InputSize["y"]))],
            [sg.Button('m_清屏',  size=(m_InputSize["x"] + \
                       m_TextSize["x"], m_InputSize["y"]))],


            [sg.Text('回收价格', pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(
                m_TextSize["x"], m_TextSize["y"])), sg.InputText(default_text='未生成', key='-BUYINPRICE-', size=(m_InputSize["x"], m_InputSize["y"]))],

            [sg.Text('现有库存', pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(
                m_TextSize["x"], m_TextSize["y"])), sg.InputText(default_text='未生成', key='-GAMESTOCK-', size=(m_InputSize["x"], m_InputSize["y"]))],

            [sg.Text('读取仓库容量', pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(m_TextSize["x"], m_TextSize["y"])),
             sg.InputText(default_text='未生成', key='-GAMESTOCKSIZEREAD-', size=(m_InputSize["x"], m_InputSize["y"]))],

            [sg.Text('终端', pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(m_TextSize["x"], m_TextSize["y"]+6)),
             sg.Multiline(default_text='终端运行正常：\n', key='-TERMINAL-', size=(m_InputSize["x"], m_InputSize["y"]+10))],

            [sg.Exit('退出', button_color=(self.random(colors), self.random(colors)), pad=((m_Pading["l"], m_Pading["r"]), (m_Pading["u"], m_Pading["d"])), size=(
                m_InputSize["x"]+m_TextSize["x"], m_InputSize["y"]+1))]
        ]

        window = sg.Window('买入数据库图形界面', layout)

        while True:
            event, values = window.read(timeout=0)

            if event == sg.WIN_CLOSED or event == '退出':
                m_JRAW.clear('关闭前的操作数据\n' + terminal)
                break
            if event == 'm_总览':
                strT = m_JRAW.InitAllJson()
                self.terminal(strT, window, values)

            if event == 'm_买入':
                m_JRAW.add(stockPath, path, str(values['-GAMENAME-']),
                           int(values['-MARKETPRICE-']),
                           values['-BUYINREMARKS-'],
                           int(values['-GAMESTOCKSIZEWRITE-']),
                           float(values['-COSTPROFIT-']), window, values)

                print("--m_买入--")

            if event == 'm_卖出':
                m_JRAWfc = JsonReadAndWrite()
                m_JRAWfc.uuid = values['-BUYINCODE-']
                m_JRAWfc.GameName = values['-GAMENAME-']
                strT = m_JRAWfc.sell()
                self.terminal(strT, window, values)
                # 删除
                # 创建
                print("m_卖出程序执行成功")

            if event == 'm_增加/修改':
                m_JRAW.modify(stockPath, values['-GAMENAME-'],
                              int(values['-GAMESTOCKSIZEWRITE-']), window,
                              values)

            if event == 'm_查找':
                result = m_JRAW.find(path, values['-GAMENAME-'],
                                     values['-BUYINREMARKS-'])
                m_str = ""

                for r in result:
                    m_str = m_str + r + "\n"

                self.terminal(m_str, window, values)

            if event == 'm_报价':
                # 录入库存：

                GameName = values['-GAMENAME-']
                StockSize = values['-GAMESTOCKSIZEWRITE-']

                m_P2 = ProfitAlgorithm()
                m_P2.sellPrice = int(values['-MARKETPRICE-'])
                m_P2.hodingNumber = 0
                m_P2.hodingWant = int(StockSize)
                m_P2.costProfitWant = float(values['-COSTPROFIT-'])
                Buyprice = m_P2.costProfitGetPrice()

                m_J2 = JsonReadAndWrite()
                m_J2.GameName = GameName
                m_J2.StockPath = stockPath
                m_J2.BuyInPrice = Buyprice
                m_J2.SellForPrice = int(values['-MARKETPRICE-'])
                m_J2.StockSize = StockSize
                m_J2.addInStock()

                # 报价显示

                m_Jj = JsonReadAndWrite()
                m_Jj.GameName = values['-GAMENAME-']

                if m_Jj.check() == 0:

                    m_str = str(values['-GAMENAME-']) + "未在仓库中赋值"
                    m_str += " 已创建案例"
                    self.terminal(m_str, window, values)
                else:
                    m_P = ProfitAlgorithm()
                    m_P.sellPrice = int(values['-MARKETPRICE-'])
                    m_J = JsonReadAndWrite("", 0)
                    m_J.GameName = str(values['-GAMENAME-'])
                    m_P.hodingNumber = m_J.check()["库存"]
                    m_P.hodingWant = m_J.check()["仓库容量"]
                    m_P.costProfitWant = float(values['-COSTPROFIT-'])
                    buyprice = m_P.costProfitGetPrice()

                    window['-BUYINPRICE-'].update(buyprice)
                    window['-GAMESTOCK-'].update(m_J.check()["库存"])
                    window['-GAMESTOCKSIZEREAD-'].update(m_J.check()["仓库容量"])

                    m_str = "执行关于：" + str(
                        values['-GAMENAME-']) + " 的价格查询\n" + "报价为：" + str(
                            m_P.costProfitGetPrice()) + "元 \n利润率设置：" + str(
                                values['-COSTPROFIT-']) + "\n库存容量：" + str(
                                    m_J.check()["仓库容量"]) + "\n当前库存" + str(
                                        m_J.check()["库存"])
                    self.terminal(m_str, window, values)

                print("m_报价")

            if event == 'm_总览':
                print("退出程序7")

            if event == 'm_清屏':
                m_JRAW.clear(values['-TERMINAL-'])
                self.terminal("终端运行正常：\n", window, values)
                terminal = '清屏前的操作数据\n' + values['-TERMINAL-']

            terminal = values['-TERMINAL-']
        window.close()

    def random(self, array):
        randomNum = random.randint(0, len(array) - 1)
        return array[randomNum]

    def terminal(self, strIn, window, values):
        if strIn == "终端运行正常：\n":

            window['-TERMINAL-'].update(strIn)

            Stock_dataBase['terminal_Number'] = 0
            window.Refresh()

        else:
            Stock_dataBase['terminal_Number'] += 1
            terminalOut = values['-TERMINAL-'] + \
                strIn + "\n-----↑ 第" + \
                str(Stock_dataBase['terminal_Number']) + "操作输出 ↑-----\n"
            window['-TERMINAL-'].update(terminalOut)


# 利润算法 ↓


class ProfitAlgorithm:
    # sellPrice 出售价格
    # hodingNumber 持有数量
    # hodingWant  最大库存
    # costProfitWant 期待收入率
    def __init__(self=None,
                 sellPrice=None,
                 hodingNumber=None,
                 hodingWant=None,
                 costProfitWant=None):
        self.sellPrice = sellPrice
        self.hodingNumber = hodingNumber
        self.hodingWant = hodingWant
        self.costProfitWant = costProfitWant

    # 根据成本利润计算定价
    # 输入：卖出价格，持有商品数量，预计持有商品数量，期待成本利润
    # 输出：购买单个商品的价格

    def costProfitGetPrice(self):
        # 公式： 利润率 = 售价-进价/售价
        # 化简后得出：
        p = self.hodingWantCount()
        buyPrice = self.sellPrice - self.costProfitWant * self.sellPrice
        buyPrice = buyPrice - buyPrice * p
        if (buyPrice <= 0):
            # print("不再收购")
            return "不再收购"
        else:
            # print("收购价格：" + str(buyPrice))
            return buyPrice

    def hodingWantCount(self):
        p = self.hodingNumber / self.hodingWant
        return p


# Json数据读写 ↓


class JsonReadAndWrite:
    def __init__(self=None, GameName=None, StockSize=None, StockPath=None, BuyInPrice=None, SellForPrice=None, uuid=None, window=None, values=None, **BuyInRemarks):
        self.GameName = GameName
        self.StockSize = StockSize
        self.StockPath = StockPath
        self.BuyInPrice = BuyInPrice
        self.SellForPrice = SellForPrice
        self.uuid = uuid
        self.window = window
        self.values = values
        for BuyInRemarkTitle, BuyInRemarkValue in BuyInRemarks.items():
            setattr(self, BuyInRemarkTitle, BuyInRemarkValue)

        print('初始化成功 买入系统已经录入')

    # 关于仓库的操作 ↓
    def buyJsonStockSize(self):
        # ----↓ 写入数据 ↓-----
        Time = str(datetime.now())[:-7]
        m_GameName = self.GameName
        m_StockSize = self.StockSize
        m_BuyInRemarks = "1"
        m_BuyInPrice = 3
        if len(m_BuyInRemarks) > 0:
            m_BuyInRemarks
        else:
            m_BuyInRemarks = "none"

        # ----↑ 写入数据 ↑-----
        # data = {}
        # data['Game1'] = []
        # data['Game1'].append(
        #     {
        #         # 游戏名称
        #         # 交易码
        #         # 游戏交易时间
        #         # 游戏购买价格
        #         # 游戏购买备注
        #         # 游戏库存
        #         # 游戏仓库容量
        #         'GameName': m_GameName,
        #         'BuyInCode': UUID,
        #         'BuyInTime': Time,
        #         'BuyInPrice': m_BuyInPrice,
        #         'BuyInRemarks': m_BuyInRemarks,
        #         'GameStock': 1,
        #         'GameStockSize': 10

        #     }
        # )

        if os.access(stockPath, os.F_OK):
            g = True
            load_dict_stock = {}
            with open(stockPath, 'r', encoding='utf-8') as load_f:
                load_dict_stock = json.load(load_f)
                for i in load_dict_stock:

                    if i["游戏名称"] == str(m_GameName):
                        i['库存'] = i['库存'] + 1
                        i['最后更新时间'] = Time
                        i["仓库容量"] = m_StockSize
                        g = True
                        break
                    else:
                        g = False
            with open('GamesBuyStock.json', 'w', encoding='utf-8') as outfile:
                json.dump(load_dict_stock,
                          outfile,
                          ensure_ascii=False,
                          indent=4)

            if g == False:
                with open('GamesBuyStock.json', 'w',
                          encoding='utf-8') as outfile:
                    Stock_Append_default["游戏名称"] = str(m_GameName)
                    Stock_Append_default["库存"] = 1
                    Stock_Append_default["最后更新时间"] = Time
                    Stock_Append_default["仓库容量"] = m_StockSize
                    load_dict_stock.append(Stock_Append_default)
                    json.dump(load_dict_stock,
                              outfile,
                              ensure_ascii=False,
                              indent=4)
                    g = True

            # with open('GamesBuyStock.json', 'w', encoding='utf-8') as outfile:

            #     Stock_Append_default["游戏名称"] = m_GameName
            #     Stock_Append_default["库存"] = Stock_default[0]["库存"] + 1
            #     Stock_Append_default["最后更新时间"] = Time
            #     load_dict_stock = Stock_Append_default
            #     json.dump(load_dict_stock, outfile,
            #               ensure_ascii=False, indent=4)
        else:
            with open('GamesBuyStock.json', 'w+', encoding='utf-8') as outfile:
                Stock_default[0]["游戏名称"] = m_GameName
                Stock_default[0]["库存"] = Stock_default[0]["库存"] + 1
                Stock_default[0]["最后更新时间"] = Time
                Stock_default[0]["仓库容量"] = m_StockSize
                json.dump(Stock_default, outfile, ensure_ascii=False, indent=4)

    def creatUUID(self):
        # 基于时间产生UUID ↓
        m_uuid = str(uuid.uuid1())
        return m_uuid

    # '增加' 入口 ↓
    @staticmethod
    def add(stockPath, path, GameName, MarketPrice, BuyInRemarks,
            GameStockSizeWrite, CostProfit, window, values):
        # 更新库存信息 ↓
        JRW = JsonReadAndWrite(GameName, GameStockSizeWrite)
        JRW.buyJsonStockSize()
        gui = GUI()
        uuid = ""
        terminalOut = ""
        terminalOut1 = ""
        hodingNumber = 0

        if os.access(path, os.F_OK) & os.access(stockPath, os.F_OK):
            # 类型库存数据 ↓
            with open(stockPath, 'r', encoding='utf-8') as load_f0:
                load_dict0 = json.load(load_f0)
                for i in load_dict0:
                    if i["游戏名称"] == GameName:
                        hodingNumber = i["库存"]

            # 单个库存数据
            with open(path, 'r', encoding='utf-8') as load_f:
                load_dict = json.load(load_f)

            with open(path, 'w', encoding='utf-8') as dump_f:
                uuid = JRW.creatUUID()
                dire_default["游戏名称"] = GameName
                dire_default["市场价格"] = MarketPrice
                dire_default["购买备注"] = BuyInRemarks
                dire_default["交易码"] = uuid
                dire_default["交易时间"] = str(datetime.now())[:-7]
                dire_default["成本利润(0.x)"] = CostProfit

                m_P2 = ProfitAlgorithm()
                m_P2.sellPrice = int(values['-MARKETPRICE-'])
                m_P2.hodingNumber = int(hodingNumber)
                m_P2.hodingWant = int(values['-GAMESTOCKSIZEWRITE-'])
                m_P2.costProfitWant = float(values['-COSTPROFIT-'])
                Buyprice = m_P2.costProfitGetPrice()

                dire_default["回收价格"] = Buyprice
                dire_default["仓库容量"] = int(values['-GAMESTOCKSIZEWRITE-'])

                # dire_default["修改仓库容量"] = GameStockSizeWrite
                load_dict.append(dire_default)
                json.dump(load_dict, dump_f, ensure_ascii=False, indent=4)
            # 生成的信息 ↓
            with open(path, 'r', encoding='utf-8') as load_f:
                load_dict = json.load(load_f)
                for i in load_dict:
                    if i["交易码"] == uuid:
                        print(i["交易时间"])
                        print(values['-BUYINCODE-'])
                        # 更新到界面 ↓
                        window['-BUYINCODE-'].update(i["交易码"])
                        window['-BUYINTIME-'].update(i["交易时间"])
                        terminalOut = "添加库存成功：\n \r\r" + \
                            "游戏名称:"+i["游戏名称"] + \
                            "\n \r\r添加时间："+i["交易时间"]+"\n "

                        break
            with open(stockPath, 'r', encoding='utf-8') as load_f:
                load_dict = json.load(load_f)
                for i in load_dict:
                    if i["游戏名称"] == GameName:
                        terminalOut1 = "游戏现有库存： " + \
                            str(i["库存"]) + "\n游戏仓库容量" + str(i["仓库容量"])

            gui.terminal(terminalOut + terminalOut1, window, values)
        else:
            # 创建库存表单 ↓
            with open(path, 'w+', encoding='utf-8') as dump_f:
                uuid = JRW.creatUUID()
                list_default[0]["游戏名称"] = GameName
                list_default[0]["市场价格"] = MarketPrice
                list_default[0]["购买备注"] = BuyInRemarks
                list_default[0]["交易码"] = uuid
                list_default[0]["交易时间"] = str(datetime.now())[:-7]
                list_default[0]["成本利润(0.x)"] = CostProfit
                # list_default[0]["修改仓库容量"] = GameStockSizeWrite
                json.dump(list_default, dump_f, ensure_ascii=False, indent=4)
            # 生成的信息 ↓
            with open(path, 'r', encoding='utf-8') as load_f:
                load_dict = json.load(load_f)
                for i in load_dict:
                    if i["交易码"] == uuid:
                        print(i["交易时间"])
                        print(values['-BUYINCODE-'])
                        # 更新到界面 ↓
                        window['-BUYINCODE-'].update(i["交易码"])
                        window['-BUYINTIME-'].update(i["交易时间"])
                        terminalOut = "添加库存成功：\n \r\r" + \
                            "游戏名称:"+i["游戏名称"] + \
                            "\n \r\r添加时间："+i["交易时间"]+"\n "

                        break
            with open(stockPath, 'r', encoding='utf-8') as load_f:
                load_dict = json.load(load_f)
                for i in load_dict:
                    if i["游戏名称"] == GameName:
                        terminalOut1 = "游戏现有库存： " + \
                            str(i["库存"]) + "\n游戏仓库容量" + str(i["仓库容量"])
            gui.terminal(terminalOut + terminalOut1, window, values)

    def addInStock(self):
        exist = True
        if os.access(self.StockPath, os.F_OK):
            with open(self.StockPath, 'r', encoding='utf-8') as load_f:
                load_dict = json.load(load_f)
            for i in load_dict:
                print(i)
                if i["游戏名称"] == self.GameName:
                    exist = True
                    i["卖出价格"] = self.SellForPrice
                    i["买入价格"] = self.BuyInPrice
                    i["仓库容量"] = int(self.StockSize)
                    i["最后更新时间"] = str(datetime.now())[:-7]

                    # return(str(self.GameName)+"存在游戏库")
                else:
                    exist = False
            with open(stockPath, 'w', encoding='utf-8') as outfile:
                json.dump(load_dict,
                          outfile,
                          ensure_ascii=False,
                          indent=4)

            if exist == False:
                with open(self.StockPath, 'w', encoding='utf-8') as dump_f:
                    Stock_Append_default["游戏名称"] = self.GameName
                    Stock_Append_default["买入价格"] = self.BuyInPrice
                    Stock_Append_default["卖出价格"] = self. SellForPrice
                    Stock_Append_default["最后更新时间"] = str(datetime.now())[:-7]
                    Stock_Append_default["库存"] = 0
                    Stock_Append_default["仓库容量"] = int(self.StockSize)
                    # dire_default["修改仓库容量"] = GameStockSizeWrite
                    load_dict.append(Stock_Append_default)
                    json.dump(load_dict, dump_f,
                              ensure_ascii=False, indent=4)
                    exist = True
        else:
            # 创建表单
            with open(stockPath, 'w+', encoding='utf-8') as dump_f:
                Stock_default[0]["游戏名称"] = self.GameName
                Stock_default[0]["买入价格"] = self.BuyInPrice
                Stock_default[0]["卖出价格"] = self.SellForPrice
                Stock_default[0]["最后更新时间"] = str(datetime.now())[:-7]
                Stock_default[0]["库存"] = 0
                Stock_default[0]["仓库容量"] = int(self.StockSize)
                # list_default[0]["修改仓库容量"] = GameStockSizeWrite
                json.dump(Stock_default, dump_f, ensure_ascii=False, indent=4)

        print("已加入库存")
    # '查找' 入口 ↓

    @staticmethod
    def find(path, GameName, UUID):
        if os.access(path, os.F_OK):
            v = []

            with open(path, 'r', encoding='utf-8') as load_f:
                load_dict = json.load(load_f)

                for i in load_dict:

                    if i["交易码"] == UUID:
                        exists = "交易码对应的游戏名称:" in v
                        if exists == False:
                            v.append("交易码对应的游戏名称:")
                            v.append(i['游戏名称'] + "\n交易时间： " + i['交易时间'])
                            return v

                        else:
                            v.append("交易码碰撞请核实交易码：" + i["交易码"])
                            return v

                time.sleep(0.1)

                for i in load_dict:

                    if i["游戏名称"] == GameName:

                        existss = "游戏名称对应的交易码:" in v
                        if existss == False:
                            v.append("游戏名称对应的交易码:")
                            v.append(i['交易码'])
                        else:
                            v.append(i['交易码'])

                return v

        else:
            return '文件未创建'

    # '清屏'入口 ↓
    @staticmethod
    def clear(LogData):
        fileName = datetime.now().strftime("%Y_%m_%d-%I_%M_%S_%p") + '_log.txt'
        path = '/logs/'
        logPath = os.path.join(root + path, fileName)
        with open(logPath, 'w+', encoding='utf-8') as outfile:
            outfile.write(LogData)
        # 导出操作数据
        print("clear")

    # '修改'入口 ↓
    @staticmethod
    def modify(stockPath, GameName, maxStockChange, window, values):
        gui = GUI()
        # 修改仓库容量
        if os.access(stockPath, os.F_OK):
            with open(stockPath, 'r', encoding="utf-8") as load_f:
                load_dict = json.load(load_f)
                for i in load_dict:
                    if i["游戏名称"] == GameName:
                        i["仓库容量"] = maxStockChange
            with open(stockPath, 'w', encoding="utf-8") as dump_f:
                json.dump(load_dict, dump_f, ensure_ascii=False, indent=4)
            terminalOut = GameName + ": \n仓库容量已修改为：" + str(maxStockChange)
            gui.terminal(terminalOut, window, values)
        else:
            print("未找到文件")

    def check(self):
        res = {}
        res["库存"] = 0
        res["仓库容量"] = 0
        if os.access(stockPath, os.F_OK):
            with open(stockPath, 'r', encoding="utf-8") as load_f:
                load_dict = json.load(load_f)

                for i in load_dict:

                    if i["游戏名称"] == str(self.GameName):
                        res["库存"] = i["库存"]

                        if i["仓库容量"] != 0:
                            res["仓库容量"] = i["仓库容量"]
                            print("库存 " + str(res["库存"]) +
                                  "\n仓库容量 " + str(res["仓库容量"]))

                            return res
                        else:
                            print(str(self.GameName) + "：仓库容量 还未赋值")

        else:
            print("请先创建 GamesBuyStock.json 表单")
            return 0

    def sell(self):
        sell_gameName = ""
        sell_price = 0
        sell_buy_price = 0
        sell_buy_time = ""
        g = False

        if os.access(path, os.F_OK) & os.access(stockPath, os.F_OK) & os.access(sellPath, os.F_OK):
            # 读取
            with open(path, 'r', encoding='utf-8') as load_f:
                load_dict0 = json.load(load_f)
                for i in load_dict0:
                    if i["交易码"] == self.uuid:
                        sell_gameName = i["游戏名称"]
                        sell_price = i["市场价格"]
                        sell_buy_price = i["回收价格"]
                        sell_buy_time = i["交易时间"]
                        g = True

                    else:
                        continue
                if g == False:
                    strT = str("系统没有关于:\n"
                               + self.GameName+" uuid:\n" +
                               self.uuid +
                               "\n的信息")
                    return strT

            if g == True:
                # 写入
                with open(sellPath, 'r', encoding='utf-8') as load_f:
                    load_dict = json.load(load_f)

                with open(sellPath, 'w', encoding='utf-8') as dump_f:

                    sell_Append_default["交易码"] = self.uuid
                    sell_Append_default["游戏名称"] = sell_gameName
                    sell_Append_default["买入价格"] = sell_buy_price
                    sell_Append_default["卖出价格"] = sell_price
                    sell_Append_default["卖出时间"] = str(datetime.now())[:-7]
                    sell_Append_default["买入时间"] = sell_buy_time

                    load_dict.append(sell_Append_default)
                    json.dump(load_dict, dump_f, ensure_ascii=False, indent=4)

                # 删除单个买入数据
                with open(path, 'r', encoding='utf-8') as load_f:
                    load_dict = json.load(load_f)
                    for i in load_dict:
                        if i["交易码"] == self.uuid:
                            load_dict.remove(i)
                with open(path, 'w', encoding='utf-8') as dump_f:
                    json.dump(load_dict, dump_f, ensure_ascii=False, indent=4)

                # 减去数据库库存
                with open(stockPath, 'r', encoding='utf-8') as load_ff:
                    load_fff = json.load(load_ff)
                    for i in load_fff:
                        if i["游戏名称"] == self.GameName:
                            i['库存'] = i['库存'] - 1
                            i['最后更新时间'] = str(datetime.now())[:-7]
                            break
                with open(stockPath, 'w', encoding='utf-8') as outfile:
                    json.dump(load_fff,
                              outfile,
                              ensure_ascii=False,
                              indent=4)
                strT = str("已经卖出：\n" + "uuid:\n" +
                           self.uuid+"\n游戏名称："+self.GameName)
                return strT

        else:

            # 读取
            with open(path, 'r', encoding='utf-8') as load_f:
                load_dict0 = json.load(load_f)
                for i in load_dict0:
                    if i["交易码"] == self.uuid:
                        sell_gameName = i["游戏名称"]
                        sell_price = i["市场价格"]
                        sell_buy_price = i["回收价格"]
                        sell_buy_time = i["交易时间"]
                        g = True

                    else:
                        continue
                if g == False:

                    print(str("系统没有关于:\n"
                              + self.GameName+" uuid:\n" +
                              self.uuid +
                              "\n的信息"))

            # 创建库存表单 ↓
            with open(sellPath, 'w+', encoding='utf-8') as dump_f:

                sell_default[0]["游戏名称"] = self.GameName
                sell_default[0]["交易码"] = self.uuid
                sell_default[0]["买入价格"] = sell_buy_price
                sell_default[0]["卖出价格"] = sell_price
                sell_default[0]["卖出时间"] = str(datetime.now())[:-7]
                sell_default[0]["买入时间"] = sell_buy_time

                json.dump(sell_default, dump_f, ensure_ascii=False, indent=4)
                strT = str("已经卖出：\n"
                           + "uuid:\n"+self.uuid+"游戏名称："+self.GameName)
                return strT

        print("卖出")

    # 单独创建对库存对操作

    def InitAllJson(self):
        gameNames = []
        stocks = []
        buyprice = []
        sellprice = []
        times = []
        if os.access(stockPath, os.F_OK) & os.access(sellPath, os.F_OK) & os.access(path, os.F_OK):
            # 生成总览
            with open(stockPath, 'r', encoding='utf-8') as load_f:
                load_dict_stock = json.load(load_f)
                for i in load_dict_stock:
                    gameNames.append(i["游戏名称"])
                    stocks.append(i["库存"])
                    buyprice.append(i["买入价格"])
                    sellprice.append(i["卖出价格"])
                    times.append(i["最后更新时间"])

                self.prettyTable(gameNames, stocks, buyprice, sellprice, times)
            return "总览表见编译器输出"

        else:
            # 创建
            with open(stockPath, 'w+', encoding='utf-8') as dump_f:
                Stock_default[0]["最后更新时间"] = str(datetime.now())[:-7]
                json.dump(Stock_default, dump_f, ensure_ascii=False, indent=4)
            with open(path, 'w+', encoding='utf-8') as dump_f:
                list_default[0]["交易时间"] = str(datetime.now())[:-7]
                json.dump(list_default, dump_f, ensure_ascii=False, indent=4)
            with open(sellPath, 'w+', encoding='utf-8') as dump_f:
                sell_default[0]["卖出时间"] = str(datetime.now())[:-7]
                json.dump(sell_default, dump_f, ensure_ascii=False, indent=4)
            return str("已生成基本json:\n"+stockPath+"+"+path+"+"+sellPath)

    def prettyTable(slef, gameNames, stocks, buyprice, sellprice, times):
        table = PrettyTable(['编号', '名称', '库存', '买入价格', '卖出价格', '更新时间'])

        for i in range(len(gameNames)):
            if i == 0:
                continue
            table.add_row([str(i), str(gameNames[i]), str(
                stocks[i]), str(buyprice[i]), str(sellprice[i]+5.5), str(times[i])])
        table2 = PrettyTable(['我的购买规则', '我的出售规则', '数据更新'])
        a = '私信 带壳寄送\n 48小时内打钱.\n第二次出售24小时打钱\n 寄送地址：\n 南京市雨花台区新亭大街66号 \n 朗诗绿色街区 \n10幢1单元701室 \n --两件包邮--\n 独立id 随时可查 欢迎咨询 \n 不在报价单的游戏也收购\n南京的开车上门取 \n当面给钱'
        b = '有货情况下 私信 地址\n 交两倍定金\n 第二次购买 或 熟人\n免定金 \n'
        c = '更新规则：\n 每5天清一次库存 \n 每天12点前更新报价\n熟人小刀 \n 小小副业'
        table2.add_row([str(a), str(b), str(c)])

        print(table)
        print(table2)
# 收购价格 ↓
# PA = ProfitAlgorithm(sellPrice=305, hodingNumber=0,
#                      hodingWant=10, costProfitWant=0.5)
# PA.costProfitGetPrice()
# 买入信息录入 ↓
# JRW = JsonReadAndWrite()
# JRW.buyJsonStockSize()
# GUI ↓


m_s = SystemTest()
m_s.test()

m_GUI = GUI()
m_GUI.show()
