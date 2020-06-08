package web_app;

import db_handler.db_handler;
import models.Product;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

@WebServlet(name = "checkout")
public class checkout extends HttpServlet {
    List<Product> l = new ArrayList<Product>();
    db_handler db = db_handler.getInstance();
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        db.log_info(request);
        if(Boolean.parseBoolean(request.getParameter("logged_in"))){
            //handle paypal
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
