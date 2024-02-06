import time
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.options import Options
from selenium.webdriver import ActionChains
from selenium.webdriver.support.wait import WebDriverWait
from colorama import Fore, Back, Style  
import sharedServices

WEBSITES_CONFIG = 'websitesConfig.json'

class SeleniumObjectModel:
    def __init__(self, headless):
        print(Style.RESET_ALL)
        chrome_options = Options()
        
        if headless == True:
            chrome_options.add_argument("--headless=new")
            
        chrome_options.add_argument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3")
        self.driver = webdriver.Chrome(options=chrome_options)


    def initiate(self):
        dataList = sharedServices.loadJson(WEBSITES_CONFIG)
        dataList = dataList['websites']
        
        for websites in dataList:
            websiteName = websites['websiteName']
            
            searchIdName = None
            searchClassName = None
            #searchIdName = websites['searchIdname']
            searchClassName = websites['searchClassName']

            urls = websites['url']
            
            for url in urls:
                printVacancies(driver= self.driver, websiteName= websiteName, url = url, className=searchClassName)

        driverQuit(self.driver)

                   

def printVacancies(driver, websiteName, url, className="",idName=""):
   
    driver.get(url)
    driver.implicitly_wait(0.5)
    
    if (className) : ObjectList = driver.find_elements(by=By.CLASS_NAME, value=className)
    elif (idName) : ObjectList = driver.find_elements(by=By.ID, value=idName)
    else: print("Error! in printVacancy function")

    printList = []
    for i in ObjectList:
        value = (i.text).strip()
        if (len(value)) >0: printList.append(value)

    #time.sleep(2)
    if len(printList) > 0: 
        print(Fore.RED + f"{websiteName}:")
        for count, values in enumerate(printList):
            print(Fore.BLUE + f"{count+1}. {values}")
            if (count == len(printList)-1 ): print()

   # else: 
        #print(Fore.RED + f"No jobs found for {websiteName}!")
 
    Style.RESET_ALL
    

def driverQuit(driver):
    print(Fore.RED + "Quitting")
    driver.quit()

