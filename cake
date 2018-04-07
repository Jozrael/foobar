#!/usr/bin/env python

# First thought, recursive function, but don't think that's correct because it's not trivial to discard patterns early.
# With only 200 characters (so at most 18 divisors), probably don't really need to worry about algorithm efficiency overly much.
# Feels like the solution should be order N*(divisors of N), though admittedly with that maxing just over 3k (180*18) prob not worth it.

# Second thought - after a bit, think I got it. Keep a list of non-invalidated divisors of the length. Iterate 1 through n, stopping at
# multiples of divisors that haven't been invalidated yet. Vet that the last sequence still matches for each one with substraings. I like it.
# I don't THINK there's a better way than this without something way over my head with like...some weird memoization of other factor sequences.

from math import floor, sqrt

def answer(s):

    def get_divisors(n):
        divisors = dict()
        for x in range(1,n/2+1):
            if n%x == 0:
                divisors[x] = 0
        return divisors
    
    potential_divisors = get_divisors(len(s))

    for string_pos in range(0,len(s)):
        divisors_to_check = []
        for divisor in potential_divisors.keys():
            if (string_pos+1)%divisor == 0 and potential_divisors[divisor] != -1:
                divisors_to_check.append(divisor)

        for divisor in divisors_to_check:
            if string_pos+1 == divisor:
                potential_divisors[divisor] = s[0:string_pos+1]
            elif (string_pos+1)%divisor == 0 and (s[string_pos+1-divisor:string_pos+1] != potential_divisors[divisor]):
                potential_divisors[divisor] = -1
    
    for x in potential_divisors:
        if potential_divisors[x] != -1:
            return len(s)/x
    return -1

answer("abccbaabccba")
