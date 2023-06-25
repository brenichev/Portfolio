import sys
from flask import Flask, request, render_template
from datetime import datetime
import json

app = Flask(__name__, static_folder="./client", template_folder="./client")  # Настройки приложения

msg_id = 1
all_messages = []
all_users = []
deleted = False
count = datetime.now()


@app.route("/chat")
def chat_page():
    return render_template("chat.html")


def add_message(sender, text):
    global msg_id
    new_message = {
        "sender": sender,
        "text": text,
        "time": datetime.now().strftime("%H:%M"),
        "msg_id": msg_id
    }
    msg_id += 1
    all_messages.append(new_message)
    with open("messages.txt", "a") as f:
        f.write(f"{new_message}\n")

@app.route("/delete_message")
def delete_message():
    global all_messages
    global deleted
    global msg_id
    message_id = request.args.get("message_id")
    # Находим сообщение по его идентификатору
    message = next((m for m in all_messages if m["msg_id"] == int(message_id)), None)
    # Если сообщение найдено, удаляем его из списка
    if message:
        # Записываем изменения в файл
        with open("messages.txt", "a") as f:            
                #f.write(f'{m["sender"]}\t{m["text"]}\t{m["time"]}\n')
            f.write(f'deleted: {message}\n')
        all_messages.remove(message)
        #msg_id -= 1
        for u in all_users:
            u["actual"] = False
        deleted = True        
        return {"result": True}
    else:
        return {"result": False}
#    if message_id in all_messages:
#        del all_messages[message_id]

# @app.route("/delete_message")
# def remove_message():
#     message_id = request.args.get("message_id")
#     delete_message(int(message_id))
#     return {"result": True}

# API для получения списка сообщений

@app.route("/get_messages")
def get_messages():
    global deleted
    global count
    global all_users
    deleted2 = False
    now_users = []
    sender = request.args.get("sender")
    after = request.args.get("after", 0)
    #user = next((x for x in all_users if x["sender"] == sender), None)
    if sender != None:
        # if (user == None):        
        #     new_user = {
        #         "sender":  sender,
        #         "first_seen_id" : msg_id - 1,
        #         "updated": datetime.now(),
        #         "actual": True
        #     }
        #     all_users.append(new_user)
        # else:
        #if user != None:
        for u in all_users:
            if u["sender"] == sender:
                print(u["sender"] + " " + str(u["updated"]), file=sys.stderr)
                u["updated"] = datetime.now()
                #u["updated"] += 1
                print(u["sender"] + " " + str(u["updated"]), file=sys.stderr)
                if (u["actual"] == False):
                    after = u["first_seen_id"]
                    u["actual"] = True
                    deleted2 = True 
    
    if(abs(datetime.now().second - count.second) >= 5 and (datetime.now().second - count.second) <= 50):
        count = datetime.now()
        now_users = all_users   
        for u in all_users:
            if (abs(u["updated"].second - count.second) > 10 and abs(u["updated"].second - count.second) < 50):
                print("deleted \n" + u["sender"] + " " + str(u["updated"]), file=sys.stderr)
                print(u["sender"] + " " + str(datetime.now().second), file=sys.stderr)
                print(abs(u["updated"].second - count.second), file=sys.stderr)
                all_users.remove(u)                              
            # if (u["actual"] == False):
            #     after = u["first_seen_id"]
            #     u["actual"] = True
            #     deleted2 = True 
    # if deleted:
    #     sender = request.args.get("sender", 0)
    #     after = next(x for x in all_users if x["sender"] == sender)["first_seen_id"]
    #     deleted2 = True 
    #     deleted = False   
    filtered_messages = [msg for msg in all_messages if msg["msg_id"] > int(after)] # get only messages with higher ids 
    return {"messages": filtered_messages, "users": now_users,"deleted": deleted2}

    #return {"messages": all_messages}

@app.route("/connect")
def connect():    
    return {"first_seen_id": msg_id - 1}

# HTTP-GET
# API для получения отправки сообщения  /send_message?sender=Mike&text=Hello
@app.route("/send_message")
def send_message():
    sender = request.args["sender"]
    text = request.args["text"]  
    add_message(sender, text)
    if request.args.get("first_seen_id", 0) != None:
        if next((u for u in all_users if u["sender"] == sender), None) == None:
            new_user = {
                "sender":  sender,
                "first_seen_id" : int(request.args.get("first_seen_id", 0)),
                "updated": datetime.now(),
                "actual": True
            }
            all_users.append(new_user)
    return {"result": True}


# Главная страница
@app.route("/")
def hello_page():
    return "New text goes here"


app.run()
