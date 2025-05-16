import time
import pyautogui
import random
import sys
from pynput import mouse, keyboard
import threading

stop_event = threading.Event()

def randoMovement(sleepTime):
	pyautogui.keyDown('right')
	time.sleep(sleepTime/9)
	pyautogui.keyUp('right')

def normal_click(sleepTime):
	pyautogui.leftClick()
	time.sleep(sleepTime/1000)
	pyautogui.leftClick()


def click(mouseX,mouseY):
	i=0
	while not stop_event.is_set() and i < max:
		pyautogui.moveTo(mouseX,mouseY)
		sleepTime = random.uniform(1.8,2)
		print(f"Clicking in: {sleepTime} seconds")
		time.sleep(sleepTime)
		
		randVariation = random.randrange(0,30)
		if (randVariation==0): 
			randoMovement(sleepTime)
		else:
			i+=1
			normal_click(sleepTime)
			print(f"Iterations remaining: {round(max-i)}\n")
	pyautogui.alert('Script ended')


def get_mouse_pos():
    print("Press 'x' to set mouse position!")

    mouse_controller = mouse.Controller()

    def on_press(key):
            if key.char == 'x':
                pos = mouse_controller.position
                print(f"Mouse position set to: {pos}")
                return False  # Stop the listener

    # Start listening to keyboard
    with keyboard.Listener(on_press=on_press) as listener:
        listener.join()
    return mouse_controller.position

def listen_keys():
	print("Press 'z' to cancel the script.")

	def on_press(key):
				if key.char == 'z':
					print("Script cancelled.")
					stop_event.set() 
					return False

		# Start listening to keyboard
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
	mouseX,mouseY = get_mouse_pos()  # Get it first
	thread2 = threading.Thread(target=click, args=(mouseX,mouseY))
	thread2.start()

	thread.start()

	if stop_event.is_set():
		thread.join()
		thread2.join()
		print("Script has ended.")
		sys.exit(0)
