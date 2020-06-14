package web_app;

import db_handler.db_handler;
import models.Product;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;
import java.util.List;

@WebServlet(name = "paypal")
public class paypal_success extends HttpServlet {
    db_handler db = db_handler.getInstance();
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        db.log_info(request);
        RequestDispatcher view = request.getRequestDispatcher("/web/paypal_success.jsp");
        double total = (double)request.getSession().getAttribute("cart_total");
        //db_insert bought items
        db.insert_purchase(request.getSession().getAttribute("uuid").toString(), total);
        view.forward(request, response);
    }
}
