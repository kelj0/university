package db_handler;

import models.Category;
import models.Product;

import javax.servlet.http.HttpServletRequest;
import java.io.File;
import java.io.IOException;
import java.net.http.HttpRequest;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.sql.*;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;
import java.util.UUID;
import java.util.logging.Level;
import java.util.logging.Logger;

public class db_handler {
    private static final String DEFAULT_JAVA_CLASS = "org.sqlite.JDBC";
    //private final String PROJECT_DIRECTORY = System.getProperty("user.dir");
    private static final String URL_FORMAT = "jdbc:sqlite:C:\\Users\\keljo\\Desktop\\JWP\\JWP_DB.db";
    private static db_handler instance = null;
    private static Connection connection = null;

    private db_handler(String url) throws ClassNotFoundException{
        Class.forName(url);
    }

    public static db_handler getInstance(){
        if(instance == null){
            try {
                instance = new db_handler(DEFAULT_JAVA_CLASS);
                instance.initial_database_fill();
            } catch (ClassNotFoundException ex) {
                Logger.getLogger(db_handler.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
        return instance;
    }

    private void initial_database_fill(){
        if(!new File("C:\\Users\\keljo\\Desktop\\JWP\\db_builder.sql").exists()) {
            instance.execute_script("C:\\Users\\keljo\\Desktop\\JWP\\db_builder.sql");
        }
    }

    public void execute_update(String query){
        Statement statement = null;
        try {
            open_connection();
            statement = connection.createStatement();
            statement.executeUpdate(query);
        } catch (SQLException ex) {
            ex.printStackTrace();
        }finally{
            close_connection();
        }
    }

    public ResultSet execute_query(String query){
        Statement statement;
        ResultSet rs = null;
        try {
            open_connection();
            statement = connection.createStatement();
            rs = statement.executeQuery(query);
        } catch (SQLException ex) {
            System.out.println("Why you do this?");
        }finally{
            close_connection();
            return rs;
        }
    }


    public void execute_script(String path){
        System.out.println("Executing script " + path);
        open_connection();
        String content = "";
        try {
            content = new String(Files.readAllBytes(Paths.get(path)));
        }catch (IOException ex) {
            Logger.getLogger(db_handler.class.getName()).log(Level.SEVERE, null, ex);
        }
        for(String query : content.split(";")){
            execute_update(query);
        }
        close_connection();
        System.out.println("Done");
    }

    private void open_connection(){
        try {
            connection = DriverManager.getConnection(URL_FORMAT);
        } catch (SQLException e) {
            close_connection();
            Logger.getLogger(db_handler.class.getName()).log(Level.SEVERE, null, e);
        }
    }

    private void close_connection(){
        try{
            connection.close();
        }catch(SQLException e){
            Logger.getLogger(db_handler.class.getName()).log(Level.SEVERE, null, e);
        }
    }

    public List<Category> get_all_categories(){
        ArrayList<Category> l = new ArrayList<Category>();
        ResultSet rs = null;
        Statement statement = null;
        try {
            open_connection();
            statement = connection.createStatement();
            rs = statement.executeQuery("SELECT * FROM categories");
            while(rs.next()){
                Category c = new Category(rs.getString("category"),rs.getString("id"));
                l.add(c);
            }
        } catch (SQLException ex) {
            Logger.getLogger(db_handler.class.getName()).log(Level.SEVERE, null, ex);
        }finally{
            close_connection();
        }
        return l;
    }

    public List<Product> get_all_products(){
        ArrayList<Product> l = new ArrayList<Product>();
        ResultSet rs = null;
        Statement statement = null;
        try {
            open_connection();
            statement = connection.createStatement();
            rs = statement.executeQuery("SELECT * FROM products");
            while(rs.next()){
                Product p = new Product(rs.getString("name"),rs.getDouble("price"),rs.getString("id"), rs.getString("category_id"));
                l.add(p);
            }
        } catch (SQLException ex) {
            Logger.getLogger(db_handler.class.getName()).log(Level.SEVERE, null, ex);
        }finally{
            close_connection();
        }
        return l;
    }

    public boolean login(String username, String password) {
        return true;
    }

    public int register(String username, String password) {
        return 0;
    }


    public void log_info(HttpServletRequest r){
        String address = r.getRemoteAddr().toString();
        String time_stamp = new SimpleDateFormat("yyyyMMdd_HHmmss").format(Calendar.getInstance().getTime());
        String uuid = (r.getParameter("uuid")==null? "Anonymous":r.getParameter("uuid"));
        StringBuffer requestURL = r.getRequestURL();
        if (r.getQueryString() != null) {
            requestURL.append("?").append(r.getQueryString());
        }
        String completeURL = requestURL.toString();
        try {
            open_connection();
            PreparedStatement ps = connection.prepareStatement("insert into log(id,ip,url,user_id,time) values(?,?,?,?,?);");
            ps.setString(1, UUID.randomUUID().toString());
            ps.setString(2, address);
            ps.setString(3, String.valueOf(requestURL));
            ps.setString(4,uuid);
            ps.setString(5,time_stamp);
            ps.executeUpdate();
        }catch (SQLException ex){
            ex.printStackTrace();
        }finally {
            close_connection();
        }
    }
}
