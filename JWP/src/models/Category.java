package models;

public class Category {
    public final String category;
    public final String uuid;

    public Category(String category, String uuid) {
        this.category = category;
        this.uuid = uuid;
    }

    @Override
    public String toString() {
        return "{" +
                    "\"category\":'" + category + '\'' +
                    "\"uuid\":'" + uuid + '\'' +
                '}';
    }
}
