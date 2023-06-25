import random
import telebot
from telebot import types
import requests
from bs4 import BeautifulSoup
import wikipedia
from transliterate import translit

from pyowm import OWM
from pyowm.utils import config
from pyowm.utils import timestamps
from pyowm.utils.config import get_default_config

token = ""
bot = telebot.TeleBot(token=token)

config_dict = get_default_config()
config_dict['language'] = 'ru'
owm = OWM('', config_dict)
mgr = owm.weather_manager()

lang = "ru"
wikipedia.set_lang(lang)

buttons = ["button1", "button2", "button3","button4","button5","button6","button7","button8","button9","button10"]
search_results = []

from enum import IntEnum

class WindDirection(IntEnum):
    Север = 0
    СевероВосток = 45
    Восток = 90
    ЮгоВосток = 135
    Юг = 180
    ЮгоЗапад = 225
    Запад = 270
    СевероЗапад = 315

def _parse_wind_direction(degrees) -> str:
    degrees = round(degrees / 45) * 45
    if degrees == 360:
        degrees = 0
    return WindDirection(degrees).name

def build_menu(buttons,n_cols,header_buttons=None,footer_buttons=None):
  menu = [buttons[i:i + n_cols] for i in range(0, len(buttons), n_cols)]
  if header_buttons:
    menu.insert(0, header_buttons)
  if footer_buttons:
    menu.append(footer_buttons)
  return menu

@bot.message_handler(commands=['start'])
def start(message):
    markup = types.InlineKeyboardMarkup()
    button1 = types.InlineKeyboardButton(text="Узнать погоду", callback_data="weather")
    button2 = types.InlineKeyboardButton(text="Поиск в Википедии", callback_data="wiki")
    markup.row(button1, button2)
    bot.send_message(message.chat.id, "Нажмите кнопку!", reply_markup=markup)
	
def get_weather(message):
	user_id = message.chat.id
	try:
		observation = mgr.weather_at_place(message.text)
		w = observation.weather
		bot.send_message(user_id, "Сейчас в городе " + message.text + " " + w.detailed_status + "\n" +
		"Страна " + observation.location.country + ". Координаты " + str(observation.location.lat) + "," + str(observation.location.lon) + "\n" +
		"Температура составляет " + str(w.temperature('celsius')["temp"]) + "°C. Ощущается как " + str(w.temperature('celsius')["feels_like"]) + "°C" + "\n" +
		"Скорость ветра " + str(w.wind()["speed"]) + " м/с. Направление ветра - " + _parse_wind_direction(w.wind()["deg"]))
	except Exception:
		mesg = bot.send_message(user_id,'Не получается найти такой город. Попробуйте еще раз.')
		bot.register_next_step_handler(mesg, get_weather)
	
@bot.message_handler(commands=['setlang'])
def setlang(message):
	user_id = message.chat.id
	lang = message.text.replace('/setlang ','')
	try:
		lang_exist = wikipedia.languages()[lang]
		wikipedia.set_lang(lang)
		bot.send_message(user_id, "Язык установлен - " + wikipedia.languages()[lang])
	except Exception:
		bot.send_message(user_id, "Такого языка нет")

@bot.callback_query_handler(func=lambda call: True)
def callback_inline(call):
	user_id = call.message.chat.id
	global search_results
	error = False

	if call.message:
		if call.data == 'weather':
			mesg = bot.send_message(user_id,'Введите город')
			bot.register_next_step_handler(mesg, get_weather)
		else:
			if call.data == 'wiki':  
				bot.send_message(user_id, "Введите запрос", reply_markup=None)
			else:
				if search_results:
					request = search_results[call.data]
				try:
					p = wikipedia.page(request, auto_suggest=False)
				except wikipedia.DisambiguationError as e:
					error = True
					search_results = dict(zip(buttons, e.options))
					button_list = []
					for k,v in search_results.items():
						button_list.append(types.InlineKeyboardButton(v, callback_data = k))
					# сборка клавиатуры из кнопок `KeyboardButton`
					reply_markup = types.InlineKeyboardMarkup(build_menu(button_list, n_cols=1))
					bot.send_message(user_id, text="Список результатов", reply_markup=reply_markup)
				if not error:
					answer = wikipedia.page(request, auto_suggest=False)
					bot.send_message(user_id, wikipedia.summary(request, auto_suggest=False) + "\n" + answer.url)

@bot.message_handler(content_types=['text'])
def getanswer(message):    
	user_id = message.chat.id
	global search_results	
	search_results = dict(zip(buttons, wikipedia.search(message.text)))
	button_list = []
	for k,v in search_results.items():
		button_list.append(types.InlineKeyboardButton(v, callback_data = k))
	reply_markup = types.InlineKeyboardMarkup(build_menu(button_list, n_cols=1))
	bot.send_message(user_id, text="Список результатов", reply_markup=reply_markup)
                     

bot.polling(none_stop=True, interval=0)