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
            rs = statement.executeQuery("SELECT * FROM products;");
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

    public String get_user_uuid(String username){
        String uuid = "";
        ResultSet rs = null;
        Statement statement = null;
        try {
            open_connection();
            statement = connection.createStatement();
            rs = statement.executeQuery("SELECT * FROM users;");
            while(rs.next()){
                uuid = rs.getString("id");
            }
        } catch (SQLException ex) {
            Logger.getLogger(db_handler.class.getName()).log(Level.SEVERE, null, ex);
        }finally{
            close_connection();
        }
        return uuid;
    }

    public boolean check_creds(String username, String password){
        open_connection();
        PreparedStatement ps = null;
        boolean correct = false;
        try {
            ps = connection.prepareStatement("SELECT * FROM users WHERE name=? AND password=?;");
            ps.setString(1, username);
            ps.setString(2, password);
            correct = ps.executeQuery().next();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }finally{
            close_connection();
        }

        return correct;
    }

    public List<String> get_purchases(String uuid){
        open_connection();
        PreparedStatement ps = null;
        List<String> purchases = new ArrayList<>();

        try {
            ps = connection.prepareStatement("SELECT * FROM purchase WHERE user_id=?");
            ps.setString(1, uuid);
            ResultSet rs = ps.executeQuery();
            while(rs.next()){
                purchases.add(rs.getString("id") + "|" + rs.getString("total"));
            }
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }finally{
            close_connection();
        }
        return purchases;

    }


    public int register(String username, String password) {
        open_connection();

        PreparedStatement ps = null;
        int return_code = 2;
        // 0 success
        // 1 exists
        // 2 bad request
        try {
            ps = connection.prepareStatement("INSERT INTO users(id,name,password) VALUES(?,?,?);");
            ps.setString(1, UUID.randomUUID().toString());
            ps.setString(2, username);
            ps.setString(3, password);
            return_code = ps.executeUpdate();
        } catch (SQLException throwables) {
            return_code = 2;
            throwables.printStackTrace();
        }finally{
            close_connection();
        }

        return return_code;
    }


    public void log_info(HttpServletRequest r){
        String address = r.getRemoteAddr().toString();
        String time_stamp = new SimpleDateFormat("yyyyMMdd_HHmmss").format(Calendar.getInstance().getTime());
        String uuid = (r.getSession().getAttribute("uuid")==null? "Anonymous":r.getSession().getAttribute("uuid").toString());
        StringBuffer requestURL = r.getRequestURL();
        if (r.getQueryString() != null) {
            requestURL.append("?").append(r.getQueryString());
        }
        String completeURL = requestURL.toString();
        try {
            open_connection();
            PreparedStatement ps = connection.prepareStatement("INSERT INTO log(id,ip,url,user_id,time) VALUES(?,?,?,?,?);");
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
