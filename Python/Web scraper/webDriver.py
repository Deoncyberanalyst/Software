from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.options import Options
from colorama import Fore, Style  
import sharedFunctions
import re

CONFIG = 'config.json'

class SeleniumObjectModel:
    def __init__(self, headless):
        print(Style.RESET_ALL)
        chrome_options = Options()
        
        if headless: chrome_options.add_argument("--headless=new")
            
        chrome_options.add_argument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3")
        global driver
        driver = webdriver.Chrome(options=chrome_options)
        driver.implicitly_wait(2)


    def initiate(self):
        websitesConfigJson = sharedFunctions.loadJson(CONFIG)
        print()
        
        for i in websitesConfigJson['websites']:
            if (i['searchBy'] != "skip"):
                for j in i['urls']:
                    driver.get(j)
                    searchUrl(websiteName=i['websiteName'], searchBy=(i['searchBy'].lower().strip()), searchValue=(i["searchValue"]), excludeItemsRegex=i['excludeItemsRegex'], globalExclude=websitesConfigJson['globalExclude'])

        driver.quit()
                   
def checkIfExcluded(text, excludeItemsRegex, globalExclude):
    for i in globalExclude:
        regexPattern = fr"{i}"
        if re.search(regexPattern, text, re.IGNORECASE):
            return True

    for i in excludeItemsRegex:
        regexPattern = fr"{i}"
        if re.search(regexPattern, text, re.IGNORECASE): 
            return True
        
    return False

def printVacancies(printList, websiteName):
    if len(printList) > 0: 
        print(Fore.RED + f"{websiteName}:")
        for count, values in enumerate(printList):
            print(Fore.BLUE + f"{count+1}. {values}")
            if (count == len(printList)-1 ): print()

    else: print(Fore.RED + f"No jobs found for {websiteName}!\n")

    Style.RESET_ALL

def searchUrl(websiteName, searchBy,searchValue, excludeItemsRegex, globalExclude):
    if (searchBy == "class"): ObjectList = driver.find_elements(by=By.CLASS_NAME, value=searchValue)
    elif (searchBy  == "name"): ObjectList = driver.find_elements(by=By.ID, value=searchValue)        
    elif (searchBy == "xpath"): ObjectList = driver.find_elements(by=By.XPATH, value=searchValue)
    else: print("No valid searchBy method found.\nEnter valid option into the JSON file\nclassName, IdName or Xpath!") & exit()
       
    printList = []
    for i in ObjectList:
        text = (i.text).strip()
        if (len(text) > 0 and checkIfExcluded(text,excludeItemsRegex,globalExclude)==False): printList.append(text)

    printVacancies(printList, websiteName)
