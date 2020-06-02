package web_api;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.List;
import db_handler.db_handler;
import models.Product;

@WebServlet(name = "get_products")
public class get_products extends HttpServlet {
    db_handler db = db_handler.getInstance();
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        response.setContentType("application/json");
        List<Product> products = db.get_all_products();
        response.getWriter().print(helper.list_to_JSON(products));
    }
}
