import json

def loadJson(json_file):

    try:
        with open (json_file, 'r') as file:
            jsonObject = json.load(file)
            return jsonObject

    except:
        print(f"Unable to find {json_file}.\nOr JSON file invalid!")    
        
        

