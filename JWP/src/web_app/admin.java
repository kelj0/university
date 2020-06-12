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

@WebServlet(name = "admin")
public class admin extends HttpServlet {

    db_handler db = db_handler.getInstance();
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        RequestDispatcher view = request.getRequestDispatcher("web/admin.jsp");
        HttpSession session = request.getSession();
        String message = "";
        if(request.getParameter("password") == ""){
            session.setAttribute("admin",null);
            session.setAttribute("user", null);
            message = "Please provide password";
        }else if(db.check_creds(request.getParameter("username"), request.getParameter("password")) && request.getParameter("username").equals("admin")){
            session.setAttribute("admin", true);
            session.setAttribute("uuid", db.get_user_uuid(request.getParameter("username")));
            session.setAttribute("user", request.getParameter("username"));
            view = request.getRequestDispatcher("web/admin_.jsp");
        }else {
            session.setAttribute("admin",null);
            session.setAttribute("user", null);
            message = "Username/password combination doesnt exist!";
        }
        request.setAttribute("message", message);
        view.forward(request, response);
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        request.setAttribute("message", "");
        RequestDispatcher view = request.getRequestDispatcher("web/admin.jsp");
        db.log_info(request);
        view.forward(request, response);
    }
}
