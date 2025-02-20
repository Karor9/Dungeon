def load_and_sort_numbers(file_path):
    # Wczytanie liczb z pliku
    with open(file_path, 'r') as file:
        numbers = [float(line.strip().replace(',', '.')) for line in file]

    # Posortowanie liczb rosnąco
    numbers.sort()

    # Podział na podzbiory z nienakładającymi się zakresami
    n = len(numbers)
    k = 8  # Liczba podzbiorów
    subsets = [[] for _ in range(k)]

    # Obliczenie rozmiaru każdego podzbioru
    subset_size = n // k
    remainder = n % k

    # Podział liczb na podzbiory
    start_index = 0
    for i in range(k):
        end_index = start_index + subset_size + (1 if i < remainder else 0)
        subsets[i] = numbers[start_index:end_index]
        start_index = end_index

    return subsets

# Przykładowe użycie funkcji
file_path = 'C:/Users/gersk/Desktop/Gra/dungeon/Dungeon/ConceptArt/test2.txt'
subsets = load_and_sort_numbers(file_path)
for i, subset in enumerate(subsets):
    print(f"Podzbiór {i+1}: {subset[0]} {subset[-1]}")
