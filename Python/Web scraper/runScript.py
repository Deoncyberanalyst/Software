import webDriver
import argsParser

def main():
    headless = argsParser.initiateArgParser()
    webDriver1 = webDriver.SeleniumObjectModel(headless)
    webDriver1.initiate()
    
    input("")
    
if __name__ == "__main__":
    main()

