package web_api;

import db_handler.db_handler;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;

@WebServlet(name = "logout")
public class logout extends HttpServlet {
    db_handler db = db_handler.getInstance();
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        db.log_info(request);
        HttpSession session = request.getSession();
        String a = request.getPathInfo();
        if(request.getPathInfo().equals("/"+session.getAttribute("uuid"))){
            session.setAttribute("uuid", null);
            session.setAttribute("logged_in", null);
            session.setAttribute("user", null);
        }

        RequestDispatcher view = request.getRequestDispatcher("/web/home.jsp");

        view.forward(request, response);
    }
}
