#!/usr/bin/env python3

import sys
import requests

sys.path.append('/usr/local/lib/python3.6/site-packages/pyst2-0.5.1.dist-info')

import asterisk
import asterisk.agi
import asterisk.manager
import asterisk.config

from asterisk.agi import AGI
from say_number import say_number1

# def say_number(number,agi):
#     try:
#          number_str = str(number)
#          for i in range(0, len(number_str), 2):
#             two_digit_part = number_str[i:i+2]
#             agi.verbose(f"two_digit_part=: {two_digit_part}", 1)
#             if len(two_digit_part) == 2 and two_digit_part[1] == "0":
#                 agi.verbose(f"two_digit_part=: {two_digit_part}", 1)
#                 part = int(two_digit_part)
#                 agi.stream_file(f"digits/{part}")
#             if len(two_digit_part) == 1:
#                 part = int(two_digit_part)
#                 agi.stream_file(f"digits/{part}")
#             if len(two_digit_part) == 2 and two_digit_part[1] != "0":
#                 agi.stream_file(f"digits/{two_digit_part[0:1]}0")
#                 agi.stream_file("extra/o")
#                 agi.stream_file(f"digits/{two_digit_part[1:2]}")

#     except Exception as e:
#         # Log the error to Asterisk CLI with verbosity level 1
#         agi.verbose(f"Error saying number in parts: {e}", 1)

def main():
    # Create an instance of AGI
    agi = AGI()
    url="http://85.215.181.244:8090/Ivr/login-mobile?mobile=09148905828"
    response = requests.get(url)
    
    if response.status_code == 200:
        data = response.json()  # Parse JSON response
    agi.verbose(f"response: {response}", level=1)
    agi.verbose(f"ayhan response: {data}", level=1)

    # Answer the call
    #agi.answer()
    
    # Play a sound file (e.g., "welcome")
    #agi.stream_file("custom/enter-password")  # Make sure the file exists in /var/lib/asterisk/sounds/custom
    
    # Set a variable in the Asterisk dialplan
    agi.set_variable("MY_VAR", data)
       #say_number_in_parts(agi, data)
    say_number1(data,agi)
    
    # Read an input from the caller (e.g., collect 4 digits)
    #digits = agi.get_data("custom/enter_digits", timeout=5000, max_digits=4)
    
    
    # Hang up the call
    #agi.hangup()

if __name__ == "__main__":
    main()
