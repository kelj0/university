class Server():
    users = {'admin@admin':{'name':'admin','password':'pass'}}
    def __init__(self):
        pass

    def register(self,email,name,password):
        if email in self.users.keys():
            return False 
        else:
            self.users[email] = {'name':name,'password':password}
            return True
    def login(self,email,password):
        if self.users[email]['password'] == password:
            return True 
        else:
            return  False


    
