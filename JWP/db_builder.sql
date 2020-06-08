CREATE TABLE categories(
    id TEXT PRIMARY KEY,
    category TEXT NOT NULL
);

CREATE TABLE products(
    id TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    price REAL NOT NULL,
    category_id TEXT,
    FOREIGN KEY(category_id) REFERENCES categories(id)
);

CREATE TABLE users(
  id TEXT PRIMARY KEY,
  name TEXT NOT NULL,
  password TEXT NOT NULL
);

CREATE TABLE purchase(
  id TEXT PRIMARY KEY,
  user_id TEXT,
  total REAL NOT NULL,
  FOREIGN KEY(user_id) REFERENCES users(id)
);

CREATE TABLE log(
  id TEXT PRIMARY KEY,
  ip TEXT NOT NULL,
  url TEXT NOT NULL,
  user_id TEXT,
  time TEXT
);

INSERT INTO categories(id, category)
VALUES
  ('afe5a67e-246a-4cb5-bc65-a42ab13fc15e', 'tech'),
  ('9a8dda3a-3541-4643-ae8e-c3dbbc49d73c', 'cars'),
  ('91380147-d87c-4e04-8bce-293955ab819a', 'doors');

INSERT INTO products(id, name, price, category_id)
VALUES
  ('77cfab2c-3d8d-4b21-8b47-31d89eb21d3e', 'PC3000', 3000, 'afe5a67e-246a-4cb5-bc65-a42ab13fc15e'),
  ('fe1cc485-3a39-48ca-be33-6c314d27b38a', 'Supra', 4500, '9a8dda3a-3541-4643-ae8e-c3dbbc49d73c'),
  ('42a774eb-bd18-4a1a-86aa-0a140d4320ea', 'Back', 123.1, '91380147-d87c-4e04-8bce-293955ab819a'),
  ('e64a46ff-3793-4d09-aeb6-7c46bdf2616a', '4k tv', 1111, 'afe5a67e-246a-4cb5-bc65-a42ab13fc15e'),
  ('16ce2dbf-d03d-40b9-8187-4060c7397688', 'phone 1', 20, 'afe5a67e-246a-4cb5-bc65-a42ab13fc15e'),
  ('88f73db9-b0fc-4732-854e-067db71e2686', 'headphone 2.3', 594, 'afe5a67e-246a-4cb5-bc65-a42ab13fc15e');