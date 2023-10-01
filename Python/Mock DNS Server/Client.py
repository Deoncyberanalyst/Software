import sys
import random
import socket
import time
from collections import namedtuple

PORT = 12345

def sendDNSquery(port):
        #Send all the data in binary
        while True:
            print("For a CName, enter a webiste such as 'www.image.google.com'")
            domain, cname = obtainDomain()
            query = convert_to_string(ruleEngine(domain, cname))
            s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
            server = ('localhost',port)

            s.sendto(query.encode(), server)
            print("DNS request sent. Waiting for response...")

            #Print Received message
            data, addr = s.recvfrom(1024)
            data = data.decode()
            print(f"\nResponse\n{data}")

            user_input = input("Send another request(Y,N)? ")
            if (user_input.capitalize() == "N"):
                s.sendto(b'CLOSE', server)
                time.sleep(2)
                s.close()
                sys.exit()

def obtainDomain():
    user_input2 = input("Is this a CName(T/F)?")
    user_input = input("\nEnter website domain: ") or 'www.google.com'

    if  (len(user_input)) >= 9 and (user_input2.upper() == "T" or user_input2.upper() == "F" ) :
        if (user_input2.capitalize() == "T"):
            cname = True 
        else:
             cname = False
        return user_input, cname
    else:
         print("Error\nEnter valid domain. Example 'www.google.com'\nRequest CName with T or F") 
         time.sleep(2)
         sys.exit()
        
def convert_to_string(query):
        return_string = ""
        i = 0
        print("\nDNS query sent")
        for fields in query._fields:
            return_string += f"{fields}|{getattr(query, fields)}\n"
            print(f"{fields}:{getattr(query, fields)}")
        print("\n")
        return return_string

def ruleEngine(domain, cname):
   #Query message
    dnsquery = namedtuple("query",
    ['transactionID', 'flags','q_count','qname','qtype', 'qclass'])

    random.seed()
    dnsquery.transactionID = random.randint(1,10) #A unique identifier for the query.
    dnsquery.flags = "None"     #Various flags indicating query type and recursion desired.
    dnsquery.q_count = 1

    #QNAME
    qname = (domain.strip()).split('.')
    dnsquery.qname = ".".join(qname[1:])

    #QTYPE
    if (cname == True):
        dnsquery.qtype = "CNAME"
    else:
        dnsquery.qtype = "A"

    dnsquery.qclass = "IN" #internet

    return dnsquery
     
def main():
    sendDNSquery(PORT)

if __name__ == "__main__":
    main()





