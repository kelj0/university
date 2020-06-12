package web_api;

import db_handler.db_handler;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.List;

@WebServlet(name = "get_logs")
public class get_logs extends HttpServlet {
    db_handler db = db_handler.getInstance();
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        response.setContentType("application/json");
        List<String> categories = db.get_logs();
        response.getWriter().print(helper.list_to_JSON(categories));
    }
}
