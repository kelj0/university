package models;

public class Product {
    public final String name;
    public final double price;
    public final String uuid;
    public final String category_id;

    public Product(String name, double price, String uuid, String category_id) {
        this.name = name;
        this.price = price;
        this.uuid = uuid;
        this.category_id = category_id;
    }

    @Override
    public String toString() {
        return "{" +
                    "\"name\": '" + name + "'," +
                    "\"price\": " + price + "'," +
                    "\"uuid\": '" + uuid + "'" +
                    "\"category_id\": " + category_id +
                '}';
    }
}
