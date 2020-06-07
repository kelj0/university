package web_api;

import db_handler.db_handler;
import models.Category;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.List;

@WebServlet(name = "get_categories")
public class get_categories extends HttpServlet {
    final db_handler db = db_handler.getInstance();
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
        response.setContentType("application/json");
        List<Category> categories = db.get_all_categories();
        response.getWriter().print(helper.list_to_JSON(categories));
    }
}
