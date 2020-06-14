package web_app;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.google.gson.stream.JsonReader;
import com.paypal.api.payments.*;
import com.paypal.base.rest.APIContext;
import com.paypal.base.rest.PayPalRESTException;
import org.json.JSONException;
import org.json.JSONObject;
import db_handler.db_handler;
import models.Product;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.*;
import java.util.*;
import java.util.stream.Collectors;

@WebServlet(name = "checkout")
public class checkout extends HttpServlet {
    db_handler db = db_handler.getInstance();
    private APIContext apiContext = new APIContext(get_client_id(), get_client_secret(),"sandbox");
    private String get_client_id(){
        Properties prop = new Properties();
        String fileName = "C:\\Users\\keljo\\Desktop\\JWP\\.paypal"; // im sick of java and im not even going to try to google how to get a root project path
        InputStream is = null;
        try {
            is = new FileInputStream(fileName);
        } catch (FileNotFoundException ex) {
        }
        try {
            prop.load(is);
        } catch (IOException ex) {
        }
        return prop.getProperty("paypal.client_id");
    }

    private String get_client_secret(){
        Properties prop = new Properties();
        String fileName = "C:\\Users\\keljo\\Desktop\\JWP\\.paypal"; // im sick of java and im not even going to try to google how to get a root project path
        InputStream is = null;
        try {
            is = new FileInputStream(fileName);
        } catch (FileNotFoundException ex) {
        }
        try {
            prop.load(is);
        } catch (IOException ex) {
        }
        return prop.getProperty("paypal.secret");
    }

    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        db.log_info(request);
        if(request.getSession().getAttribute("logged_in") != null){
            List<String> item_ids = Arrays.asList(request.getParameter("items").split("\\|",-1));
            List<Product> products_in_cart = new ArrayList<>();
            List<Product> all_products =  db.get_all_products();
            for(Product p: all_products){
                item_ids.forEach(id-> {
                    if (p.uuid.equals(id)) {
                        products_in_cart.add(p);
                    }
                });
            }
            List<Item> items = new ArrayList<Item>();
            double total = 0;
            for(Product p: products_in_cart){
                items.add((new Item()).setName(p.name).setQuantity("1").setCurrency(("USD")).setPrice(String.valueOf(p.price)));
                total+=p.price;
            }
            request.getSession().setAttribute("cart_total", total);
            ItemList itemList = new ItemList();
            itemList.setItems(items);
            Details details = new Details();
            details.setShipping("1");
            details.setSubtotal(String.valueOf(total));
            details.setTax("1");
            Amount amount = new Amount();
            amount.setCurrency("USD");
            amount.setTotal(String.valueOf(total+2));
            amount.setDetails(details);
            Transaction transaction = new Transaction();
            transaction.setAmount(amount);
            transaction.setDescription("Thank you for shopping in the kelj0 shop");
            transaction.setItemList(itemList);
            List<Transaction> transactions = new ArrayList<Transaction>();
            transactions.add(transaction);

            Payer payer = new Payer();
            payer.setPaymentMethod("paypal");

            Payment payment = new Payment();
            payment.setIntent("sale");
            payment.setPayer(payer);
            payment.setTransactions(transactions);

            RedirectUrls redirectUrls = new RedirectUrls();
            String guid = UUID.randomUUID().toString().replaceAll("-", ""); // Not necessary, just demonstrating how we can add the order/user id as a param.
            String scheme = request.getScheme();
            String serverName = request.getServerName();
            int serverPort = request.getServerPort();
            redirectUrls.setCancelUrl(scheme + "://" + serverName+ ":" + serverPort + "/paypal_fail?guid=" + guid);
            redirectUrls.setReturnUrl(scheme + "://" + serverName+ ":" + serverPort + "/paypal_success?guid=" + guid);
            payment.setRedirectUrls(redirectUrls);
            try {
                Payment createdPayment = payment.create(apiContext);
                // ###Payment Approval Url
                Iterator<Links> links = createdPayment.getLinks().iterator();
                while (links.hasNext()) {
                    Links link = links.next();
                    if (link.getRel().equalsIgnoreCase("approval_url")) {
                        // redirecting to paypal site for handling payment
                        response.sendRedirect(link.getHref());
                    }
                }

            } catch (PayPalRESTException e) {
                System.err.println(e.getDetails());
            }
        }else{
            RequestDispatcher view = request.getRequestDispatcher("web/user_mgmt.jsp");
            view.forward(request, response);
        }
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        db.log_info(request);
        System.out.println(checkout.class.getProtectionDomain().getCodeSource().getLocation().getPath());
        RequestDispatcher view = request.getRequestDispatcher("web/checkout.jsp");

        view.forward(request, response);
    }
}
