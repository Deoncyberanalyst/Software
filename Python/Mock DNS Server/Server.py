import socket
from collections import namedtuple
import sys
import time

PORT = 12345

def ruleEngine(field_names, data_values):
    fields = field_names
    fields.append("rname")
    fields.append("ttl")
    fields.append("rdlength")
    fields.append("rdata")
    fields.append("rtype")
    fields.append("dnsResponse")
    
     # ['transactionID', 'flags','q_count','qname','qtype', 'qclass')
    dnstuple = namedtuple('dnstuple', fields)
    dnstuple.transactionID = data_values[0]
    dnstuple.flags = data_values[1]
    dnstuple.q_count = 0
    dnstuple.qname = data_values[3]
    dnstuple.qtype = data_values[4]
    dnstuple.qclass = data_values[5]

    #answer section
    dnstuple.rname = data_values[2]

    #image.google
    #google.com
    

    if (dnstuple.qtype == "CNAME"):
        dnstuple.rtype = "CNAME"
        var = dnstuple.qname.split('.')
        var = ".".join(var[1:])
        dnstuple.rdata = f"www.{var}"
    else:
        dnstuple.rtype = "A"
        dnstuple.rdata = "192.168.1.99"
        
    dnstuple.ttl = "3600"
    dnstuple.rdlength = "8"

    #Answer
    dnstuple.dnsResponse = f"\t{dnstuple.qname}\t{dnstuple.ttl}\t{dnstuple.qclass}\t{dnstuple.rtype}\t{dnstuple.rdata}"
    return dnstuple


def convert_to_string(query):
        return_string = ""
 
        for fields in query._fields:
            return_string += f"{fields}:{getattr(query, fields)} \n"

    
        return return_string

def convert_to_tuple(data):
    data = data.split("\n")
    fields = []

    field_names = []
    data_values = []

    for line in data:
        fields.append(line.strip())

    fields.pop(-1)

    for i in fields:
        parts = i.split('|')
        field_names.append(parts[0])
        data_values.append(parts[1])

    return field_names, data_values
        
def startDNS():
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    server = ('localhost', 12345)
    s.bind(server)
    print("DNS server is listening...")

    while True:
        data, client_address = s.recvfrom(1024)
        decoded_data = data.decode()

        if (decoded_data != "CLOSE"):
            field_names, data_values = convert_to_tuple(decoded_data)
            response = convert_to_string(ruleEngine(field_names, data_values))
            s.sendto(response.encode(), client_address)
            print("DNS responded to query.")
        else:
            s.close()
            print("Server closing....")
            time.sleep(2)
            sys.exit()
              
def main():
    startDNS()

if __name__ == '__main__':
    startDNS()