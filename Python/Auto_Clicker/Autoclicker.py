import time
import pyautogui
import random

def click():
	totalTime = 0 
	button = 0
	for i in range(100):
		sleepTime = random.randrange(30,75)
		print(f"Clicking in: {sleepTime} seconds")
		time.sleep(sleepTime)
		
		button = random.randrange(0,30)
		
		if (button==2):
			pyautogui.typewrite('a')
			print(f"Button 'a' pressed. Time elapsed: {round(totalTime /60,1)}.\nIterations remaining: {100-i}")
		else:
			pyautogui.leftClick()
			print(f"Clicked. Time elapsed: {round(totalTime /60,1)}\nIterations remaining: {100-i}\n")

		totalTime = totalTime + sleepTime
	
	pyautogui.alert('Script has ended.\nTotal time elapsed: {round(totalTime/60),2}')

click()

