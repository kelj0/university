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

@WebServlet(name = "profile")
public class profile extends HttpServlet {
    db_handler db = db_handler.getInstance();
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        db.log_info(request);
        HttpSession session = request.getSession();
        RequestDispatcher view = request.getRequestDispatcher("/web/profile.jsp");

        if(session.getAttribute("logged_in") == null) {
           view = request.getRequestDispatcher("/web/home.jsp");
        }
        view.forward(request, response);
    }
}
