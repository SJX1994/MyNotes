from http.server import HTTPServer, BaseHTTPRequestHandler
class helloHandler(BaseHTTPRequestHandler):
    def do_request(self):
        self.wfile.write('Hello, world sjx'.encode())
    # # def do_Get(self):
    #     self.send_response(200)
    #     self.send_header('content-type','text/html')
    #     self.end_headers()
    #     self.wfile.write('Hello, world sjx'.encode())
def main():
    PORT = 1234
    server = HTTPServer(('',PORT),helloHandler)
    print('runing on port %s' % PORT)
    server.serve_forever()

if __name__ == '__main__':
    main()

