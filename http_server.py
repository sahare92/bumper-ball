from socket import *
from flask import request

if __name__ == '__main__':

   client = socket(AF_INET, SOCK_DGRAM)

   bind_ip = '127.0.0.1'
   bind_port = 2121

   server = socket(AF_INET, SOCK_STREAM)
   server.bind((bind_ip, bind_port))
   server.listen(5)  # max backlog of connections

   print 'Listening on {}:{}'.format(bind_ip, bind_port)

   client_sock, address = server.accept()
   print 'Accepted connection from {}:{}'.format(address[0], address[1])

   while True:
       buff = []
       bytes_read = 0

       while (bytes_read < 27):
          buff = client_sock.recv(27);
          bytes_read += len(buff)

       message = str(buff)

       print 'The massage:', message
       client.sendto(message, ('localhost', 2122))