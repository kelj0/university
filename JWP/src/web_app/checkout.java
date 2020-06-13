package web_app;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.google.gson.stream.JsonReader;
import org.json.JSONException;
import org.json.JSONObject;
import db_handler.db_handler;
import models.Product;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.StringReader;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

@WebServlet(name = "checkout")
public class checkout extends HttpServlet {
    List<Product> l = new ArrayList<Product>();
    db_handler db = db_handler.getInstance();
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        db.log_info(request);
        if(request.getSession().getAttribute("logged_in") != null){
            List<String> item_ids = Arrays.asList(request.getParameter("items").split("\\|",-1));
            List<Product> products_in_cart = new ArrayList<>();
            List<Product> all_products =  db.get_all_products();
            for(Product p: all_products){
                item_ids.forEach(id-> {
                    if (p.uuid.equals(id)) {
                        products_in_cart.add(p);
                    }
                });
            }
            System.out.println(products_in_cart);
            // redirect to paypal and done
        }else{
            RequestDispatcher view = request.getRequestDispatcher("web/user_mgmt.jsp");
            view.forward(request, response);
        }
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        db.log_info(request);
        RequestDispatcher view = request.getRequestDispatcher("web/checkout.jsp");

        view.forward(request, response);
    }
}
