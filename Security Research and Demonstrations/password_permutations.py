from itertools import product

password_list = "output.txt"

words = ['cricket', 'football', 'netball', 'soccer'] #adding longer words or more symbols will grow exponentially. 2^len(word)
num_start = 2020 #inclusive
num_end = 2026 #inclusive
special_chars = ['!', '@', '#', '$'] 

numbers = [str(i) for i in range(num_start,num_end+1)]

def capitalisation_permutations(word):
    for pattern in product([0, 1], repeat=len(word)):
        yield ''.join(
            c.upper() if b else c.lower()
            for c, b in zip(word, pattern)
        )

with open(password_list, "w") as file:
    for word in words:
        for variant in capitalisation_permutations(word):
            for year in numbers:
                for symbol in special_chars:
                    file.write(f"{variant}{year}{symbol}\n")


# Author: DeonCyberAnalyst
# Generates a customisable password wordlist by combining words with every capitalisation permutation, year range, and special character, then saves the results to output.txt.
