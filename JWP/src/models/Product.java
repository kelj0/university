package models;

public class Product {
    public String name;
    public double price;
    public String uuid;

    public Product(String name, double price, String uuid) {
        this.name = name;
        this.price = price;
        this.uuid = uuid;
    }

    @Override
    public String toString() {
        return "{" +
                    "\"name\": '" + name + "\'," +
                    "\"price\": " + price + "'," +
                    "\"uuid\": '" + uuid + "\'" +
                '}';
    }
}
