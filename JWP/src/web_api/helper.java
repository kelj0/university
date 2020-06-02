package web_api;


import models.Product;
import com.google.gson.Gson;
import java.util.List;

public class helper {
    public static String list_to_JSON(List<Product> list){
        Gson g = new Gson();
        return g.toJson(list);
    }
}
