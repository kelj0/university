package db_handler;

import models.Product;

import java.sql.Connection;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

public class db_handler {
    private static final String DEFAULT_JAVA_CLASS = "com.microsoft.sqlserver.jdbc.SQLServerDriver";
    //private final String PROJECT_DIRECTORY = System.getProperty("user.dir");
    //private static final String URL_FORMAT = "jdbc:sqlserver://KELJO-PC\\SQLEXPRESS;databaseName=PPPK_DATABASE;user=sa;password=SQL"; //CHANGE ME
    private static db_handler instance = null;
    private static Connection connection = null;

    private db_handler(String url) throws ClassNotFoundException{
        Class.forName(url);
    }

    public static db_handler getInstance(){
        if(instance == null){
            try {
                instance = new db_handler(DEFAULT_JAVA_CLASS);
            } catch (ClassNotFoundException ex) {
                Logger.getLogger(db_handler.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
        return instance;
    }

    public static List<Product> get_all_products(){
        List<Product> products = List.of(
                new Product("Product 1", 1.2, "1fe5a67e-246a-4cb5-bc65-a42ab13fc15e"),
                new Product("Product 2", 1.3, "2fe5a67e-246a-4cb5-bc65-a42ab13fc15e"),
                new Product("Product 3", 1.4, "3fe5a67e-246a-4cb5-bc65-a42ab13fc15e")
            );

        return products;
    }

/*
    public void ExecuteScript(String path){
        System.out.println("Executing script " + path);
        OpenConnection();
        String content = "";
        try {
            content = new String(Files.readAllBytes(Paths.get(path)));
        }catch (IOException ex) {
            Logger.getLogger(dbHandler.class.getName()).log(Level.SEVERE, null, ex);
        }
        for(String query : content.split(";")){
            ExecuteUpdate(query);
        }
        CloseConnection();
        System.out.println("Done");
    }

    public void ExecuteUpdate(String query){
        Statement statement = null;
        try {
            OpenConnection();
            statement = connection.createStatement();
            statement.executeUpdate(query);
        } catch (SQLException ex) {
            ex.printStackTrace();
        }finally{
            CloseConnection();
        }
    }

    public ResultSet ExecuteQuery(String query){
        Statement statement = null;
        ResultSet rs = null;
        try {
            OpenConnection();
            statement = connection.createStatement();
            rs = statement.executeQuery(query);
        } catch (SQLException ex) {
            System.out.println("Why you do this?");
        }finally{
            CloseConnection();
            return rs;
        }
    }

    private void OpenConnection(){
        try {
            connection = DriverManager.getConnection(URL_FORMAT);
        } catch (SQLException e) {
            CloseConnection();
            Logger.getLogger(dbHandler.class.getName()).log(Level.SEVERE, null, e);
        }

    }

    private void CloseConnection(){
        try{
            connection.close();
        }catch(SQLException e){
            Logger.getLogger(dbHandler.class.getName()).log(Level.SEVERE, null, e);
        }

    }

    public void InsertRuta(Ruta r) {
        CallableStatement cstmt = null;
        try{
            OpenConnection();
            cstmt = connection.prepareCall("{call insert_ruta(?,?,?,?,?,?,?)}");
            cstmt.setInt("putni_nalog_id", r.getPutni_nalog_id());
            cstmt.setDouble("x_koordinata_a", r.getX_koordinata_a());
            cstmt.setDouble("y_koordinata_a", r.getY_koordinata_a());
            cstmt.setDouble("x_koordinata_b", r.getX_koordinata_b());
            cstmt.setDouble("y_koordinata_b", r.getY_koordinata_b());
            cstmt.setDouble("km_izmedu_a_b", r.getKm_izmedu_a_b());
            cstmt.setDouble("prosjecna_brzina", r.getProsjecna_brzina());
            cstmt.execute();
        }catch (SQLException ex){
            ex.printStackTrace();
        }finally{
            CloseConnection();
        }

    }

    public ArrayList<Ruta> SelectRute(int putni_nalog_id) {
        ArrayList<Ruta> l = new ArrayList<Ruta>();
        ResultSet rs = null;
        Statement statement = null;
        try {
            OpenConnection();
            statement = connection.createStatement();
            rs = statement.executeQuery("SELECT * FROM RUTA WHERE putni_nalog_id=" + putni_nalog_id);
            while(rs.next()){
                Ruta r = new Ruta(
                        rs.getInt("id"),
                        rs.getInt("putni_nalog_id"),
                        rs.getDouble("x_koordinata_a"),
                        rs.getDouble("y_koordinata_a"),
                        rs.getDouble("x_koordinata_b"),
                        rs.getDouble("y_koordinata_b"),
                        rs.getDouble("km_izmedu_a_b"),
                        rs.getDouble("prosjecna_brzina")
                );
                l.add(r);
            }
        } catch (SQLException ex) {
            Logger.getLogger(dbHandler.class.getName()).log(Level.SEVERE, null, ex);
        }finally{
            CloseConnection();
        }
        return l;
    }
 */
}
