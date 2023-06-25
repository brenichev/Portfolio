#from utils import set_timeout, fetch
import asyncio
from pyodide.http import pyfetch
import json
#import utils

last_seen_id = 0
first_seen_id = 0
start = False
# Находим элементы интерфейса по их ID
send_message = js.document.getElementById("send_message")
sender = js.document.getElementById("sender")
message_text = js.document.getElementById("message_text")
chat_window = js.document.getElementById("chat_window")
users_window = js.document.getElementById("users_window")

async def fetch(url, method, payload=None):
    kwargs = {
        "method": method
    }
    if method == "POST":
        kwargs["body"] = json.dumps(payload)
        kwargs["headers"] = {"Content-Type": "application/json"}
    return await pyfetch(url, **kwargs)


def set_timeout(delay, callback):
    def sync():
        asyncio.get_running_loop().run_until_complete(callback())

    asyncio.get_running_loop().call_later(delay, sync)


# Добавляет новое сообщение в список сообщений
def append_message(message):
    # Создаем HTML-элемент представляющий сообщение
    item = js.document.createElement("li")  # li - это HTML-тег для элемента списка
    item.className = "list-group-item"   # className - определяет как элемент выглядит
    # Добавляем его в список сообщений (chat_window)
    item.innerHTML = f'[<b>{message["sender"]}</b>]: <span>{message["text"]}</span><span class="badge text-bg-light text-secondary">{message["time"]}</span>'
    if sender.value == message["sender"]:
        delete_button = js.document.createElement("button")
        delete_button.className = "btn btn-outline-danger bi bi-trash" 
        async def delete_message_click(e):
            message_id = message["msg_id"]
            await fetch(f"/delete_message?message_id={message_id}", method="GET")
            chat_window.removeChild(item)        
        delete_button.onclick = delete_message_click
        # Добавляем кнопку удаления к элементу сообщения
        item.appendChild(delete_button)
    chat_window.prepend(item)

# def delete_message_click(e):
#     message_id = e.target.data["msg_id"]
#     item = e.target.data["item"] 
#     fetch(f"/delete_message?message_id={message_id}", method="GET")
#     chat_window.removeChild(item)

# Вызывается при клике на send_message
async def send_message_click(e):
    global start
    if(start == False):
        await fetch(f"/send_message?sender={sender.value}&text={message_text.value}&first_seen_id={first_seen_id}", method="GET")
    # Отправляем запрос
    else:
        await fetch(f"/send_message?sender={sender.value}&text={message_text.value}", method="GET")
    # Очищаем поле
    message_text.value = ""
    start = True



# Загружает новые сообщения с сервера и отображает их
async def load_fresh_messages():
    global last_seen_id
    # 1. Загружать все сообщения каждую секунду (большой трафик)
    if(start):
        result = await fetch(f"/get_messages?after={last_seen_id}&sender={sender.value}", method="GET")  # Делаем запрос
    else:
         result = await fetch(f"/get_messages?after={last_seen_id}", method="GET")  # Делаем запрос
    # chat_window.innerHTML = ""  # Очищаем окно с сообщениями
    data = await result.json()
    all_messages = data["messages"]  # Берем список сообщений из ответа сервера
    if data["users"] != []:
        users_window.innerHTML = ""
        for u in data["users"]:
            item = js.document.createElement("li")  # li - это HTML-тег для элемента списка
            item.className = "list-group-item"   # className - определяет как элемент выглядит    
            item.innerHTML = f'<span>{u["sender"]}</span>'
            users_window.prepend(item)
    
    if data["deleted"] == True:
        chat_window.innerHTML = ""
        last_seen_id = 0 #?
    for msg in all_messages:
        last_seen_id = msg["msg_id"]  # msg_id Последнего сообщение
        append_message(msg)
    set_timeout(1, load_fresh_messages) # Запускаем загрузку заново через секунду
    # 2. Загружать только новые сообщения

async def connect():
    global last_seen_id, first_seen_id
    result = await fetch(f"/connect", method="GET")  # Делаем запрос
    data = await result.json()
    first_seen_id = data["first_seen_id"]
    last_seen_id = first_seen_id
    await load_fresh_messages()

# Устнаваливаем действие при клике
send_message.onclick = send_message_click
connect()
#append_message({"sender":"Елена Борисовна", "text":"Присылаем в чат только рабочие сообщения!!!", "time": "00:01"})
#load_fresh_messages()