import matplotlib.pyplot as plt
from PIL import Image
import numpy as np
from perlin_noise import PerlinNoise

xpix, ypix = 325, 325

# 39
# 39 24
# 24 12
# 12 0

#29
#29 9
#9 9


def determine_biome(temperature, humidity, evaporation):
    biomes = {
        '1' : [
            {'temperature':(0.37, 1), 'humidity':(0.39, 1), 'evaporation':(-1, -0.39)}
        ],
        '2' : [
            {'temperature':(0.37, 1), 'humidity':(0.24, 0.39), 'evaporation':(-0.39, -0.24)}
        ],
        '3' : [
            {'temperature':(0.37, 1), 'humidity':(0.12, 0.24), 'evaporation':(-0.24, -0.12)}
        ],
        '4' : [
            {'temperature':(0.37, 1), 'humidity':(0, 0.12), 'evaporation':(-0.12, 0)}
        ],
        '5' : [
            {'temperature':(0.37, 1), 'humidity':(-0.12, 0), 'evaporation':(0, 0.12)}
        ],
        '6' : [
            {'temperature':(0.37, 1), 'humidity':(-0.24, -0.12), 'evaporation':(0.12, 0.24)}
        ],
        '7' : [
            {'temperature':(0.37, 1), 'humidity':(-0.39, -0.24), 'evaporation':(0.24, 0.39)}
        ],
        '8' : [
            {'temperature':(0.37, 1), 'humidity':(-1, -0.39), 'evaporation':(0.39, 1)}
        ],
        # '9' : [
        #     {'temperature':(0.21, 0.37), 'humidity':(0.39, 1), 'evaporation':(-1, -0.39)}
        # ],
        '10' : [
            {'temperature':(0.21, 0.37), 'humidity':(0.24, 0.39), 'evaporation':(-0.39, -0.24)}
        ],
        '11' : [
            {'temperature':(0.21, 0.37), 'humidity':(0.12, 0.24), 'evaporation':(-0.24, -0.12)}
        ],
        '12' : [
            {'temperature':(0.21, 0.37), 'humidity':(0, 0.12), 'evaporation':(-0.12, 0)}
        ],
        '13' : [
            {'temperature':(0.21, 0.37), 'humidity':(-0.12, 0), 'evaporation':(0, 0.12)}
        ],
        '14' : [
            {'temperature':(0.21, 0.37), 'humidity':(-0.24, -0.12), 'evaporation':(0.12, 0.24)}
        ],
        '15' : [
            {'temperature':(0.21, 0.37), 'humidity':(-0.39, -0.24), 'evaporation':(0.24, 0.39)}
        ],
        '16' : [
            {'temperature':(0.21, 0.37), 'humidity':(-1, -0.39), 'evaporation':(0.39, 1)}
        ],
        # '17' : [
        #     {'temperature':(0.07, 0.21), 'humidity':(0.39, 1), 'evaporation':(-1, -0.39)}
        # ],
        '18' : [
            {'temperature':(0.07, 0.21), 'humidity':(0.24, 0.39), 'evaporation':(-0.39, -0.24)}
        ],
        '19' : [
            {'temperature':(0.07, 0.21), 'humidity':(0.12, 0.24), 'evaporation':(-0.24, -0.12)}
        ],
        '20' : [
            {'temperature':(0.07, 0.21), 'humidity':(0, 0.12), 'evaporation':(-0.12, 0)}
        ],
        '21' : [
            {'temperature':(0.07, 0.21), 'humidity':(-0.12, 0), 'evaporation':(0, 0.12)}
        ],
        '22' : [
            {'temperature':(0.07, 0.21), 'humidity':(-0.24, -0.12), 'evaporation':(0.12, 0.24)}
        ],
        '23' : [
            {'temperature':(0.07, 0.21), 'humidity':(-0.39, -0.24), 'evaporation':(0.24, 0.39)}
        ],
        '24' : [
            {'temperature':(0.07, 0.21), 'humidity':(-1, -0.39), 'evaporation':(0.39, 1)}
        ],
        # '25' : [
        #     {'temperature':(-0.07, 0.07), 'humidity':(0.39, 1), 'evaporation':(-1, -0.39)}
        # ],
        # '26' : [
        #     {'temperature':(-0.07, 0.07), 'humidity':(0.24, 0.39), 'evaporation':(-0.39, -0.24)}
        # # ],
        '27' : [
            {'temperature':(-0.07, 0.07), 'humidity':(0.12, 0.24), 'evaporation':(-0.24, -0.12)}
        ],
        '28' : [
            {'temperature':(-0.07, 0.07), 'humidity':(0, 0.12), 'evaporation':(-0.12, 0)}
        ],
        '29' : [
            {'temperature':(-0.07, 0.07), 'humidity':(-0.12, 0), 'evaporation':(0, 0.12)}
        ],
        '30' : [
            {'temperature':(-0.07, 0.07), 'humidity':(-0.24, -0.12), 'evaporation':(0.12, 0.24)}
        ],
        '31' : [
            {'temperature':(-0.07, 0.07), 'humidity':(-0.39, -0.24), 'evaporation':(0.24, 0.39)}
        ],
        '32' : [
            {'temperature':(-0.07, 0.07), 'humidity':(-1, -0.39), 'evaporation':(0.39, 1)}
        ],
        # '33' : [
        #     {'temperature':(0.07, 0.21), 'humidity':(0.39, 1), 'evaporation':(-1, -0.39)}
        # ],
        # '34' : [
        #     {'temperature':(0.07, 0.21), 'humidity':(0.24, 0.39), 'evaporation':(-0.39, -0.24)}
        # # ],
        # '35' : [
        #     {'temperature':(0.07, 0.21), 'humidity':(0.12, 0.24), 'evaporation':(-0.24, -0.12)}
        # ],
        '36' : [
            {'temperature':(0.07, 0.21), 'humidity':(0, 0.12), 'evaporation':(-0.12, 0)}
        ],
        '37' : [
            {'temperature':(0.07, 0.21), 'humidity':(-0.12, 0), 'evaporation':(0, 0.12)}
        ],
        '38' : [
            {'temperature':(0.07, 0.21), 'humidity':(-0.24, -0.12), 'evaporation':(0.12, 0.24)}
        ],
        '39' : [
            {'temperature':(0.07, 0.21), 'humidity':(-0.39, -0.24), 'evaporation':(0.24, 0.39)}
        ],
        '40' : [
            {'temperature':(0.07, 0.21), 'humidity':(-1, -0.39), 'evaporation':(0.39, 1)}
        ],
        # '41' : [
        #     {'temperature':(0.21, 0.37), 'humidity':(0.39, 1), 'evaporation':(-1, -0.39)}
        # ],
        # '42' : [
        #     {'temperature':(0.21, 0.37), 'humidity':(0.24, 0.39), 'evaporation':(-0.39, -0.24)}
        # # ],
        # '43' : [
        #     {'temperature':(0.21, 0.37), 'humidity':(0.12, 0.24), 'evaporation':(-0.24, -0.12)}
        # ],
        # '44' : [
        #     {'temperature':(0.21, 0.37), 'humidity':(0, 0.12), 'evaporation':(-0.12, 0)}
        # ],
        '45' : [
            {'temperature':(0.21, 0.37), 'humidity':(-0.12, 0), 'evaporation':(0, 0.12)}
        ],
        '46' : [
            {'temperature':(0.21, 0.37), 'humidity':(-0.24, -0.12), 'evaporation':(0.12, 0.24)}
        ],
        '47' : [
            {'temperature':(0.21, 0.37), 'humidity':(-0.39, -0.24), 'evaporation':(0.24, 0.39)}
        ],
        '48' : [
            {'temperature':(0.21, 0.37), 'humidity':(-1, -0.39), 'evaporation':(0.39, 1)}
        ],
        
        # '49' : [
        #     {'temperature':(0.37, 1), 'humidity':(0.39, 1), 'evaporation':(-1, -0.39)}
        # ],
        # '50' : [
        #     {'temperature':(0.37, 1), 'humidity':(0.24, 0.39), 'evaporation':(-0.39, -0.24)}
        # # ],
        # '51' : [
        #     {'temperature':(0.37, 1), 'humidity':(0.12, 0.24), 'evaporation':(-0.24, -0.12)}
        # ],
        # '52' : [
        #     {'temperature':(0.37, 1), 'humidity':(0, 0.12), 'evaporation':(-0.12, 0)}
        # ],
        # '53' : [
        #     {'temperature':(0.37, 1), 'humidity':(-0.12, 0), 'evaporation':(0, 0.12)}
        # ],
        '54' : [
            {'temperature':(0.37, 1), 'humidity':(-0.24, -0.12), 'evaporation':(0.12, 0.24)}
        ],
        '55' : [
            {'temperature':(0.37, 1), 'humidity':(-0.39, -0.24), 'evaporation':(0.24, 0.39)}
        ],
        '56' : [
            {'temperature':(0.37, 1), 'humidity':(-1, -0.39), 'evaporation':(0.39, 1)}
        ]

    #     'A' : [
    #     {'temperature':(-1, 0), 'humidity':(-1,-0.2), 'evaporation':(-1,1)},
    #     {'temperature':(-1, -0.2), 'humidity':(-0.2, 0), 'evaporation':(-1,1)}
    # ],
    #     'B' : [
    #         {'temperature':(0,1), 'humidity':(-1,-0.2), 'evaporation':(-1,1)},
    #         {'temperature':(0.2,1), 'humidity':(-0.2, 0), 'evaporation':(-1,1)}
    # ]
    }

    for biome, ranges in biomes.items():
        for range_set in ranges:
            temp_range = range_set['temperature']
            hum_range = range_set['humidity']
            eva_range = range_set['evaporation']
            if temp_range[0] <= temperature <= temp_range[1] and hum_range[0] <= humidity <= hum_range[1] and eva_range[0] <= evaporation <= eva_range[1]:
 
                return biome
    return 'None'

