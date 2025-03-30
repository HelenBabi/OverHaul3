#!/usr/bin/env python3

import sys
import requests

sys.path.append('/usr/local/lib/python3.6/site-packages/pyst2-0.5.1.dist-info')

import asterisk
import asterisk.agi
import asterisk.manager
import asterisk.config

from asterisk.agi import AGI


def say_number1(number):
    agi = AGI()
    try:
         number_str = str(number)
         for i in range(0, len(number_str), 2):
            two_digit_part = number_str[i:i+2]
            agi.verbose(f"two_digit_part=: {two_digit_part}", 1)
            if len(two_digit_part) == 2 and two_digit_part[1] == "0":
                agi.verbose(f"two_digit_part=: {two_digit_part}", 1)
                part = int(two_digit_part)
                agi.stream_file(f"digits/{part}")
            if len(two_digit_part) == 1:
                part = int(two_digit_part)
                agi.stream_file(f"digits/{part}")
            if len(two_digit_part) == 2 and two_digit_part[1] != "0":
                agi.stream_file(f"digits/{two_digit_part[0:1]}0")
                agi.stream_file("extra/o")
                agi.stream_file(f"digits/{two_digit_part[1:2]}")

    except Exception as e:
        agi.verbose(f"Error saying number in parts: {e}", 1)


if __name__ == "__main__":
    say_number1()
