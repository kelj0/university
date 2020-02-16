/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package datahandlers;

import db.dbHandler;
import java.io.File;
import org.w3c.dom.Document;
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import models.Ruta;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

/**
 *
 * @author keljo
 */
public class xmlDataHandler {
    private static dbHandler db = dbHandler.getInstance();

    public void fullDatabaseRestore(String dbname) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void fullDatabaseBackup(String dbname) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void exportRute(int ruta_id,String path, String fname) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public int importRute(String path) {
        try{
            File fXmlFile = new File(path);
            DocumentBuilder dBuilder = DocumentBuilderFactory.newInstance()
                             .newDocumentBuilder();

	    Document doc = dBuilder.parse(fXmlFile);
            doc.getDocumentElement().normalize();
            System.out.println("Root element :" + doc.getDocumentElement().getNodeName());
            NodeList nList = doc.getElementsByTagName("ruta");
            int temp = 0;
            for(; temp<nList.getLength(); ++temp){
                Node nNode = nList.item(temp);
                if (nNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element eElement = (Element) nNode;
                    double x_koordinata_a =   Double.parseDouble(eElement.getElementsByTagName("x_koordinata_a").item(0).getTextContent());
                    double y_koordinata_a =   Double.parseDouble(eElement.getElementsByTagName("y_koordinata_a").item(0).getTextContent());
                    double x_koordinata_b =   Double.parseDouble(eElement.getElementsByTagName("x_koordinata_b").item(0).getTextContent());
                    double y_koordinata_b =   Double.parseDouble(eElement.getElementsByTagName("y_koordinata_b").item(0).getTextContent());
                    double km_izmedu_a_b =    Double.parseDouble(eElement.getElementsByTagName("km_izmedu_a_b").item(0).getTextContent());
                    double prosjecna_brzina = Double.parseDouble(eElement.getElementsByTagName("prosjecna_brzina").item(0).getTextContent());
                    Ruta r = new Ruta(
                            x_koordinata_a,y_koordinata_a,
                            x_koordinata_b,y_koordinata_b,
                            km_izmedu_a_b,
                            prosjecna_brzina
                    );
                    db.InsertRuta(r);
		}
            }
            return temp;
        }catch (Exception e){
            e.printStackTrace();
        }
        return 0;
    }
    
}
