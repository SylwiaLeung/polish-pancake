from random import choice
from datetime import datetime
import time


def turn_file_to_dict(file):
    """this reads lines from file, turns them into a dictionary and returns the dict"""
    capital_dict = {}
    with open(file) as f:
        for line in f:
            key, value = line.split("|")
            capital_dict[key.strip()] = value.strip()
    return capital_dict


def get_random_word(func):
    """this takes a function as an argument to get the country-capital dictionary and return a random pair as list"""
    word_dict = func
    word_pair = choice(list(word_dict.items()))
    return list(word_pair)


def display_hangman(num):
    """this returns a stage of hangman corresponding to the num of lives left"""
    stages = ["""
                    ------------
                    |          |
                    |          0
                    |         \\|/
                    |          |
                    |         / \\
                    |
                    ------------

            """,
              """
                    ------------
                    |          |
                    |          0
                    |         \\|/
                    |          |
                    |         
                    |
                    ------------
  
              """,
              """
                      ------------
                      |          |
                      |          0
                      |         \\|/
                      |          
                      |         
                      |
                      ------------
  
              """,
              """
                    ------------
                    |          |
                    |          0
                    |        
                    |          
                    |         
                    |
                    ------------
  
              """,
              """
                      ------------
                      |          |
                      |          
                      |        
                      |          
                      |         
                      |
                      ------------
  
              """,
              """
                    ------------
                    |          
                    |          
                    |        
                    |          
                    |         
                    |
                    ------------
  
              """
              ]
    return f"You have {num} life points.\n" + stages[num]


def add_record(winning_record):
    """this takes winner's data as an argument to then compare it with existing records in the file 
    and insert to file, if it fits criteria for 10 best scores, ordered from  the shortest to the longest solving time"""
    list_of_records = []
    with open("high_scores.txt", "r") as f:
        for line in f:
            record = line.strip().split(" | ")
            list_of_records.append(record)
        if len(list_of_records) == 0:
            list_of_records.append(winning_record)
        else:
            for element in list_of_records:
                if winning_record[2] < int(element[2]):
                    list_of_records.insert(list_of_records.index(element), winning_record)
                    break
                else:
                    continue
            if winning_record not in list_of_records:
                list_of_records.append(winning_record)
            if len(list_of_records) > 10:
                list_of_records.pop()
    with open("high_scores.txt", "w") as f:
        for e in list_of_records:
            f.writelines(f"{e[0]} | {e[1]} | {e[2]} | {e[3]}\n")


def win(num_tries, city, solving_time):
    """this displays winning information along with doge ascii art, and adds the score to the best scores file if the criteria is met"""
    print("You win! Congratulations!")
    print(f"You guessed the capital after {num_tries} guesses. It took you {solving_time} seconds.")
    name = input("Please enter your name: ").strip().capitalize()
    now = datetime.now()
    date = now.strftime("%d.%m.%Y %H:%M")
    file_record = [name, date, solving_time, city]
    add_record(file_record)
    doge = """
                    ▄              ▄    
          WOW       ▌▒█           ▄▀▒▌   
                    ▌▒▒█        ▄▀▒▒▒▐   
                   ▐▄█▒▒▀▀▀▀▄▄▄▀▒▒▒▒▒▐   
                 ▄▄▀▒▒▒▒▒▒▒▒▒▒▒█▒▒▄█▒▐   
               ▄▀▒▒▒░░░▒▒▒░░░▒▒▒▀██▀▒▌   
              ▐▒▒▒▄▄▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▀▄▒▌  WOW
              ▌░░▌█▀▒▒▒▒▒▄▀█▄▒▒▒▒▒▒▒█▒▐  
             ▐░░░▒▒▒▒▒▒▒▒▌██▀▒▒░░░▒▒▒▀▄▌ 
             ▌░▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░░░░░▒▒▒▒▌ 
            ▌▒▒▒▄██▄▒▒▒▒▒▒▒▒░░░░░░░░▒▒▒▐ 
            ▐▒▒▐▄█▄█▌▒▒▒▒▒▒▒▒▒▒░▒░▒░▒▒▒▒▌
            ▐▒▒▐▀▐▀▒▒▒▒▒▒▒▒▒▒▒▒▒░▒░▒░▒▒▐ 
             ▌▒▒▀▄▄▄▄▄▄▒▒▒▒▒▒▒▒░▒░▒░▒▒▒▌ 
             ▐▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░▒░▒▒▄▒▒▐  
              ▀▄▒▒▒▒▒▒▒▒▒▒▒▒▒░▒░▒▄▒▒▒▒▌  WOW
                ▀▄▒▒▒▒▒▒▒▒▒▒▄▄▄▀▒▒▒▒▄▀   
          WOW     ▀▄▄▄▄▄▄▀▀▀▒▒▒▒▒▄▄▀     
                     ▀▀▀▀▀▀▀▀▀▀▀▀   
            """
    return f"{name} | {date} | {solving_time} | {city}\n" + doge


def play(country_capital):
    """this runs the game flow"""
    start_time = time.time()
    capital = country_capital[1].upper().replace(" ", "")
    country = country_capital[0]
    word_completion = "_" * len(capital)
    guessed = False
    guessed_letters = []
    guessed_words = []
    lives = 5
    tries = 0
    while not guessed and lives > 0:
        print(display_hangman(lives))
        print(word_completion)
        if lives == 1:
            print(f"HINT: the capital of {country}.")
        guess = input("Guess a letter or word: ").upper().replace(" ", "")
        tries += 1
        if len(guess) == 1 and guess.isalpha():
            if guess in guessed_letters:
                print("You already guessed this letter.")
            elif guess not in capital:
                print("This letter is not in the word.")
                lives -= 1
                guessed_letters.append(guess)
                print(f"These letters do not occur in the word: {guessed_letters}")
            else:
                word_as_list = list(word_completion)
                indices = [i for i, letter in enumerate(capital) if letter == guess]
                for index in indices:
                    word_as_list[index] = guess
                word_completion = "".join(word_as_list)
                if "_" not in word_completion:
                    guessed = True
        elif len(guess) == len(capital) and guess.isalpha():
            if guess in guessed_words:
                print("You already guessed the word.")
            elif guess != capital:
                print(f"{guess} is not the word.")
                lives -= 2
                guessed_words.append(guess)
            else:
                guessed = True
                word_completion = capital
        else:
            print("This is not a valid input.")
    if guessed:
        end_time = time.time()
        total_time = int(end_time - start_time)
        print(win(tries, capital, total_time))
    else:
        print(display_hangman(0))
        print(f"You're obviously dead. The capital was {capital.capitalize()}.")


def main():
    """this asks the user if he wants to play and picks a random capital for the game"""
    word = get_random_word(turn_file_to_dict("countries_and_capitals.txt"))
    play(word)
    while input("Play again? (Y/N) ").upper() == "Y":
        word = get_random_word(turn_file_to_dict("countries_and_capitals.txt"))
        play(word)