def multiply(x, multiplier=2):
    l = np.array(x)
    return l * multiplier

temperature_noise = PerlinNoise(octaves=8)
humidity_noise = PerlinNoise(octaves=8)
evaporation_noise = PerlinNoise(octaves=8)

temperature = [[temperature_noise([i/xpix, j/ypix]) for j in range(xpix)] for i in range(ypix)]
temperature = multiply(temperature, 1)

humidity = [[humidity_noise([i/xpix, j/ypix]) for j in range(xpix)] for i in range(ypix)]
humidity = multiply(humidity, 1)

evaporation = [[evaporation_noise([i/xpix, j/ypix]) for j in range(xpix)] for i in range(ypix)]
evaporation = multiply(evaporation, 1)

data = np.zeros((xpix, ypix, 3), dtype=np.uint8)

for y in range(ypix):
    for x in range(xpix):
        result = determine_biome(temperature[y][x], humidity[y][x], evaporation[y][x])
        r = [0, 0, 0]
        match result:
            case '1':
                r = [32, 255, 160]
            case '2':
                r = [64, 255, 144]
            case '3':
                r = [60, 255, 128]
            case '4':
                r = [128, 255, 128]
            case '5':
                r = [160, 255, 128]
            case '6':
                r = [192, 255, 128]
            case '7':
                r = [224, 255, 128]
            case '8':
                r = [255, 255, 160]
            case '10':
                r = [32, 240, 176]
            case '11':
                r = [64, 240, 144]
            case '12':
                r = [60, 240, 128]
            case '13':
                r = [128, 240, 128]
            case '14':
                r = [176, 240, 128]
            case '15':
                r = [208, 240, 128]
            case '16':
                r = [240, 255, 128]
            case '18':
                r = [32, 224, 192]
            case '19':
                r = [64, 224, 144]
            case '20':
                r = [60, 224, 128]
            case '21':
                r = [128, 224, 128]
            case '22':
                r = [160, 224, 128]
            case '23':
                r = [192, 224, 128]
            case '24':
                r = [224, 224, 128]
            case '27':
                r = [32, 192, 192]
            case '28':
                r = [64, 192, 144]
            case '29':
                r = [60, 192, 128]
            case '30':
                r = [128, 192, 128]
            case '31':
                r = [160, 192, 128]
            case '32':
                r = [192, 192, 128]
            case '36':
                r = [32, 160, 192]
            case '37':
                r = [64, 160, 144]
            case '38':
                r = [60, 160, 128]
            case '39':
                r = [128, 160, 128]
            case '40':
                r = [160, 160, 128]
            case '45':
                r = [32, 128, 192]
            case '46':
                r = [64, 128, 128]
            case '47':
                r = [60, 128, 128]
            case '48':
                r = [128, 128, 128]
            case '54':
                r = [255, 255, 255]
            case '55':
                r = [255, 255, 255]
            case '56':
                r = [192, 192, 160]
            case 'None':
                r = [0,0,0]

        data[y][x] = r
# match case
img = Image.fromarray(data, 'RGB')
img.show()

# heightnoise = PerlinNoise(octaves=10, seed=15)
# xpix, ypix = 325, 325

# height = [[heightnoise([i/xpix, j/ypix]) for j in range(xpix)] for i in range(ypix)]
# height = multiply(height, 1)
# data = np.zeros((xpix, ypix, 3), dtype=np.uint8)

# for y in range(ypix):
#     for x in range(xpix):
#         if height[y][x] < 0:
#             data[y][x] = [0,0,255]
#         elif height[y][x] > 0.6:
#             data[y][x] = [51, 51, 51]
#         else:
#             data[y][x] = [0, 255, 0]

# img = Image.fromarray(data, 'RGB')
# # img.show()

# plt.imshow(img, interpolation='nearest')
# plt.axis("off")
# plt.show()