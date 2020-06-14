package web_app;

import db_handler.db_handler;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;

@WebServlet(name = "paypal_fail")
public class paypal_fail extends HttpServlet {
    db_handler db = db_handler.getInstance();
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        db.log_info(request);
        RequestDispatcher view = request.getRequestDispatcher("/web/paypal_fail.jsp");
        view.forward(request, response);
    }
}
