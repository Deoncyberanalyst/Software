import argparse
import sharedFunctions   

def printText(text, silentCondition):
    (silentCondition): print(text)

def initiateArgParser():
    headless = False
    silentCondition = False

    config = sharedFunctions.loadJson('helpContent.json')
    parser = argparse.ArgumentParser(prog=config['help_program_name'], description=config['help_program_description'])
    parser.add_argument('-headless', help=config['help_headless'], action='store_true')
    parser.add_argument('-head', help=config['help_head'], action='store_true')
    parser.add_argument('-silent', help=config['help_silent'], action='store_true')
    args = parser.parse_args()

    if (args.silent):
        silentCondition = True

    if args.headless: 
        headless = True
        printText(config['run_headless'], silentCondition)

    else:
        headless = False
        printText(config['run_head'], silentCondition)
  
    data = sharedFunctions.loadJson("websitesConfig.json")
 
       
    return headless
    
