import json

def loadJson(json_file):
    with open (json_file, 'r') as file:
        jsonObject = json.load(file)
    return jsonObject

