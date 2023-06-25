import random
import telebot
from telebot import types
import requests
from bs4 import BeautifulSoup

token = ""
bot = telebot.TeleBot(token=token)

def normalize_city_name(name):
    return name.strip().lower().replace('ё', 'е')

def get_currency(id):
    URL = "https://cbr.ru/currency_base/daily/"

    r = requests.get(URL)
    soup = BeautifulSoup(r.text, features="html.parser")
    table = soup.find("table", attrs={"class": "data"})
    currently_watching = soup.find('td', text=id)
    #for row in table.find_all("tr")[2:]:
    test = currently_watching.parent.contents[9].get_text().strip()
    return "Курс доллара США равен " + test + " рублей"

#Загрузка списка городов с вики

# URL = "https://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D0%B3%D0%BE%D1%80%D0%BE%D0%B4%D0%BE%D0%B2_%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D0%B8"

# r = requests.get(URL)
# with open("cities.html", "w", encoding="utf-8") as f:
#     f.write(r.text)

# from bs4 import BeautifulSoup

# with open("cities.html", "r", encoding="utf-8") as f:
#     html = f.read()

# soup = BeautifulSoup(html, features="html.parser")
# table = soup.find("table", attrs={"class": "standard sortable"})

# with open("cities.txt", "w", encoding="utf-8") as f:
#     for row in table.find_all("tr")[2:]:
#         city = row.find_all("td")[2].get_text().strip()
#         f.write(f"{city}\n")

notes = {}
cities = {}
citiesList = {normalize_city_name(x) for x in open("cities.txt", "r", encoding="utf-8").readlines() if x.strip()}
wrong_char = ("Ъ", "ь", "ы", "й")
win = {}

@bot.message_handler(commands=['start'])
def start(message):
    #keyboard = types.InlineKeyboardMarkup()
    markup = types.InlineKeyboardMarkup()
    # добавляем на нее две кнопки
    button1 = types.InlineKeyboardButton(text="Курс валюты", callback_data="currency")
    button2 = types.InlineKeyboardButton(text="Играть в города", callback_data="cities")
    # keyboard.add(button1)
    # keyboard.add(button2)
    markup.row(button1, button2)
    bot.send_message(message.chat.id, "Нажмите кнопку!", reply_markup=markup)
    # user_id = message.chat.id
    # #if user_id not in notes:
    # bot.send_message(user_id, "Назовите город")
    # notes[user_id] = []
    # cities[user_id] = citiesList
    #else:
    #    bot.send_message(user_id, notes[user_id])

@bot.message_handler(commands=['currency'])
def currency(message):
    user_id = message.chat.id
    bot.send_message(user_id, get_currency(840))
    #else:
    #    bot.send_message(user_id, notes[user_id])

@bot.callback_query_handler(func=lambda call: True)
def callback_inline(call):
    user_id = call.message.chat.id
    if call.message:
        if call.data == 'currency':
            bot.send_message(call.message.chat.id, get_currency(840))
        if call.data == 'cities':  
            bot.send_message(user_id, "Назовите город", reply_markup=None)
            notes[user_id] = []
            #cities[user_id] = citiesList  
            mylist = [] 
            mylist = random.choices(list(citiesList),k=len(citiesList)//25)
            cities[user_id] = mylist
            win[user_id] = False

#@bot.message_handler(content_types=['text'])
@bot.message_handler(content_types=['text'])
def remember(message):    
    user_id = message.chat.id
    char = ""
    #letters =   
    if notes: #len(notes[user_id]) != 0:
        if notes[user_id]: 
            for char in notes[user_id][-1][::-1]:
                        if char in wrong_char:
                            continue
                        else:
                            break
        if message.text.startswith(char) or notes[user_id] == "":  
            if message.text not in notes[user_id]:
                if message.text in citiesList: #cities[user_id]
                    notes[user_id].append(message.text)
                    #cities[user_id].remove(message.text)            
                    # выбираем букву для следующего города
                    for char in message.text[::-1]:
                        if char in wrong_char:
                            continue
                        else:
                            break
                    for city in cities[user_id]:
                        if city not in notes[user_id]:                     
                            if city.startswith(char):                            
                                break
                        else:
                            cities[user_id].remove(city)  
                    else:
                        bot.send_message(user_id, "Вы победили")
                        win[user_id] = True
                                                
                    if not win[user_id]:
                        bot.send_message(user_id, city)
                        notes[user_id].append(city)
                        cities[user_id].remove(city)
                else:
                    bot.send_message(user_id, "Я не знаю такого города, выберите другой")
            else:
                bot.send_message(user_id, "Такой город уже был")
        else:
            bot.send_message(user_id, "Неправильная буква")

bot.polling(none_stop=True, interval=0)