import numpy as np

def min_max_normalization(data):
    """
    对输入数据进行最小-最大归一化
    :param data: 输入数据，类型为numpy数组
    :return: 归一化后的数据
    """
    data_min = np.min(data)
    data_max = np.max(data)
    
    # 归一化公式
    normalized_data = (data - data_min) / (data_max - data_min)
    normalized_data = np.around(normalized_data,3)
    return normalized_data

def main():
    # 示例数据
    data = np.array([1794, 444910, 6, 221010, 222746,2,2,2,2,165623])
    
    print("原始数据:", data)
    
    normalized_data = min_max_normalization(data)
    
    print("归一化后的数据:", normalized_data)

if __name__ == "__main__":
    main()