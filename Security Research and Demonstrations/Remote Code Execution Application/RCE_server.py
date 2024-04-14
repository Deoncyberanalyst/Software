import socket

LOGFILE_LOCATION = "log.txt"

def setup():
    host = '192.168.1.199'
    port = 54320
    global addr 
    addr= (host,port)

    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.bind((host, port))
    s.listen()
    save(f'[+]\tListening on {host}, {port}')

    conn, address = s.accept()
    save(f'[+]\tEstablished connection! {conn.getpeername()}\n')
    start(s, conn)



def start(s, conn):
    
    while True:
        data = input('[+]\tEnter command: ')

        if data=="Powershell":    
            conn.sendall(data.encode())
            data = conn.recv(4096)
            print(data.decode())
            data = input('[+]\Powershell: ')
            conn.sendall(data.encode())
            data = conn.recv(4096)
            save(data.decode())
        

def save(text):
    print(text)
    with open(f'{LOGFILE_LOCATION}.txt', 'a') as f:
        f.write(text)

setup()



