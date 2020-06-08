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

@WebServlet(name = "user_mgmt")
public class user_mgmt extends HttpServlet {
    public String message = "";
    db_handler db = db_handler.getInstance();
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        db.log_info(request);
        RequestDispatcher view = null;
        HttpSession session = request.getSession();

        if(request.getParameter("password") == ""){
            session.setAttribute("logged_in",false);
            view = request.getRequestDispatcher("web/home.jsp");
            view.forward(request, response);
            return;
        }

        if(request.getParameter("rpassword") != ""){
            if(db.login(request.getParameter("username"), request.getParameter("password"))){
                view = request.getRequestDispatcher("web/checkout.jsp");
                session.setAttribute("logged_in",true);
            }else {
                view = request.getRequestDispatcher("web/user_mgmt.jsp");
                message = "Username/password combination doesnt exist!";
            }
        }else{
            view = request.getRequestDispatcher("web/user_mgmt.jsp");
            if(request.getParameter("password") == request.getParameter("rpassword")) {
                int ret = db.register(request.getParameter("username"), request.getParameter("password"));
                switch (ret){
                    case 0:
                        message = "Successfully create a new account, please login";
                        break;
                    case 1:
                        message = "Account already exists!";
                        break;
                    default:
                        message = "Bad request";
                        break;
                }
            }else{
                message = "Passwords dont match!";
            }
        }

        view.forward(request, response);
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        db.log_info(request);
        RequestDispatcher view = request.getRequestDispatcher("web/user_mgmt.jsp");
        view.forward(request, response);
    }
}
