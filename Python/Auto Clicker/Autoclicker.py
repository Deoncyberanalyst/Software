import time
import pyautogui
import random
import sys
from pynput import mouse, keyboard
import threading

pos = None
stop_event = threading.Event()
pause_event = threading.Event()


def randoMovement(sleepTime):
	pyautogui.keyDown('right')
	time.sleep(sleepTime/9)
	pyautogui.keyUp('right')

def normal_click(sleepTime):
	pyautogui.leftClick()
	time.sleep(sleepTime/1000)
	pyautogui.leftClick()



def click():
	i=0
	while i < max:

		pause_event.wait()
		if stop_event.is_set(): break

		if pos is not None:
			mouseX, mouseY = pos
			x_center = mouseX
			y_center = mouseY

			sleepTime = random.uniform(1.6,1.9)
			
			
			randVariation = random.randrange(0,30)
			pyautogui.moveTo(x_center,y_center,duration=random.uniform(0.1,0.3))

			print(f"Clicking in: {sleepTime} seconds")
			time.sleep(sleepTime)
			if (randVariation==0): 
				randoMovement(sleepTime)
				normal_click(sleepTime)

			elif (randVariation%2==0):
				pyautogui.moveRel(random.randint(-10,10), random.randint(-10,10))
				normal_click(sleepTime)
			else:
				i+=1
				normal_click(sleepTime)
				print(f"Iterations remaining: {round(max-i)}\n")
		else:
			print("Mouse position not set. Press 'x' to set the mouse position.")
			time.sleep(1)

def listen_keys():
	print("Press 'z' to cancel the script.")
	global pos  # Declare pos as global in the outer function (optional here)
	mouse_controller = mouse.Controller()

	def on_press(key):
		global pos  # Add this line to update the global pos variable
		try:
			if key.char == 'z':
				print("Script cancelled.")
				stop_event.set() 
				return False
			elif key.char == 'p':
				if pause_event.is_set():
					pause_event.clear()	
					print("Resumed. Press 'p' to pause.")
				else:
					pause_event.set()
					print("Paused. Press 'p' to resume.")
			elif key.char == 'x':
				pos = mouse_controller.position
				print(f"Mouse position: {pos}")
				pause_event.set()
		except AttributeError:
			pass

	with keyboard.Listener(on_press=on_press) as listener:
		listener.join()


if __name__ == '__main__':
	if len(sys.argv) > 1:
		max = int(sys.argv[1])
		print(f"Iterations: {max}")

	else:
		print("No arguments provided.")
		sys.exit(1)

	thread = threading.Thread(target=listen_keys)
	thread2 = threading.Thread(target=click)

	thread.start()
	thread2.start()

	if stop_event.is_set():
		thread.join()
		thread2.join()
		print("Script has ended.")
		sys.exit(0)
