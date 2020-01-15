import unittest
from night_nomads import Server

class TestStringMethods(unittest.TestCase):
    s = Server()
    def test_registerWorking(self):
        self.assertTrue(self.s.register('newuser@email','name','password'))
        
    def test_registerFail(self):
        self.assertFalse(self.s.register('admin@admin','randomname','pass'))

    def test_loginWorking(self):
        self.assertTrue(self.s.login('admin@admin', 'pass'))

    def test_loginFail(self):
        self.assertFalse(self.s.login('admin@admin','Wrong Password'))

    def test_loginException(self):
        with self.assertRaises(KeyError):
            self.s.login('unknown@email.com','nope')

if __name__ == '__main__':
    unittest.main()
