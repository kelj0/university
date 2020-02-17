/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package datahandlers;


import com.itextpdf.kernel.pdf.PdfDocument;
import com.itextpdf.kernel.pdf.PdfWriter;
import com.itextpdf.layout.Document;
import com.itextpdf.layout.element.Paragraph;
import java.io.FileNotFoundException;
import java.nio.file.Paths;
import java.sql.Date;
import javax.persistence.ParameterMode;
import javax.persistence.StoredProcedureQuery;
import models.PN_for_pdf;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.cfg.Configuration;

/**
 *
 * @author keljo
 */
public class pdfDataHandler {

    private String path;
    private String fname;
    public void generatePutniNalogPdf(int id, String path, String fname) {
        Configuration cfg = createHibernateConfiguration();
        this.path = path;
        this.fname = fname;
        try(SessionFactory sessionFactory = cfg.buildSessionFactory();
                Session session = sessionFactory.openSession()){
            session.beginTransaction();
            StoredProcedureQuery spq = session.createStoredProcedureCall("dohvati_putni_nalog");
            spq.registerStoredProcedureParameter("id", int.class, ParameterMode.IN);
            spq.setParameter("id", id);
            spq.execute();
            Object[] result = (Object[]) spq.getResultList().get(0);
            String datum_izrade = ((Date)result[4]).toString();
            String datum_pocetka = ((Date)result[5]).toString();
            String datum_zavrsetka = ((Date)result[6]).toString();
            String status = (String) result[7];
            String ime = (String) result[8];
            String prezime = (String) result[9];
            String marka = (String) result[10];
            String godina_proizvodnje = String.valueOf((int)result[11]);
            
            PN_for_pdf pfp = new PN_for_pdf(datum_izrade, datum_pocetka, datum_zavrsetka, status, ime, prezime, marka, godina_proizvodnje);
            
            writeToPdf(pfp);
            
            session.getTransaction().commit();
        }catch(Exception ex){
            ex.printStackTrace();
        }
       
    }
    
    private void writeToPdf(PN_for_pdf pfp) throws FileNotFoundException{
        String dest = Paths.get(this.path, this.fname).toString();
        PdfWriter writer = new PdfWriter(dest);
        PdfDocument pdf = new PdfDocument(writer);

        Document document = new Document(pdf);
        String para1 = String.format(
                "Datum izrade: %s \n"
                + "Datum pocetka: %s , Datum zavrsetka: %s \n"
                + "INFO O VOZACU\n"
                + "%s %s \n"
                + "INFO O VOZILU\n"
                + "%s(%s) \n"
                + "STATUS PUTNOG NALOGA: %s",
                pfp.getDatum_izrade(),pfp.getDatum_pocetka(),pfp.getDatum_zavrsetka(),
                pfp.getIme(),pfp.getPrezime(),
                pfp.getMarka(),pfp.getGodina_proizvodnje(),pfp.getStatus());
        Paragraph paragraph1 = new Paragraph(para1);
        document.add(paragraph1);
        document.close();  
        
    }
    
    
    private Configuration createHibernateConfiguration() {
        String url = "jdbc:sqlserver://KELJO-PC\\SQLEXPRESS;databaseName=PPPK_DATABASE";
        Configuration cfg = new Configuration()
                .setProperty("hibernate.connection.driver_class", "com.microsoft.sqlserver.jdbc.SQLServerDriver")
                .setProperty("hibernate.connection.url", url)
                .setProperty("hibernate.connection.username", "sa")
                .setProperty("hibernate.connection.password", "SQL")
                .setProperty("hibernate.connection.autocommit", "true")
                .setProperty("hibernate.show_sql", "false");

        // Tell Hibernate to use the 'SQL Server' dialect when dynamically
        // generating SQL queries
        cfg.setProperty("hibernate.dialect", "org.hibernate.dialect.SQLServerDialect");

        // Tell Hibernate to show the generated T-SQL
        cfg.setProperty("hibernate.show_sql", "false");

        // This is ok during development, but not recommended in production
        // See: http://stackoverflow.com/questions/221379/hibernate-hbm2ddl-auto-update-in-production
        cfg.setProperty("hibernate.hbm2ddl.auto", "update");
        return cfg;
    }
    
    
    
}
