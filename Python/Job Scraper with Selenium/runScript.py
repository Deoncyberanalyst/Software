import time
import webDriver
import argsParser

def main():
    
    headless  = argsParser.initiateArgParser()
    webDriver1 = webDriver.SeleniumObjectModel(headless=headless)
    webDriver1.initiate()
    
    end = input("")
    
if __name__ == "__main__":
    main()
