package web_app;

import db_handler.db_handler;

import javax.servlet.RequestDispatcher;
import java.io.IOException;

@javax.servlet.annotation.WebServlet(name = "home")
public class home extends javax.servlet.http.HttpServlet {
    db_handler db = db_handler.getInstance();

    protected void doGet(javax.servlet.http.HttpServletRequest request, javax.servlet.http.HttpServletResponse response) throws javax.servlet.ServletException, IOException {
        db.log_info(request);
        RequestDispatcher view = request.getRequestDispatcher("web/home.jsp");

        view.forward(request, response);
    }
}
